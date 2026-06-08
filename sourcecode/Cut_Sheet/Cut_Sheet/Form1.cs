using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cut_Sheet
{
    public partial class Form1 : Form
    {
        private bool _isRunning = false;

        private string _qrCode1 = string.Empty;
        private string _qrCode2 = string.Empty;

        public bool _result { get; set; } = false;

        private string _stationName = "PPG-01";
        private string _apiUrl = string.Empty;
        private bool _apiEnabled = false;

        // Patterns nhận dạng QR Phiếu Cắt — load từ App.config, refresh khi mở FormConfig
        private string[] _cutSheetEndsWith = new string[0];
        private string[] _cutSheetContains = new string[0];

        PLCModbusManager _plc;
        private string _plcIp = string.Empty;
        private int _plcPort = 502;

        // HttpClient dùng chung — bypass SSL vì server nội bộ dùng self-signed cert
        private static readonly HttpClient _httpClient = CreateHttpClient();

        // Badge trạng thái API — hiển thị kết quả gọi API gần nhất
        //private Label _labApiStatus;

        // Hàng đợi gửi bù — tab-delimited, mỗi dòng 1 bản ghi thất bại
        private static readonly object _queueFileLock = new object();
        private static readonly string _queueFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "api_retry_queue.txt");

        private static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler();
            // HACK(auto, 2026-05-27): Bỏ qua SSL validation — 192.168.96.10 dùng self-signed cert.
            //   Xoá khi server được cấp cert hợp lệ.
            handler.ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true;
            return new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(10) };
        }

        /// <summary>
        /// địa chỉ tuyệt đối thanh ghi holding cho vùng nhớ D0
        /// </summary>
        private ushort _d0Register = 4096;
        private ushort _x0Address = 57344;

        private bool _x0Value = false;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_plc != null)
            {
                _plc.Dispose();
            }
        }

        private void LoadQrPatterns()
        {
            var endsWithStr = ConfigurationManager.AppSettings["CutSheetQR_EndsWith"] ?? string.Empty;
            var containsStr = ConfigurationManager.AppSettings["CutSheetQR_Contains"] ?? string.Empty;

            _cutSheetEndsWith = endsWithStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            _cutSheetContains = containsStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool IsCutSheetQr(string text)
        {
            foreach (var p in _cutSheetEndsWith)
                if (text.EndsWith(p)) return true;
            foreach (var p in _cutSheetContains)
                if (text.Contains(p)) return true;
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _stationName = ConfigurationManager.AppSettings["Station"] ?? "PPG-01";
            _apiUrl = ConfigurationManager.AppSettings["AldilaCuttingApi_Url"] ?? string.Empty;
            _apiEnabled = string.Equals(ConfigurationManager.AppSettings["AldilaCuttingApi_Enabled"],
                "true", StringComparison.OrdinalIgnoreCase);
            _labStation.Text = $"Station: {_stationName}";
            _plcIp = ConfigurationManager.AppSettings["PlcIp"];
            _plcPort = int.TryParse(ConfigurationManager.AppSettings["PlcPort"], out int value) ? value : 502;

            LoadQrPatterns();

            _btnStartStop.Text = "BẮT ĐẦU";
            _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);

         
            _btnConfig.Click += (s, args) =>
            {
                using (var login = new FormLogin())
                {
                    if (login.ShowDialog(this) != DialogResult.OK)
                        return;
                }

                using (var frm = new FormConfig())
                {
                    frm.ShowDialog(this);
                    LoadQrPatterns();
                }
            };

            // Badge trạng thái API — nằm bên phải, cùng hàng với các nút bấm
            //_labApiStatus = new Label
            //{
            //    Text = "API: Chưa gửi",
            //    Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold),
            //    BackColor = Color.DarkGray,
            //    ForeColor = Color.White,
            //    Location = new Point(600, 594),
            //    Size = new Size(960, 100),
            //    TextAlign = ContentAlignment.MiddleCenter,
            //};
            //this.Controls.Add(_labApiStatus);

            _btnStartStop.Click += _btnStartStop_Click;
            _txtQR1.KeyDown += _txtQR1_KeyDown;
            _txtQR2.KeyDown += _txtQR2_KeyDown;

            _txtQR1.Focus();

            _plc = new PLCModbusManager(_plcIp, _plcPort);

            if (_plc.EnsureConnection())
            {
                _plc.WriteRegisterSafe(_d0Register, 0);
            }

            Task.Run(() =>
            {
                while (true)
                {
                    var data = _plc.ReadHoldingRegistersSafe(_d0Register, 1);
                    bool[] coils = _plc.ReadCoilsSafe(_x0Address, 1);

                    if (coils != null)
                    {
                        _x0Value = coils[0];
                    }

                    InvokeIfRequired(this, () =>
                    {
                        if (data != null)
                        {
                            _labStatus.Text = $"[ {DateTime.Now:HH:mm:ss} ] TT kết nối PLC: {_plc.IsConnected} - Cảm biến: {_x0Value} - Cho phép máy chạy: {data[0]}";
                        }
                        else
                        {
                            _labStatus.Text = $"[ {DateTime.Now:HH:mm:ss} ] Mất kết nối PLC, đang chờ thử lại...";
                        }
                    });

                    if (!_x0Value)
                    {
                        _qrCode1 = string.Empty;
                        _qrCode2 = string.Empty;
                        _isRunning = false;

                        InvokeIfRequired(this, () =>
                        {
                            _txtQR1.Text = string.Empty;
                            _txtQR2.Text = string.Empty;
                            _txtTextQr1.Text = string.Empty;
                            _txtTextQr2.Text = string.Empty;

                            _labResult.Text = "";
                            _labResult.BackColor = Color.DarkGray;

                            _btnStartStop.Text = "BẮT ĐẦU";
                            _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);

                            _txtQR1.Focus();
                        });
                    }

                    Thread.Sleep(200);
                }
            });

            // Vòng lặp gửi bù các bản ghi API bị lỗi đang nằm trong file hàng đợi
            Task.Run(StartRetryLoopAsync);
        }

        private void _txtQR2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var t = sender as TextBox;

                // QR2 là cuộn Prepreg — hợp lệ khi KHÔNG khớp pattern phiếu cắt
                if (!t.Text.Contains("-") || IsCutSheetQr(t.Text))
                {
                    MessageBox.Show("QR code không hợp lệ.");

                    _qrCode2 = string.Empty;

                    InvokeIfRequired(this, () =>
                    {
                        _txtQR2.Text = string.Empty;
                        _txtTextQr2.Text = string.Empty;

                        _txtQR2.Focus();
                    });

                    return;
                }

                _qrCode2 = t.Text;

                var arr = _qrCode2.Split('-');
                InvokeIfRequired(_txtTextQr2, () => _txtTextQr2.Text = arr[1]);
                InvokeIfRequired(_txtQR2, () => _txtQR2.Text = arr[0]);

                e.Handled = true;
                e.SuppressKeyPress = true;

                _btnStartStop.Focus();

                _btnStartStop_Click(sender, e);
            }
        }

        private void _txtQR1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var t = sender as TextBox;

                // QR1 là phiếu cắt — hợp lệ khi PHẢI khớp pattern phiếu cắt
                if (!t.Text.Contains("-") || !IsCutSheetQr(t.Text))
                {
                    MessageBox.Show("QR code không hợp lệ.");

                    _qrCode1 = string.Empty;

                    InvokeIfRequired(this, () =>
                    {
                        _txtQR1.Text = string.Empty;
                        _txtTextQr1.Text = string.Empty;

                        _txtQR1.Focus();
                    });

                    return;
                }

                _qrCode1 = t.Text;

                var arr = _qrCode1.Split('-');
                InvokeIfRequired(_txtTextQr1, () => _txtTextQr1.Text = arr[1]);
                InvokeIfRequired(_txtQR1, () => _txtQR1.Text = arr[0]);

                e.Handled = true;
                e.SuppressKeyPress = true;

                _txtQR2.Focus();
            }
        }

        private void _btnStartStop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_qrCode1) || string.IsNullOrEmpty(_qrCode2))
            {
                MessageBox.Show("Chưa quét mã QR trên cuộn Prepreg hoặc trên phiếu cắt.");
                return;
            }

            _isRunning = !_isRunning;

            if (_isRunning)
            {
                var arr1 = _qrCode1.Split('-');
                var arr2 = _qrCode2.Split('-');

                _result = arr1[0].Trim() == arr2[0].Trim();

                var scanTime = DateTime.Now;

                // QR1 = phiếu cắt (cut sheet order) → prepregOrderItem
                var orderItemId = arr1[0].Trim();
                var orderItemName = arr1[1];

                // QR2 = cuộn Prepreg thực tế → prepregItem
                var prepregItemId = arr2[0].Trim();
                var prepregItemName = arr2[1];

                if (_result)
                {
                    _plc.WriteRegisterSafe(_d0Register, 1);

                    _ = PostCuttingValidatorAsync(
                        _stationName,
                        prepregItemId, prepregItemName,
                        orderItemId, orderItemName,
                        scanTime, passed: true);

                    InvokeIfRequired(this, () =>
                    {
                        _labResult.Text = "MÃ PREPREG HỢP LỆ";
                        _labResult.BackColor = Color.Green;
                        _btnStartStop.Text = "KẾT THÚC";
                        _btnStartStop.BackColor = Color.Chocolate;
                    });
                }
                else
                {
                    _plc.WriteRegisterSafe(_d0Register, 0);

                    _ = PostCuttingValidatorAsync(
                        _stationName,
                        prepregItemId, prepregItemName,
                        orderItemId, orderItemName,
                        scanTime, passed: false);

                    _qrCode1 = string.Empty;
                    _qrCode2 = string.Empty;
                    _isRunning = false;

                    InvokeIfRequired(this, () =>
                    {
                        _txtQR1.Text = string.Empty;
                        _txtQR2.Text = string.Empty;
                        _txtTextQr1.Text = string.Empty;
                        _txtTextQr2.Text = string.Empty;

                        _labResult.Text = "MÃ PREPREG KHÔNG HỢP LỆ";
                        _labResult.BackColor = Color.Red;
                        _btnStartStop.Text = "BẮT ĐẦU";
                        _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);

                        _txtQR1.Focus();
                    });
                }
            }
            else
            {
                _plc.WriteRegisterSafe(_d0Register, 0);

                _qrCode1 = string.Empty;
                _qrCode2 = string.Empty;
                _isRunning = false;

                InvokeIfRequired(this, () =>
                {
                    _txtQR1.Text = string.Empty;
                    _txtQR2.Text = string.Empty;
                    _txtTextQr1.Text = string.Empty;
                    _txtTextQr2.Text = string.Empty;

                    _labResult.Text = "";
                    _labResult.BackColor = Color.DarkGray;

                    _btnStartStop.Text = "BẮT ĐẦU";
                    _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);

                    _txtQR1.Focus();
                });
            }
        }

        // ─── API: gửi kết quả + retry queue ────────────────────────────────────────

        /// <summary>
        /// Gửi kết quả quét QR lên Aldila Cutting Validator API (fire-and-forget).
        /// Nếu thất bại, lưu vào file hàng đợi để gửi lại trong vòng lặp retry.
        /// </summary>
        private async Task PostCuttingValidatorAsync(
            string stationName,
            string prepregItemId, string prepregItemName,
            string prepregOrderItemId, string prepregOrderItemName,
            DateTime scannedAt, bool passed)
        {
            if (string.IsNullOrWhiteSpace(_apiUrl) || !_apiEnabled)
                return;

            var scannedDateTimeStr = scannedAt.ToString("dd/MM/yyyy hh:mm tt",
                System.Globalization.CultureInfo.InvariantCulture);
            var result = passed ? "PASSED" : "FAILED";

            bool success = await TryPostAsync(_apiUrl,
                stationName, prepregItemId, prepregItemName,
                prepregOrderItemId, prepregOrderItemName,
                scannedDateTimeStr, result).ConfigureAwait(false);

            if (success)
            {
                SetApiBadge(success: true, pendingCount: GetQueueCount());
            }
            else
            {
                EnqueueRecord(stationName, prepregItemId, prepregItemName,
                    prepregOrderItemId, prepregOrderItemName, scannedDateTimeStr, result);
                SetApiBadge(success: false, pendingCount: GetQueueCount());
            }
        }

        /// <summary>
        /// HTTP POST helper. Returns true on 2xx, false on any error/exception.
        /// </summary>
        private static async Task<bool> TryPostAsync(
            string apiUrl,
            string stationName, string prepregItemId, string prepregItemName,
            string prepregOrderItemId, string prepregOrderItemName,
            string scannedDateTime, string result)
        {
            try
            {
                var json = "{"
                    + $"\"stationName\":\"{EscapeJson(stationName)}\","
                    + $"\"prepregItemId\":\"{EscapeJson(prepregItemId)}\","
                    + $"\"prepregItemName\":\"{EscapeJson(prepregItemName)}\","
                    + $"\"prepregOrderItemId\":\"{EscapeJson(prepregOrderItemId)}\","
                    + $"\"prepregOrderItemName\":\"{EscapeJson(prepregOrderItemName)}\","
                    + $"\"scannedDateTime\":\"{EscapeJson(scannedDateTime)}\","
                    + $"\"result\":\"{result}\""
                    + "}";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content).ConfigureAwait(false);

                System.Diagnostics.Debug.WriteLine(
                    $"[CuttingApi] {result} → HTTP {(int)response.StatusCode}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CuttingApi] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lưu bản ghi thất bại vào file hàng đợi (tab-delimited, 7 trường).
        /// Tab không xuất hiện trong dữ liệu QR hoặc tên trạm nên dùng làm dấu phân cách an toàn.
        /// </summary>
        private void EnqueueRecord(
            string stationName, string prepregItemId, string prepregItemName,
            string prepregOrderItemId, string prepregOrderItemName,
            string scannedDateTime, string result)
        {
            var line = string.Join("\t",
                stationName, prepregItemId, prepregItemName,
                prepregOrderItemId, prepregOrderItemName,
                scannedDateTime, result);

            lock (_queueFileLock)
                File.AppendAllLines(_queueFilePath, new[] { line }, Encoding.UTF8);
        }

        private int GetQueueCount()
        {
            lock (_queueFileLock)
            {
                if (!File.Exists(_queueFilePath)) return 0;
                return File.ReadAllLines(_queueFilePath, Encoding.UTF8)
                           .Count(l => !string.IsNullOrWhiteSpace(l));
            }
        }

        /// <summary>
        /// Cập nhật badge trạng thái API trên UI thread.
        /// </summary>
        private void SetApiBadge(bool success, int pendingCount = 0)
        {
            if (_labApiStatus == null || IsDisposed || !IsHandleCreated) return;
            InvokeIfRequired(this, () =>
            {
                if (_labApiStatus.IsDisposed) return;
                if (success && pendingCount == 0)
                {
                    _labApiStatus.BackColor = Color.FromArgb(0, 160, 0);
                    _labApiStatus.Text = $"API  ✓  Gửi thành công   ({DateTime.Now:HH:mm:ss})";
                }
                else
                {
                    _labApiStatus.BackColor = Color.OrangeRed;
                    _labApiStatus.Text =
                        $"API  ✗  Lỗi — Đang chờ gửi lại  ({pendingCount} bản ghi)   ({DateTime.Now:HH:mm:ss})";
                }
            });
        }

        /// <summary>
        /// Vòng lặp nền: cứ 30 giây thử gửi lại các bản ghi trong hàng đợi.
        /// </summary>
        private async Task StartRetryLoopAsync()
        {
            while (!IsDisposed)
            {
                await Task.Delay(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
                if (!IsDisposed)
                    await RetryQueueAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Một lượt retry: đọc file, thử POST từng dòng, xoá bản ghi thành công, ghi lại file.
        /// </summary>
        private async Task RetryQueueAsync()
        {
            string[] lines;
            lock (_queueFileLock)
            {
                if (!File.Exists(_queueFilePath)) return;
                lines = File.ReadAllLines(_queueFilePath, Encoding.UTF8)
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                            .ToArray();
            }
            if (lines.Length == 0) return;

            if (string.IsNullOrWhiteSpace(_apiUrl) || !_apiEnabled)
                return;

            var remaining = new List<string>();
            foreach (var line in lines)
            {
                var parts = line.Split('\t');
                if (parts.Length != 7) { remaining.Add(line); continue; }

                bool sent = await TryPostAsync(_apiUrl,
                    parts[0], parts[1], parts[2],
                    parts[3], parts[4], parts[5], parts[6])
                    .ConfigureAwait(false);

                System.Diagnostics.Debug.WriteLine(
                    $"[CuttingApi] retry {parts[6]}  {(sent ? "OK" : "FAIL")}");

                if (!sent) remaining.Add(line);
            }

            lock (_queueFileLock)
            {
                if (remaining.Count == 0)
                    File.Delete(_queueFilePath);
                else
                    File.WriteAllLines(_queueFilePath, remaining, Encoding.UTF8);
            }

            // Cập nhật badge nếu ít nhất một bản ghi được gửi thành công trong lần này
            if (remaining.Count < lines.Length)
                SetApiBadge(success: remaining.Count == 0, pendingCount: remaining.Count);
        }

        // ─── Helpers ────────────────────────────────────────────────────────────────

        /// <summary>Escape các ký tự đặc biệt trong chuỗi JSON.</summary>
        private static string EscapeJson(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return s.Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\r", "\\r")
                    .Replace("\n", "\\n")
                    .Replace("\t", "\\t");
        }

        public static void InvokeIfRequired(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        private void _txtTextQr1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

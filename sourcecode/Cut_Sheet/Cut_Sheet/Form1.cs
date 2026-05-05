using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
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

        PLCModbusManager _plc;
        private string _plcIp = string.Empty;
        private int _plcPort = 502;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Đọc giá trị dựa trên Key
            _labStation.Text = ConfigurationManager.AppSettings["Station"];
            _plcIp = ConfigurationManager.AppSettings["PlcIp"];
            _plcPort = int.TryParse(ConfigurationManager.AppSettings["PlcPort"], out int value) ? value : 502;

            _btnStartStop.Text = "BẮT ĐẦU";
            _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);

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
                    // 1. Đọc Digital Output (Coils) - Ví dụ: Trạng thái Relay
                    bool[] coils = _plc.ReadCoilsSafe(_x0Address, 1);

                    if (coils!=null)
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

                    Thread.Sleep(200); // Đợi 1 giây trước khi đọc lần tiếp theo
                }
            });
        }

        private void _txtQR2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var t = sender as TextBox;
                _qrCode2 = t.Text;

                InvokeIfRequired(_txtTextQr2, () => _txtTextQr2.Text = _qrCode2);

                // THÊM 2 DÒNG NÀY:
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Tự động nhảy sang ô nhập thứ 2 để tiện cho người dùng
                _btnStartStop.Focus();

                _btnStartStop_Click(sender, e);
            }
        }

        private void _txtQR1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var t = sender as TextBox;
                _qrCode1 = t.Text;

                InvokeIfRequired(_txtTextQr1, () => _txtTextQr1.Text = _qrCode1);

                // THÊM 2 DÒNG NÀY:
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Tự động nhảy sang ô nhập thứ 2 để tiện cho người dùng
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
                _result = _qrCode1 == _qrCode2;

                if (_result)
                {
                    //MessageBox.Show("KẾT QUẢ: ĐÚNG");
                    _plc.WriteRegisterSafe(_d0Register, 1);

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
                    //MessageBox.Show("KẾT QUẢ: SAI");
                    _plc.WriteRegisterSafe(_d0Register, 0);

                    _isRunning = false;

                    InvokeIfRequired(this, () =>
                    {
                        _labResult.Text = "MÃ PREPREG KHÔNG HỢP LỆ";
                        _labResult.BackColor = Color.Red;
                        _btnStartStop.Text = "BẮT ĐẦU";
                        _btnStartStop.BackColor = Color.FromArgb(0, 192, 0); ;
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
    }
}

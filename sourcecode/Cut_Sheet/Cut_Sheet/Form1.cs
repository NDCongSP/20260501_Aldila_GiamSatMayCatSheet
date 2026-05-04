using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private string _plcIp = string.Empty;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Đọc giá trị dựa trên Key
            _labStation.Text = ConfigurationManager.AppSettings["Station"];
            _plcIp = ConfigurationManager.AppSettings["IpAddress"];

            _btnStartStop.Text = "BẮT ĐẦU";
            _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);

            _btnStartStop.Click += _btnStartStop_Click;
            _txtQR1.KeyDown += _txtQR1_KeyDown;
            _txtQR2.KeyDown += _txtQR2_KeyDown;

            _txtQR1.Focus();
        }

        private void _txtQR2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _qrCode2 = _txtQR2.Text;

                InvokeIfRequired(_txtTextQr2, () => _txtTextQr2.Text = _qrCode2 );
            }
        }

        private void _txtQR1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                var t = sender as TextBox;
                _qrCode1 = t.Text;

                InvokeIfRequired(_txtTextQr1, () => _txtTextQr1.Text = _qrCode1 );
            }
        }

        private void _btnStartStop_Click(object sender, EventArgs e)
        {
            _isRunning = !_isRunning;

            if (_isRunning)
            {
                _result = _qrCode1 == _qrCode2;

                if (_result)
                {
                    //MessageBox.Show("KẾT QUẢ: ĐÚNG");
                   
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
                _qrCode1 = string.Empty;
                _qrCode2 = string.Empty;
         
                InvokeIfRequired(this, () =>
                {
                    _txtQR1.Text = string.Empty;
                    _txtQR2.Text = string.Empty;
                    _txtTextQr1.Text = string.Empty;
                    _txtTextQr2.Text = string.Empty;

                    _labResult.Text = "";
                    _labResult.BackColor = Color.DarkGray;

                    _txtQR1.Focus();
                });
                _btnStartStop.Text = "BẮT ĐẦU";
                _btnStartStop.BackColor = Color.FromArgb(0, 192, 0);
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

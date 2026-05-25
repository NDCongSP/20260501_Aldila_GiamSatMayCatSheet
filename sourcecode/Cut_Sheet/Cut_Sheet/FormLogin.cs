using System;
using System.Configuration;
using System.Windows.Forms;

namespace Cut_Sheet
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            this.AcceptButton = _btnLogin;
            this.CancelButton = _btnCancel;
            _txtUsername.Text = "admin";
            _txtPassword.Focus();
        }

        private void _btnLogin_Click(object sender, EventArgs e)
        {
            var expectedUser = ConfigurationManager.AppSettings["Config_Username"] ?? "admin";
            var expectedPass = ConfigurationManager.AppSettings["Config_Password"] ?? HashHelper.MD5Hash("admin");

            if (_txtUsername.Text.Trim() == expectedUser &&
                HashHelper.MD5Hash(_txtPassword.Text) == expectedPass)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                _lblError.Visible = true;
                _txtPassword.Clear();
                _txtPassword.Focus();
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                _btnLogin_Click(sender, e);
        }
    }
}

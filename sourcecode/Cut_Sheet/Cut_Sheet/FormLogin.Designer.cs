namespace Cut_Sheet
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._lblTitle    = new System.Windows.Forms.Label();
            this._lblUsername = new System.Windows.Forms.Label();
            this._txtUsername = new System.Windows.Forms.TextBox();
            this._lblPassword = new System.Windows.Forms.Label();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._lblError    = new System.Windows.Forms.Label();
            this._btnLogin    = new System.Windows.Forms.Button();
            this._btnCancel   = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // _lblTitle
            this._lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this._lblTitle.Location = new System.Drawing.Point(12, 16);
            this._lblTitle.Size = new System.Drawing.Size(316, 28);
            this._lblTitle.Text = "Đăng nhập Config";
            this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // _lblUsername
            this._lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._lblUsername.Location = new System.Drawing.Point(12, 60);
            this._lblUsername.Size = new System.Drawing.Size(100, 23);
            this._lblUsername.Text = "Tên đăng nhập:";
            this._lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // _txtUsername
            this._txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this._txtUsername.Location = new System.Drawing.Point(118, 58);
            this._txtUsername.Size = new System.Drawing.Size(210, 24);

            // _lblPassword
            this._lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._lblPassword.Location = new System.Drawing.Point(12, 98);
            this._lblPassword.Size = new System.Drawing.Size(100, 23);
            this._lblPassword.Text = "Mật khẩu:";
            this._lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // _txtPassword
            this._txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this._txtPassword.Location = new System.Drawing.Point(118, 96);
            this._txtPassword.Size = new System.Drawing.Size(210, 24);
            this._txtPassword.UseSystemPasswordChar = true;
            this._txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this._txtPassword_KeyDown);

            // _lblError
            this._lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this._lblError.ForeColor = System.Drawing.Color.Red;
            this._lblError.Location = new System.Drawing.Point(12, 128);
            this._lblError.Size = new System.Drawing.Size(316, 20);
            this._lblError.Text = "Sai tên đăng nhập hoặc mật khẩu.";
            this._lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lblError.Visible = false;

            // _btnLogin
            this._btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this._btnLogin.Location = new System.Drawing.Point(118, 156);
            this._btnLogin.Size = new System.Drawing.Size(100, 34);
            this._btnLogin.Text = "Đăng nhập";
            this._btnLogin.BackColor = System.Drawing.Color.RoyalBlue;
            this._btnLogin.ForeColor = System.Drawing.Color.White;
            this._btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLogin.Click += new System.EventHandler(this._btnLogin_Click);

            // _btnCancel
            this._btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this._btnCancel.Location = new System.Drawing.Point(228, 156);
            this._btnCancel.Size = new System.Drawing.Size(100, 34);
            this._btnCancel.Text = "Huỷ";
            this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 206);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác thực";

            this.Controls.Add(this._lblTitle);
            this.Controls.Add(this._lblUsername);
            this.Controls.Add(this._txtUsername);
            this.Controls.Add(this._lblPassword);
            this.Controls.Add(this._txtPassword);
            this.Controls.Add(this._lblError);
            this.Controls.Add(this._btnLogin);
            this.Controls.Add(this._btnCancel);

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label   _lblTitle;
        private System.Windows.Forms.Label   _lblUsername;
        private System.Windows.Forms.TextBox _txtUsername;
        private System.Windows.Forms.Label   _lblPassword;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.Label   _lblError;
        private System.Windows.Forms.Button  _btnLogin;
        private System.Windows.Forms.Button  _btnCancel;
    }
}

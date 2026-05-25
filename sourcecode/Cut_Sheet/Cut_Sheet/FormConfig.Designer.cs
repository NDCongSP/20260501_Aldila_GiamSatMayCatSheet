namespace Cut_Sheet
{
    partial class FormConfig
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
            this._tabControl     = new System.Windows.Forms.TabControl();
            this._tabQr          = new System.Windows.Forms.TabPage();
            this._tabErp         = new System.Windows.Forms.TabPage();
            this._tabAccount     = new System.Windows.Forms.TabPage();

            // ── Tab QR: controls ────────────────────────────────────────
            this._lblNote        = new System.Windows.Forms.Label();
            this._lblEndsWith    = new System.Windows.Forms.Label();
            this._lstEndsWith    = new System.Windows.Forms.ListBox();
            this._txtNewEndsWith = new System.Windows.Forms.TextBox();
            this._btnAddEndsWith    = new System.Windows.Forms.Button();
            this._btnRemoveEndsWith = new System.Windows.Forms.Button();
            this._lblContains    = new System.Windows.Forms.Label();
            this._lstContains    = new System.Windows.Forms.ListBox();
            this._txtNewContains = new System.Windows.Forms.TextBox();
            this._btnAddContains    = new System.Windows.Forms.Button();
            this._btnRemoveContains = new System.Windows.Forms.Button();

            // ── Tab ERP: controls ───────────────────────────────────────
            this._chkErpEnabled      = new System.Windows.Forms.CheckBox();
            this._lblErpNote         = new System.Windows.Forms.Label();
            this._lblErpTenantId     = new System.Windows.Forms.Label();
            this._txtErpTenantId     = new System.Windows.Forms.TextBox();
            this._lblErpClientId     = new System.Windows.Forms.Label();
            this._txtErpClientId     = new System.Windows.Forms.TextBox();
            this._lblErpClientSecret = new System.Windows.Forms.Label();
            this._txtErpClientSecret = new System.Windows.Forms.TextBox();
            this._chkShowSecret      = new System.Windows.Forms.CheckBox();
            this._lblErpBaseUrl      = new System.Windows.Forms.Label();
            this._txtErpBaseUrl      = new System.Windows.Forms.TextBox();
            this._lblErpEndpoint     = new System.Windows.Forms.Label();
            this._txtErpEndpoint     = new System.Windows.Forms.TextBox();

            // ── Tab Account: controls ───────────────────────────────────
            this._lblAccountNote       = new System.Windows.Forms.Label();
            this._lblCurrentUsername   = new System.Windows.Forms.Label();
            this._txtCurrentUsername   = new System.Windows.Forms.TextBox();
            this._lblNewPassword       = new System.Windows.Forms.Label();
            this._txtNewPassword       = new System.Windows.Forms.TextBox();
            this._lblConfirmPassword   = new System.Windows.Forms.Label();
            this._txtConfirmPassword   = new System.Windows.Forms.TextBox();
            this._btnChangePassword    = new System.Windows.Forms.Button();

            // ── Bottom ──────────────────────────────────────────────────
            this._btnSave  = new System.Windows.Forms.Button();
            this._btnClose = new System.Windows.Forms.Button();

            this._tabControl.SuspendLayout();
            this._tabQr.SuspendLayout();
            this._tabErp.SuspendLayout();
            this._tabAccount.SuspendLayout();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════════════
            // Tab QR — nội dung
            // ════════════════════════════════════════════════════════════

            // _lblNote
            this._lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this._lblNote.ForeColor = System.Drawing.Color.DarkSlateGray;
            this._lblNote.Location = new System.Drawing.Point(6, 6);
            this._lblNote.Size = new System.Drawing.Size(548, 32);
            this._lblNote.Text = "QR1 (Phiếu cắt) HỢP LỆ nếu khớp bất kỳ pattern nào.\r\n" +
                                 "QR2 (Cuộn Prepreg) HỢP LỆ nếu KHÔNG khớp bất kỳ pattern nào.";

            // EndsWith group
            this._lblEndsWith.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this._lblEndsWith.Location = new System.Drawing.Point(6, 44);
            this._lblEndsWith.Size = new System.Drawing.Size(260, 20);
            this._lblEndsWith.Text = "Kết thúc bằng (EndsWith):";

            this._lstEndsWith.Font = new System.Drawing.Font("Consolas", 11F);
            this._lstEndsWith.Location = new System.Drawing.Point(6, 68);
            this._lstEndsWith.Size = new System.Drawing.Size(258, 147);

            this._txtNewEndsWith.Font = new System.Drawing.Font("Consolas", 11F);
            this._txtNewEndsWith.Location = new System.Drawing.Point(6, 222);
            this._txtNewEndsWith.Size = new System.Drawing.Size(174, 24);

            this._btnAddEndsWith.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddEndsWith.Location = new System.Drawing.Point(188, 220);
            this._btnAddEndsWith.Size = new System.Drawing.Size(76, 28);
            this._btnAddEndsWith.Text = "Thêm";
            this._btnAddEndsWith.BackColor = System.Drawing.Color.MediumSeaGreen;
            this._btnAddEndsWith.ForeColor = System.Drawing.Color.White;
            this._btnAddEndsWith.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddEndsWith.Click += new System.EventHandler(this._btnAddEndsWith_Click);

            this._btnRemoveEndsWith.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._btnRemoveEndsWith.Location = new System.Drawing.Point(6, 254);
            this._btnRemoveEndsWith.Size = new System.Drawing.Size(258, 28);
            this._btnRemoveEndsWith.Text = "Xoá pattern đã chọn";
            this._btnRemoveEndsWith.BackColor = System.Drawing.Color.IndianRed;
            this._btnRemoveEndsWith.ForeColor = System.Drawing.Color.White;
            this._btnRemoveEndsWith.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRemoveEndsWith.Click += new System.EventHandler(this._btnRemoveEndsWith_Click);

            // Contains group
            this._lblContains.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this._lblContains.Location = new System.Drawing.Point(292, 44);
            this._lblContains.Size = new System.Drawing.Size(260, 20);
            this._lblContains.Text = "Chứa chuỗi (Contains):";

            this._lstContains.Font = new System.Drawing.Font("Consolas", 11F);
            this._lstContains.Location = new System.Drawing.Point(292, 68);
            this._lstContains.Size = new System.Drawing.Size(258, 147);

            this._txtNewContains.Font = new System.Drawing.Font("Consolas", 11F);
            this._txtNewContains.Location = new System.Drawing.Point(292, 222);
            this._txtNewContains.Size = new System.Drawing.Size(174, 24);

            this._btnAddContains.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddContains.Location = new System.Drawing.Point(474, 220);
            this._btnAddContains.Size = new System.Drawing.Size(76, 28);
            this._btnAddContains.Text = "Thêm";
            this._btnAddContains.BackColor = System.Drawing.Color.MediumSeaGreen;
            this._btnAddContains.ForeColor = System.Drawing.Color.White;
            this._btnAddContains.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddContains.Click += new System.EventHandler(this._btnAddContains_Click);

            this._btnRemoveContains.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._btnRemoveContains.Location = new System.Drawing.Point(292, 254);
            this._btnRemoveContains.Size = new System.Drawing.Size(258, 28);
            this._btnRemoveContains.Text = "Xoá pattern đã chọn";
            this._btnRemoveContains.BackColor = System.Drawing.Color.IndianRed;
            this._btnRemoveContains.ForeColor = System.Drawing.Color.White;
            this._btnRemoveContains.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRemoveContains.Click += new System.EventHandler(this._btnRemoveContains_Click);

            // _tabQr
            this._tabQr.Controls.Add(this._lblNote);
            this._tabQr.Controls.Add(this._lblEndsWith);
            this._tabQr.Controls.Add(this._lstEndsWith);
            this._tabQr.Controls.Add(this._txtNewEndsWith);
            this._tabQr.Controls.Add(this._btnAddEndsWith);
            this._tabQr.Controls.Add(this._btnRemoveEndsWith);
            this._tabQr.Controls.Add(this._lblContains);
            this._tabQr.Controls.Add(this._lstContains);
            this._tabQr.Controls.Add(this._txtNewContains);
            this._tabQr.Controls.Add(this._btnAddContains);
            this._tabQr.Controls.Add(this._btnRemoveContains);
            this._tabQr.Location = new System.Drawing.Point(4, 22);
            this._tabQr.Padding = new System.Windows.Forms.Padding(4);
            this._tabQr.Size = new System.Drawing.Size(562, 298);
            this._tabQr.Text = "QR Patterns";
            this._tabQr.UseVisualStyleBackColor = true;

            // ════════════════════════════════════════════════════════════
            // Tab ERP — nội dung
            // ════════════════════════════════════════════════════════════
            int col1 = 6, col2 = 150, fieldW = 400, rowH = 34, y = 10;

            // _chkErpEnabled
            this._chkErpEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this._chkErpEnabled.Location = new System.Drawing.Point(col1, y);
            this._chkErpEnabled.Size = new System.Drawing.Size(200, 24);
            this._chkErpEnabled.Text = "Bật tích hợp ERP";
            y += rowH;

            // _lblErpNote
            this._lblErpNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic);
            this._lblErpNote.ForeColor = System.Drawing.Color.Gray;
            this._lblErpNote.Location = new System.Drawing.Point(col1, y);
            this._lblErpNote.Size = new System.Drawing.Size(548, 20);
            this._lblErpNote.Text = "Dynamics 365 — xác thực OAuth2 qua Azure AD (Client Credentials flow)";
            y += 28;

            // TenantId
            this._lblErpTenantId.Text = "Tenant ID:";
            this._lblErpTenantId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._lblErpTenantId.Location = new System.Drawing.Point(col1, y + 3);
            this._lblErpTenantId.Size = new System.Drawing.Size(138, 18);

            this._txtErpTenantId.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtErpTenantId.Location = new System.Drawing.Point(col2, y);
            this._txtErpTenantId.Size = new System.Drawing.Size(fieldW, 23);
            y += rowH;

            // ClientId
            this._lblErpClientId.Text = "Client ID:";
            this._lblErpClientId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._lblErpClientId.Location = new System.Drawing.Point(col1, y + 3);
            this._lblErpClientId.Size = new System.Drawing.Size(138, 18);

            this._txtErpClientId.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtErpClientId.Location = new System.Drawing.Point(col2, y);
            this._txtErpClientId.Size = new System.Drawing.Size(fieldW, 23);
            y += rowH;

            // ClientSecret
            this._lblErpClientSecret.Text = "Client Secret:";
            this._lblErpClientSecret.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._lblErpClientSecret.Location = new System.Drawing.Point(col1, y + 3);
            this._lblErpClientSecret.Size = new System.Drawing.Size(138, 18);

            this._txtErpClientSecret.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtErpClientSecret.Location = new System.Drawing.Point(col2, y);
            this._txtErpClientSecret.Size = new System.Drawing.Size(fieldW, 23);
            this._txtErpClientSecret.UseSystemPasswordChar = true;
            y += rowH;

            // ShowSecret checkbox
            this._chkShowSecret.Text = "Hiển thị Secret";
            this._chkShowSecret.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Italic);
            this._chkShowSecret.Location = new System.Drawing.Point(col2, y);
            this._chkShowSecret.Size = new System.Drawing.Size(140, 20);
            this._chkShowSecret.CheckedChanged += new System.EventHandler(this._chkShowSecret_CheckedChanged);
            y += 26;

            // BaseUrl
            this._lblErpBaseUrl.Text = "Base URL:";
            this._lblErpBaseUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._lblErpBaseUrl.Location = new System.Drawing.Point(col1, y + 3);
            this._lblErpBaseUrl.Size = new System.Drawing.Size(138, 18);

            this._txtErpBaseUrl.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtErpBaseUrl.Location = new System.Drawing.Point(col2, y);
            this._txtErpBaseUrl.Size = new System.Drawing.Size(fieldW, 23);
            y += rowH;

            // Endpoint
            this._lblErpEndpoint.Text = "API Endpoint:";
            this._lblErpEndpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this._lblErpEndpoint.Location = new System.Drawing.Point(col1, y + 3);
            this._lblErpEndpoint.Size = new System.Drawing.Size(138, 18);

            this._txtErpEndpoint.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtErpEndpoint.Location = new System.Drawing.Point(col2, y);
            this._txtErpEndpoint.Size = new System.Drawing.Size(fieldW, 23);

            // _tabErp
            this._tabErp.Controls.Add(this._chkErpEnabled);
            this._tabErp.Controls.Add(this._lblErpNote);
            this._tabErp.Controls.Add(this._lblErpTenantId);
            this._tabErp.Controls.Add(this._txtErpTenantId);
            this._tabErp.Controls.Add(this._lblErpClientId);
            this._tabErp.Controls.Add(this._txtErpClientId);
            this._tabErp.Controls.Add(this._lblErpClientSecret);
            this._tabErp.Controls.Add(this._txtErpClientSecret);
            this._tabErp.Controls.Add(this._chkShowSecret);
            this._tabErp.Controls.Add(this._lblErpBaseUrl);
            this._tabErp.Controls.Add(this._txtErpBaseUrl);
            this._tabErp.Controls.Add(this._lblErpEndpoint);
            this._tabErp.Controls.Add(this._txtErpEndpoint);
            this._tabErp.Location = new System.Drawing.Point(4, 22);
            this._tabErp.Padding = new System.Windows.Forms.Padding(4);
            this._tabErp.Size = new System.Drawing.Size(562, 298);
            this._tabErp.Text = "ERP Kết nối";
            this._tabErp.UseVisualStyleBackColor = true;

            // ════════════════════════════════════════════════════════════
            // Tab Account — nội dung
            // ════════════════════════════════════════════════════════════
            int aCol1 = 6, aCol2 = 160, aFieldW = 350, aRowH = 36, aY = 12;

            this._lblAccountNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this._lblAccountNote.ForeColor = System.Drawing.Color.Gray;
            this._lblAccountNote.Location = new System.Drawing.Point(aCol1, aY);
            this._lblAccountNote.Size = new System.Drawing.Size(540, 20);
            this._lblAccountNote.Text = "Thay đổi tên đăng nhập hoặc mật khẩu để mở Config.";
            aY += 30;

            this._lblCurrentUsername.Text = "Tên đăng nhập:";
            this._lblCurrentUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._lblCurrentUsername.Location = new System.Drawing.Point(aCol1, aY + 3);
            this._lblCurrentUsername.Size = new System.Drawing.Size(148, 20);

            this._txtCurrentUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this._txtCurrentUsername.Location = new System.Drawing.Point(aCol2, aY);
            this._txtCurrentUsername.Size = new System.Drawing.Size(aFieldW, 24);
            aY += aRowH;

            this._lblNewPassword.Text = "Mật khẩu mới:";
            this._lblNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._lblNewPassword.Location = new System.Drawing.Point(aCol1, aY + 3);
            this._lblNewPassword.Size = new System.Drawing.Size(148, 20);

            this._txtNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this._txtNewPassword.Location = new System.Drawing.Point(aCol2, aY);
            this._txtNewPassword.Size = new System.Drawing.Size(aFieldW, 24);
            this._txtNewPassword.UseSystemPasswordChar = true;
            aY += aRowH;

            this._lblConfirmPassword.Text = "Xác nhận mật khẩu:";
            this._lblConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._lblConfirmPassword.Location = new System.Drawing.Point(aCol1, aY + 3);
            this._lblConfirmPassword.Size = new System.Drawing.Size(148, 20);

            this._txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this._txtConfirmPassword.Location = new System.Drawing.Point(aCol2, aY);
            this._txtConfirmPassword.Size = new System.Drawing.Size(aFieldW, 24);
            this._txtConfirmPassword.UseSystemPasswordChar = true;
            aY += aRowH + 8;

            this._btnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this._btnChangePassword.Location = new System.Drawing.Point(aCol2, aY);
            this._btnChangePassword.Size = new System.Drawing.Size(180, 34);
            this._btnChangePassword.Text = "Cập nhật tài khoản";
            this._btnChangePassword.BackColor = System.Drawing.Color.SteelBlue;
            this._btnChangePassword.ForeColor = System.Drawing.Color.White;
            this._btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnChangePassword.Click += new System.EventHandler(this._btnChangePassword_Click);

            this._tabAccount.Controls.Add(this._lblAccountNote);
            this._tabAccount.Controls.Add(this._lblCurrentUsername);
            this._tabAccount.Controls.Add(this._txtCurrentUsername);
            this._tabAccount.Controls.Add(this._lblNewPassword);
            this._tabAccount.Controls.Add(this._txtNewPassword);
            this._tabAccount.Controls.Add(this._lblConfirmPassword);
            this._tabAccount.Controls.Add(this._txtConfirmPassword);
            this._tabAccount.Controls.Add(this._btnChangePassword);
            this._tabAccount.Location = new System.Drawing.Point(4, 22);
            this._tabAccount.Padding = new System.Windows.Forms.Padding(4);
            this._tabAccount.Size = new System.Drawing.Size(562, 298);
            this._tabAccount.Text = "Tài khoản";
            this._tabAccount.UseVisualStyleBackColor = true;

            // ════════════════════════════════════════════════════════════
            // TabControl
            // ════════════════════════════════════════════════════════════
            this._tabControl.Controls.Add(this._tabQr);
            this._tabControl.Controls.Add(this._tabErp);
            this._tabControl.Controls.Add(this._tabAccount);
            this._tabControl.Location = new System.Drawing.Point(6, 6);
            this._tabControl.Size = new System.Drawing.Size(570, 324);
            this._tabControl.SelectedIndex = 0;

            // ════════════════════════════════════════════════════════════
            // Bottom buttons
            // ════════════════════════════════════════════════════════════
            this._btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this._btnSave.Location = new System.Drawing.Point(368, 338);
            this._btnSave.Size = new System.Drawing.Size(100, 36);
            this._btnSave.Text = "Lưu";
            this._btnSave.BackColor = System.Drawing.Color.RoyalBlue;
            this._btnSave.ForeColor = System.Drawing.Color.White;
            this._btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);

            this._btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this._btnClose.Location = new System.Drawing.Point(476, 338);
            this._btnClose.Size = new System.Drawing.Size(100, 36);
            this._btnClose.Text = "Đóng";
            this._btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);

            // ════════════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════════════
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 384);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cấu hình";

            this.Controls.Add(this._tabControl);
            this.Controls.Add(this._btnSave);
            this.Controls.Add(this._btnClose);

            this._tabControl.ResumeLayout(false);
            this._tabQr.ResumeLayout(false);
            this._tabErp.ResumeLayout(false);
            this._tabAccount.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ── Tab control ─────────────────────────────────────────────────
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage    _tabQr;
        private System.Windows.Forms.TabPage    _tabErp;

        // ── Tab QR controls ──────────────────────────────────────────────
        private System.Windows.Forms.Label   _lblNote;
        private System.Windows.Forms.Label   _lblEndsWith;
        private System.Windows.Forms.ListBox _lstEndsWith;
        private System.Windows.Forms.TextBox _txtNewEndsWith;
        private System.Windows.Forms.Button  _btnAddEndsWith;
        private System.Windows.Forms.Button  _btnRemoveEndsWith;
        private System.Windows.Forms.Label   _lblContains;
        private System.Windows.Forms.ListBox _lstContains;
        private System.Windows.Forms.TextBox _txtNewContains;
        private System.Windows.Forms.Button  _btnAddContains;
        private System.Windows.Forms.Button  _btnRemoveContains;

        // ── Tab ERP controls ─────────────────────────────────────────────
        private System.Windows.Forms.CheckBox _chkErpEnabled;
        private System.Windows.Forms.Label    _lblErpNote;
        private System.Windows.Forms.Label    _lblErpTenantId;
        private System.Windows.Forms.TextBox  _txtErpTenantId;
        private System.Windows.Forms.Label    _lblErpClientId;
        private System.Windows.Forms.TextBox  _txtErpClientId;
        private System.Windows.Forms.Label    _lblErpClientSecret;
        private System.Windows.Forms.TextBox  _txtErpClientSecret;
        private System.Windows.Forms.CheckBox _chkShowSecret;
        private System.Windows.Forms.Label    _lblErpBaseUrl;
        private System.Windows.Forms.TextBox  _txtErpBaseUrl;
        private System.Windows.Forms.Label    _lblErpEndpoint;
        private System.Windows.Forms.TextBox  _txtErpEndpoint;

        // ── Tab Account controls ─────────────────────────────────────────
        private System.Windows.Forms.TabPage  _tabAccount;
        private System.Windows.Forms.Label    _lblAccountNote;
        private System.Windows.Forms.Label    _lblCurrentUsername;
        private System.Windows.Forms.TextBox  _txtCurrentUsername;
        private System.Windows.Forms.Label    _lblNewPassword;
        private System.Windows.Forms.TextBox  _txtNewPassword;
        private System.Windows.Forms.Label    _lblConfirmPassword;
        private System.Windows.Forms.TextBox  _txtConfirmPassword;
        private System.Windows.Forms.Button   _btnChangePassword;

        // ── Bottom ────────────────────────────────────────────────────────
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnClose;
    }
}

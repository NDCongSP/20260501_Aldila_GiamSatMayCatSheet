using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace Cut_Sheet
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            LoadQrPatterns();
            LoadErpSettings();
            LoadAccountSettings();
        }

        // ─── Tab 1: QR Patterns ───────────────────────────────────────────

        private void LoadQrPatterns()
        {
            var endsWithStr = ConfigurationManager.AppSettings["CutSheetQR_EndsWith"] ?? string.Empty;
            var containsStr = ConfigurationManager.AppSettings["CutSheetQR_Contains"] ?? string.Empty;

            _lstEndsWith.Items.Clear();
            foreach (var p in endsWithStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var t = p.Trim();
                if (!string.IsNullOrEmpty(t)) _lstEndsWith.Items.Add(t);
            }

            _lstContains.Items.Clear();
            foreach (var p in containsStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var t = p.Trim();
                if (!string.IsNullOrEmpty(t)) _lstContains.Items.Add(t);
            }
        }

        private void _btnAddEndsWith_Click(object sender, EventArgs e)
        {
            var val = _txtNewEndsWith.Text.Trim();
            if (string.IsNullOrEmpty(val)) return;
            if (!_lstEndsWith.Items.Contains(val)) _lstEndsWith.Items.Add(val);
            _txtNewEndsWith.Clear();
            _txtNewEndsWith.Focus();
        }

        private void _btnRemoveEndsWith_Click(object sender, EventArgs e)
        {
            if (_lstEndsWith.SelectedIndex >= 0)
                _lstEndsWith.Items.RemoveAt(_lstEndsWith.SelectedIndex);
        }

        private void _btnAddContains_Click(object sender, EventArgs e)
        {
            var val = _txtNewContains.Text.Trim();
            if (string.IsNullOrEmpty(val)) return;
            if (!_lstContains.Items.Contains(val)) _lstContains.Items.Add(val);
            _txtNewContains.Clear();
            _txtNewContains.Focus();
        }

        private void _btnRemoveContains_Click(object sender, EventArgs e)
        {
            if (_lstContains.SelectedIndex >= 0)
                _lstContains.Items.RemoveAt(_lstContains.SelectedIndex);
        }

        private void SaveQrPatterns()
        {
            var ends = new List<string>();
            foreach (var item in _lstEndsWith.Items) ends.Add(item.ToString());

            var contains = new List<string>();
            foreach (var item in _lstContains.Items) contains.Add(item.ToString());

            SaveAppSetting("CutSheetQR_EndsWith", string.Join("|", ends));
            SaveAppSetting("CutSheetQR_Contains", string.Join("|", contains));
        }

        // ─── Tab 2: ERP Settings ─────────────────────────────────────────

        private void LoadErpSettings()
        {
            _txtErpTenantId.Text     = ConfigurationManager.AppSettings["ERP_TenantId"] ?? string.Empty;
            _txtErpClientId.Text     = ConfigurationManager.AppSettings["ERP_ClientId"] ?? string.Empty;
            _txtErpBaseUrl.Text      = ConfigurationManager.AppSettings["ERP_BaseUrl"]  ?? string.Empty;
            _txtErpEndpoint.Text     = ConfigurationManager.AppSettings["ERP_Endpoint"] ?? string.Empty;
            _chkErpEnabled.Checked   = string.Equals(
                ConfigurationManager.AppSettings["ERP_Enabled"], "true",
                StringComparison.OrdinalIgnoreCase);

            // ClientSecret không hiển thị hash — để trống, user nhập mới nếu muốn đổi
            _txtErpClientSecret.Text = string.Empty;
        }

        private void SaveErpSettings()
        {
            SaveAppSetting("ERP_TenantId", _txtErpTenantId.Text.Trim());
            SaveAppSetting("ERP_ClientId", _txtErpClientId.Text.Trim());
            SaveAppSetting("ERP_BaseUrl",  _txtErpBaseUrl.Text.Trim());
            SaveAppSetting("ERP_Endpoint", _txtErpEndpoint.Text.Trim());
            SaveAppSetting("ERP_Enabled",  _chkErpEnabled.Checked ? "true" : "false");

            // Chỉ cập nhật ClientSecret khi user nhập mới (không để trống)
            var secret = _txtErpClientSecret.Text.Trim();
            if (!string.IsNullOrEmpty(secret))
                SaveAppSetting("ERP_ClientSecret", HashHelper.MD5Hash(secret));
        }

        private void _chkShowSecret_CheckedChanged(object sender, EventArgs e)
        {
            _txtErpClientSecret.UseSystemPasswordChar = !_chkShowSecret.Checked;
        }

        // ─── Tab 3: Account ──────────────────────────────────────────────

        private void LoadAccountSettings()
        {
            _txtCurrentUsername.Text = ConfigurationManager.AppSettings["Config_Username"] ?? "admin";
        }

        private void _btnChangePassword_Click(object sender, EventArgs e)
        {
            var username = _txtCurrentUsername.Text.Trim();
            var newPass  = _txtNewPassword.Text;
            var confirm  = _txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Mật khẩu mới không được để trống.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtConfirmPassword.Clear();
                _txtConfirmPassword.Focus();
                return;
            }

            SaveAppSetting("Config_Username", username);
            SaveAppSetting("Config_Password", HashHelper.MD5Hash(newPass));

            _txtNewPassword.Clear();
            _txtConfirmPassword.Clear();

            MessageBox.Show("Đã cập nhật tài khoản thành công.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─── Save / Close ─────────────────────────────────────────────────

        private void _btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveQrPatterns();
                SaveErpSettings();

                MessageBox.Show("Đã lưu cấu hình thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu cấu hình: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _btnClose_Click(object sender, EventArgs e) => Close();

        // ─── Helper ───────────────────────────────────────────────────────

        private static void SaveAppSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings[key].Value = value;
            else
                config.AppSettings.Settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

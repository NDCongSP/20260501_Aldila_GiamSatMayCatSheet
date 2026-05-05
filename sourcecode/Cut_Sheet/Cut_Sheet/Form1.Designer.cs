namespace Cut_Sheet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this._labStation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._txtQR1 = new System.Windows.Forms.TextBox();
            this._txtQR2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._labResult = new System.Windows.Forms.Label();
            this._txtTextQr1 = new System.Windows.Forms.TextBox();
            this._txtTextQr2 = new System.Windows.Forms.TextBox();
            this._labStatus = new System.Windows.Forms.Label();
            this._btnStartStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Cut_Sheet.Properties.Resources.logoAldila;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(106, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(105, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(773, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prepreg Cutting Validation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _labStation
            // 
            this._labStation.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._labStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labStation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._labStation.Location = new System.Drawing.Point(878, 0);
            this._labStation.Name = "_labStation";
            this._labStation.Size = new System.Drawing.Size(386, 51);
            this._labStation.TabIndex = 2;
            this._labStation.Text = "Station: PPG-02";
            this._labStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(322, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mã QR trên cuộn Prepreg";
            // 
            // _txtQR1
            // 
            this._txtQR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtQR1.ForeColor = System.Drawing.Color.Green;
            this._txtQR1.Location = new System.Drawing.Point(12, 102);
            this._txtQR1.Multiline = true;
            this._txtQR1.Name = "_txtQR1";
            this._txtQR1.Size = new System.Drawing.Size(307, 92);
            this._txtQR1.TabIndex = 1;
            this._txtQR1.Text = "SK02022";
            // 
            // _txtQR2
            // 
            this._txtQR2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtQR2.ForeColor = System.Drawing.Color.Blue;
            this._txtQR2.Location = new System.Drawing.Point(12, 262);
            this._txtQR2.Multiline = true;
            this._txtQR2.Name = "_txtQR2";
            this._txtQR2.Size = new System.Drawing.Size(307, 92);
            this._txtQR2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(12, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mã QR trên phiếu cắt";
            // 
            // _labResult
            // 
            this._labResult.BackColor = System.Drawing.SystemColors.WindowFrame;
            this._labResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labResult.Location = new System.Drawing.Point(10, 452);
            this._labResult.Name = "_labResult";
            this._labResult.Size = new System.Drawing.Size(1227, 116);
            this._labResult.TabIndex = 8;
            this._labResult.Text = "Prepreg Cutting Validation";
            this._labResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _txtTextQr1
            // 
            this._txtTextQr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtTextQr1.ForeColor = System.Drawing.Color.Green;
            this._txtTextQr1.Location = new System.Drawing.Point(371, 102);
            this._txtTextQr1.Multiline = true;
            this._txtTextQr1.Name = "_txtTextQr1";
            this._txtTextQr1.ReadOnly = true;
            this._txtTextQr1.Size = new System.Drawing.Size(868, 92);
            this._txtTextQr1.TabIndex = 9;
            this._txtTextQr1.Text = "dfafdsajfhasDFKJASDHFA KJCHASDKLFHASDKFL HUASDOFUHASLDF ÁDFGASDFG";
            // 
            // _txtTextQr2
            // 
            this._txtTextQr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtTextQr2.ForeColor = System.Drawing.Color.Blue;
            this._txtTextQr2.Location = new System.Drawing.Point(371, 262);
            this._txtTextQr2.Multiline = true;
            this._txtTextQr2.Name = "_txtTextQr2";
            this._txtTextQr2.ReadOnly = true;
            this._txtTextQr2.Size = new System.Drawing.Size(868, 92);
            this._txtTextQr2.TabIndex = 10;
            // 
            // _labStatus
            // 
            this._labStatus.AutoSize = true;
            this._labStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._labStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labStatus.Location = new System.Drawing.Point(0, 587);
            this._labStatus.Name = "_labStatus";
            this._labStatus.Size = new System.Drawing.Size(192, 25);
            this._labStatus.TabIndex = 11;
            this._labStatus.Text = "PLC connect Status:";
            // 
            // _btnStartStop
            // 
            this._btnStartStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this._btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnStartStop.Location = new System.Drawing.Point(12, 371);
            this._btnStartStop.Name = "_btnStartStop";
            this._btnStartStop.Size = new System.Drawing.Size(307, 68);
            this._btnStartStop.TabIndex = 3;
            this._btnStartStop.Text = "KẾT THÚC";
            this._btnStartStop.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 612);
            this.Controls.Add(this._btnStartStop);
            this.Controls.Add(this._labStatus);
            this.Controls.Add(this._txtTextQr2);
            this.Controls.Add(this._txtTextQr1);
            this.Controls.Add(this._labResult);
            this.Controls.Add(this._txtQR2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._txtQR1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._labStation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _labStation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtQR1;
        private System.Windows.Forms.TextBox _txtQR2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label _labResult;
        private System.Windows.Forms.TextBox _txtTextQr1;
        private System.Windows.Forms.TextBox _txtTextQr2;
        private System.Windows.Forms.Label _labStatus;
        private System.Windows.Forms.Button _btnStartStop;
    }
}


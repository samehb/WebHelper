namespace WebHelper
{
    partial class SettingsForm
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
            this.ProxyExceptionsLbl = new System.Windows.Forms.Label();
            this.ProxyExceptionsTB = new System.Windows.Forms.TextBox();
            this.DecryptHTTPSCB = new System.Windows.Forms.CheckBox();
            this.UseMachineStoreCB = new System.Windows.Forms.CheckBox();
            this.HideBlockedCB = new System.Windows.Forms.CheckBox();
            this.AutoCaptureStartCB = new System.Windows.Forms.CheckBox();
            this.UseAlternativeProxyCB = new System.Windows.Forms.CheckBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.AllowRemoteCB = new System.Windows.Forms.CheckBox();
            this.ParseRulesCaptureCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ProxyExceptionsLbl
            // 
            this.ProxyExceptionsLbl.AutoSize = true;
            this.ProxyExceptionsLbl.Location = new System.Drawing.Point(12, 9);
            this.ProxyExceptionsLbl.Name = "ProxyExceptionsLbl";
            this.ProxyExceptionsLbl.Size = new System.Drawing.Size(88, 13);
            this.ProxyExceptionsLbl.TabIndex = 0;
            this.ProxyExceptionsLbl.Text = "Proxy Exceptions";
            // 
            // ProxyExceptionsTB
            // 
            this.ProxyExceptionsTB.Location = new System.Drawing.Point(106, 6);
            this.ProxyExceptionsTB.Name = "ProxyExceptionsTB";
            this.ProxyExceptionsTB.Size = new System.Drawing.Size(325, 20);
            this.ProxyExceptionsTB.TabIndex = 1;
            // 
            // DecryptHTTPSCB
            // 
            this.DecryptHTTPSCB.AutoSize = true;
            this.DecryptHTTPSCB.Location = new System.Drawing.Point(15, 36);
            this.DecryptHTTPSCB.Name = "DecryptHTTPSCB";
            this.DecryptHTTPSCB.Size = new System.Drawing.Size(102, 17);
            this.DecryptHTTPSCB.TabIndex = 2;
            this.DecryptHTTPSCB.Text = "Decrypt HTTPS";
            this.DecryptHTTPSCB.UseVisualStyleBackColor = true;
            // 
            // UseMachineStoreCB
            // 
            this.UseMachineStoreCB.AutoSize = true;
            this.UseMachineStoreCB.Location = new System.Drawing.Point(125, 36);
            this.UseMachineStoreCB.Name = "UseMachineStoreCB";
            this.UseMachineStoreCB.Size = new System.Drawing.Size(117, 17);
            this.UseMachineStoreCB.TabIndex = 3;
            this.UseMachineStoreCB.Text = "Use Machine Store";
            this.UseMachineStoreCB.UseVisualStyleBackColor = true;
            // 
            // HideBlockedCB
            // 
            this.HideBlockedCB.AutoSize = true;
            this.HideBlockedCB.Location = new System.Drawing.Point(15, 59);
            this.HideBlockedCB.Name = "HideBlockedCB";
            this.HideBlockedCB.Size = new System.Drawing.Size(148, 17);
            this.HideBlockedCB.TabIndex = 4;
            this.HideBlockedCB.Text = "Hide Blocked Hosts Links";
            this.HideBlockedCB.UseVisualStyleBackColor = true;
            // 
            // AutoCaptureStartCB
            // 
            this.AutoCaptureStartCB.AutoSize = true;
            this.AutoCaptureStartCB.Location = new System.Drawing.Point(15, 82);
            this.AutoCaptureStartCB.Name = "AutoCaptureStartCB";
            this.AutoCaptureStartCB.Size = new System.Drawing.Size(128, 17);
            this.AutoCaptureStartCB.TabIndex = 5;
            this.AutoCaptureStartCB.Text = "Auto Capture on Start";
            this.AutoCaptureStartCB.UseVisualStyleBackColor = true;
            // 
            // UseAlternativeProxyCB
            // 
            this.UseAlternativeProxyCB.AutoSize = true;
            this.UseAlternativeProxyCB.Enabled = false;
            this.UseAlternativeProxyCB.Location = new System.Drawing.Point(15, 151);
            this.UseAlternativeProxyCB.Name = "UseAlternativeProxyCB";
            this.UseAlternativeProxyCB.Size = new System.Drawing.Size(127, 17);
            this.UseAlternativeProxyCB.TabIndex = 7;
            this.UseAlternativeProxyCB.Text = "Use Alternative Proxy";
            this.UseAlternativeProxyCB.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(356, 183);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(275, 183);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 8;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // AllowRemoteCB
            // 
            this.AllowRemoteCB.AutoSize = true;
            this.AllowRemoteCB.Location = new System.Drawing.Point(15, 105);
            this.AllowRemoteCB.Name = "AllowRemoteCB";
            this.AllowRemoteCB.Size = new System.Drawing.Size(153, 17);
            this.AllowRemoteCB.TabIndex = 6;
            this.AllowRemoteCB.Text = "Allow Remote Connections";
            this.AllowRemoteCB.UseVisualStyleBackColor = true;
            // 
            // ParseRulesCaptureCB
            // 
            this.ParseRulesCaptureCB.AutoSize = true;
            this.ParseRulesCaptureCB.Location = new System.Drawing.Point(15, 128);
            this.ParseRulesCaptureCB.Name = "ParseRulesCaptureCB";
            this.ParseRulesCaptureCB.Size = new System.Drawing.Size(138, 17);
            this.ParseRulesCaptureCB.TabIndex = 10;
            this.ParseRulesCaptureCB.Text = "Parse Rules on Capture";
            this.ParseRulesCaptureCB.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 219);
            this.Controls.Add(this.ParseRulesCaptureCB);
            this.Controls.Add(this.AllowRemoteCB);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.DecryptHTTPSCB);
            this.Controls.Add(this.UseAlternativeProxyCB);
            this.Controls.Add(this.UseMachineStoreCB);
            this.Controls.Add(this.HideBlockedCB);
            this.Controls.Add(this.ProxyExceptionsTB);
            this.Controls.Add(this.ProxyExceptionsLbl);
            this.Controls.Add(this.AutoCaptureStartCB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ProxyExceptionsLbl;
        private System.Windows.Forms.TextBox ProxyExceptionsTB;
        private System.Windows.Forms.CheckBox DecryptHTTPSCB;
        private System.Windows.Forms.CheckBox UseMachineStoreCB;
        private System.Windows.Forms.CheckBox HideBlockedCB;
        private System.Windows.Forms.CheckBox AutoCaptureStartCB;
        private System.Windows.Forms.CheckBox UseAlternativeProxyCB;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.CheckBox AllowRemoteCB;
        private System.Windows.Forms.CheckBox ParseRulesCaptureCB;
    }
}
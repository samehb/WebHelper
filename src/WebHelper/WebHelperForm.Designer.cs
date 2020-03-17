namespace WebHelper
{
    partial class WebHelperForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebHelperForm));
            this.StartStopCapture = new System.Windows.Forms.Button();
            this.FilterText = new System.Windows.Forms.TextBox();
            this.LinksGrid = new System.Windows.Forms.DataGridView();
            this.Link = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserAgent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AppNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ClearBtn = new System.Windows.Forms.Button();
            this.SettingsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LinksGrid)).BeginInit();
            this.TrayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartStopCapture
            // 
            this.StartStopCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartStopCapture.Location = new System.Drawing.Point(723, 12);
            this.StartStopCapture.Name = "StartStopCapture";
            this.StartStopCapture.Size = new System.Drawing.Size(91, 23);
            this.StartStopCapture.TabIndex = 0;
            this.StartStopCapture.Text = "Start";
            this.StartStopCapture.UseVisualStyleBackColor = true;
            this.StartStopCapture.Click += new System.EventHandler(this.StartStopCapture_Click);
            // 
            // FilterText
            // 
            this.FilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterText.Location = new System.Drawing.Point(12, 12);
            this.FilterText.Name = "FilterText";
            this.FilterText.Size = new System.Drawing.Size(705, 20);
            this.FilterText.TabIndex = 3;
            // 
            // LinksGrid
            // 
            this.LinksGrid.AllowUserToAddRows = false;
            this.LinksGrid.AllowUserToDeleteRows = false;
            this.LinksGrid.AllowUserToResizeRows = false;
            this.LinksGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinksGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.LinksGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.LinksGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LinksGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.LinksGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LinksGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.LinksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LinksGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Link,
            this.Method,
            this.UserAgent,
            this.Process,
            this.Action});
            this.LinksGrid.Location = new System.Drawing.Point(12, 41);
            this.LinksGrid.Name = "LinksGrid";
            this.LinksGrid.ReadOnly = true;
            this.LinksGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LinksGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.LinksGrid.RowHeadersVisible = false;
            this.LinksGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LinksGrid.Size = new System.Drawing.Size(987, 202);
            this.LinksGrid.TabIndex = 5;
            this.LinksGrid.TabStop = false;
            // 
            // Link
            // 
            this.Link.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Link.DefaultCellStyle = dataGridViewCellStyle2;
            this.Link.FillWeight = 50F;
            this.Link.HeaderText = "Link";
            this.Link.Name = "Link";
            this.Link.ReadOnly = true;
            this.Link.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Method
            // 
            this.Method.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Method.DefaultCellStyle = dataGridViewCellStyle3;
            this.Method.FillWeight = 12.5F;
            this.Method.HeaderText = "Method";
            this.Method.Name = "Method";
            this.Method.ReadOnly = true;
            this.Method.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UserAgent
            // 
            this.UserAgent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UserAgent.DefaultCellStyle = dataGridViewCellStyle4;
            this.UserAgent.FillWeight = 12.5F;
            this.UserAgent.HeaderText = "User Agent";
            this.UserAgent.Name = "UserAgent";
            this.UserAgent.ReadOnly = true;
            this.UserAgent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Process
            // 
            this.Process.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Process.DefaultCellStyle = dataGridViewCellStyle5;
            this.Process.FillWeight = 12.5F;
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Action.DefaultCellStyle = dataGridViewCellStyle6;
            this.Action.FillWeight = 12.5F;
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TrayMenuStrip
            // 
            this.TrayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.TrayMenuStrip.Name = "TrayMenuStrip";
            this.TrayMenuStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // AppNotifyIcon
            // 
            this.AppNotifyIcon.ContextMenuStrip = this.TrayMenuStrip;
            this.AppNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("AppNotifyIcon.Icon")));
            this.AppNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AppNotifyIcon_MouseClick);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearBtn.Location = new System.Drawing.Point(820, 12);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(91, 23);
            this.ClearBtn.TabIndex = 1;
            this.ClearBtn.Text = "Clear List";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsBtn.Location = new System.Drawing.Point(917, 12);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Size = new System.Drawing.Size(82, 23);
            this.SettingsBtn.TabIndex = 2;
            this.SettingsBtn.Text = "Settings";
            this.SettingsBtn.UseVisualStyleBackColor = true;
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // WebHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 262);
            this.Controls.Add(this.SettingsBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.LinksGrid);
            this.Controls.Add(this.FilterText);
            this.Controls.Add(this.StartStopCapture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1027, 300);
            this.Name = "WebHelperForm";
            this.ShowInTaskbar = false;
            this.Text = "WebHelper";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebHelperForm_FormClosing);
            this.Shown += new System.EventHandler(this.WebHelperForm_Shown);
            this.Resize += new System.EventHandler(this.WebHelperForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.LinksGrid)).EndInit();
            this.TrayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartStopCapture;
        private System.Windows.Forms.TextBox FilterText;
        private System.Windows.Forms.DataGridView LinksGrid;
        private System.Windows.Forms.ContextMenuStrip TrayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon AppNotifyIcon;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Link;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAgent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.Button SettingsBtn;
    }
}


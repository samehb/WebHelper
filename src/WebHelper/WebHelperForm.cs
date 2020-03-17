using System;
using System.Windows.Forms;
using WebHelper.Helper;
using WebHelper.Interface;
using WebHelper.Model;
//using WebHelper.Network.Fiddler;
using WebHelper.Network.Titanium;
using WebHelper.Util;

namespace WebHelper
{
    public partial class WebHelperForm : Form
    {
        private SettingsManager Settings;
        private IWebHelperProxy WebProxy;
        public delegate void AsyncMethodCaller(Information information);

        public WebHelperForm()
        {
            InitializeComponent();
            Settings = new SettingsManager(Application.StartupPath);
        }

        private void WebHelperForm_Shown(object sender, EventArgs e)
        {
            if (Settings.IsAutoCaptureMinimized())
                StartStopCapture.PerformClick();
            else
                ShowWebHelperForm();
        }

        private void WebHelperForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                AppNotifyIcon.Visible = true;
            }
        }

        private void WebHelperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (StartStopCapture.Text == "Stop")
                WebProxy.Stop();
        }

        private void AddText(Information information)
        {
            LinksGrid.Rows.Add(information.Url, information.Method, information.Agent, information.Program, information.Action);
            LinksGrid.ClearSelection();
        }

        public void UpdateGridView(Information information)
        {
            LinksGrid.BeginInvoke(new AsyncMethodCaller(AddText), information);
        }

        private void StartStopCapture_Click(object sender, EventArgs e)
        {
            if (StartStopCapture.Text == "Start")
                StartCapture();
            else
                StopCapture();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            LinksGrid.Rows.Clear();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(Settings);
            settingsForm.ShowDialog();
        }

        private void StartCapture()
        {
            StartStopCapture.Enabled = false;
            SettingsBtn.Enabled = false;
            FilterText.Enabled = false;

            LinksGrid.Rows.Clear();

            // If the option to parse rules on clicking "Start" capture is enabled, do it. This option allows users to modify and reload the rules after stopping the 
            // capture, without having to restart the program.
            if (Settings.IsParseRulesCapture())
                Settings.ParseRules();

            ProxyUtils.AddToHostsFile("myownwebhelper.com");

            WebProxy = new TitaniumProxy(Settings, this);

            WebProxy.Start(FilterText.Text);

            ProxyUtils.EnableProxy("myownwebhelper.com", Settings.GetProxyExceptions());

            StartStopCapture.Text = "Stop";

            StartStopCapture.Enabled = true;
        }

        private void StopCapture()
        {
            StartStopCapture.Enabled = false;
            WebProxy.Stop();
            ProxyUtils.DisableProxy();
            StartStopCapture.Text = "Start";
            StartStopCapture.Enabled = true;
            SettingsBtn.Enabled = true;
            FilterText.Enabled = true;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWebHelperForm();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AppNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ShowWebHelperForm();
        }

        private void ShowWebHelperForm()
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            AppNotifyIcon.Visible = false;
        }
    }
}

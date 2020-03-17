using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebHelper.Helper;
using WebHelper.Util;

namespace WebHelper
{
    public partial class SettingsForm : Form
    {
        private SettingsManager Settings;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(SettingsManager settings)
        {
            InitializeComponent();
            Settings = settings;

            ProxyExceptionsTB.Text = settings.GetProxyExceptions().Replace("<-loopback>;", "");
            ProxyExceptionsTB.SelectionStart = 0;

            DecryptHTTPSCB.Checked = settings.IsDecryptHTTPS();
            UseMachineStoreCB.Checked = settings.IsUseMachineStore();
            HideBlockedCB.Checked = settings.IsHideBlockedEnabled();
            AutoCaptureStartCB.Checked = settings.IsAutoCaptureMinimized();
            AllowRemoteCB.Checked = settings.IsAllowRemote();
            ParseRulesCaptureCB.Checked = settings.IsParseRulesCapture();
            UseAlternativeProxyCB.Checked = settings.IsUseAlternativeProxy();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Settings.SaveSettings(ProxyExceptionsTB.Text, DecryptHTTPSCB.Checked, UseMachineStoreCB.Checked, HideBlockedCB.Checked, AutoCaptureStartCB.Checked, AllowRemoteCB.Checked, ParseRulesCaptureCB.Checked, UseAlternativeProxyCB.Checked);
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

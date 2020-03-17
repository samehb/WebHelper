using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WebHelper.Util
{
    public static class ProxyUtils
    {
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;

        // Adds an alias to the proxy into the hosts file.
        // The reason we have created this method is that some programs check the proxy, and when they find it to be the localhost, they counter it.
        public static void AddToHostsFile(string proxyAlias)
        {
            string pathPart = (Environment.OSVersion.Platform == PlatformID.Win32NT) ? "system32\\drivers\\etc\\hosts" : "hosts";
            string hostFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), pathPart);

            string line = "\r\n127.0.0.1 " + proxyAlias;
            if (!File.ReadAllText(hostFile).Contains(line))
                File.AppendAllText(hostFile, line);
        }

        // Enable the proxy on the system once we start it. We allow the user to provide exceptions if he wants.
        public static void EnableProxy(string proxyAlias, string exceptions)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            //registry.SetValue("ProxyEnable", 1); Not needed.
            string port = registry.GetValue("ProxyServer").ToString().Split(':')[2];
            registry.SetValue("ProxyServer", "http=" + proxyAlias + ":" + port + ";https=" + proxyAlias + ":" + port);
            registry.SetValue("ProxyOverride", exceptions);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        // Disable the proxy on the system once we stop it.
        public static void DisableProxy()
        {
            RegistryKey registry2 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry2.SetValue("ProxyEnable", 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
    }
}

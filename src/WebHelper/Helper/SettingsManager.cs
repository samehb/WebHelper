using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace WebHelper.Helper
{
    // The class is responsible for handling the program settings including proxy rules.
    public class SettingsManager
    {
        private bool DecryptHTTPSEnabled = false;
        private bool UseMachineStoreEnabled = false;
        private bool HideBlockedEnabled = false;
        private bool AutoCaptureEnabled = false;
        private bool AllowRemoteEnabled = false;
        private bool ParseRulesCapture = false;
        private bool UseAlternativeProxyEnabled = false;

        private string ProxyExceptions = "";

        private string RulesPath;
        private string RulesFilesPath;
        private string SettingsPath;

        private Dictionary<string, string> ResponseEntries = new Dictionary<string, string>(); // Exact url match.
        private Dictionary<string, string> ResponseWildCardEntries = new Dictionary<string, string>(); // Matching urls starting with those matches (e.g. example.com/test*)
        private Dictionary<string, string> ResponseRegexEntries = new Dictionary<string, string>(); // Regex matches.

        private List<string> BlockingEntries = new List<string>();

        private Dictionary<string, string> ForwardEntries = new Dictionary<string, string>(); // Exact url match.
        private Dictionary<string, string> ForwardWildCardEntries = new Dictionary<string, string>(); // Matching urls starting with those matches (e.g. example.com/test*)
        private Dictionary<string, string> ForwardRegexEntries = new Dictionary<string, string>();

        private Dictionary<string, string> DownloadEntries = new Dictionary<string, string>(); // Exact url match.
        private Dictionary<string, string> DownloadWildCardEntries = new Dictionary<string, string>(); // Matching urls starting with those matches (e.g. example.com/test*)
        private Dictionary<string, string> DownloadRegexEntries = new Dictionary<string, string>(); // Regex matches.

        public SettingsManager(string programPath)
        {
            IntilizeSettings(programPath);
            ParseSettings();
            ParseRules();
        }

        private void IntilizeSettings(string programPath)
        {
            RulesPath = programPath + "\\rules\\";
            RulesFilesPath = programPath + "\\rules\\files\\";
            SettingsPath = programPath + "\\settings.ini";

            Directory.CreateDirectory(RulesPath);
            Directory.CreateDirectory(RulesFilesPath);

            if (!File.Exists(SettingsPath))
                SaveSettings("", true, false, true, false, false, false, false);
        }

        // Parse the main settings for the program.
        private void ParseSettings()
        {
            string[] settings = File.Exists(SettingsPath) ? File.ReadAllText(SettingsPath).Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;

            if (settings != null)
            {
                ProxyExceptions = "<-loopback>;";
                if (settings[0].Contains("proxyexceptions="))
                    ProxyExceptions += settings[0].Split('=')[1];

                DecryptHTTPSEnabled = settings[1].Contains("descrypthttps=true");
                UseMachineStoreEnabled = settings[2].Contains("usemachinestore=true");
                HideBlockedEnabled = settings[3].Contains("hideblocked=true");
                AutoCaptureEnabled = settings[4].Contains("autocapture=true");
                AllowRemoteEnabled = settings[5].Contains("allowremote=true");
                ParseRulesCapture = settings[6].Contains("parserulescapture=true");
                UseAlternativeProxyEnabled = settings[7].Contains("usealternativeproxy=true");
            }
        }

        // Parses the rules that the proxies use, for manipulations.
        public void ParseRules()
        {
            string[] responseentries = File.Exists(RulesPath + "respond.txt") ? File.ReadAllText(RulesPath + "respond.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;
            string[] responseregexentries = File.Exists(RulesPath + "respondregex.txt") ? File.ReadAllText(RulesPath + "respondregex.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;
            string[] blockedentries = File.Exists(RulesPath + "block.txt") ? File.ReadAllText(RulesPath + "block.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;
            string[] forwardentries = File.Exists(RulesPath + "forward.txt") ? File.ReadAllText(RulesPath + "forward.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;
            string[] forwardregexentries = File.Exists(RulesPath + "forwardregex.txt") ? File.ReadAllText(RulesPath + "forwardregex.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;
            string[] downloadentries = File.Exists(RulesPath + "download.txt") ? File.ReadAllText(RulesPath + "download.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;
            string[] downloadentriesregex = File.Exists(RulesPath + "downloadregex.txt") ? File.ReadAllText(RulesPath + "downloadregex.txt").Split(new[] { Environment.NewLine }, StringSplitOptions.None) : null;

            ClearRules();

            PopulateMainDictionaries(responseentries, ResponseEntries, ResponseWildCardEntries);
            PopulateOtherDictionary(responseregexentries, ResponseRegexEntries);

            PopulateMainList(blockedentries, BlockingEntries);

            PopulateMainDictionaries(forwardentries, ForwardEntries, ForwardWildCardEntries);
            PopulateOtherDictionary(forwardregexentries, ForwardRegexEntries);

            PopulateMainDictionaries(downloadentries, DownloadEntries, DownloadWildCardEntries);
            PopulateOtherDictionary(downloadentriesregex, DownloadRegexEntries);
        }

        // Needed for the parsing of rules on capture option.
        private void ClearRules()
        {
            ResponseEntries.Clear();
            ResponseWildCardEntries.Clear();
            ResponseRegexEntries.Clear();

            BlockingEntries.Clear();

            ForwardEntries.Clear();
            ForwardWildCardEntries.Clear();
            ForwardRegexEntries.Clear();

            DownloadEntries.Clear();
            DownloadWildCardEntries.Clear();
            DownloadRegexEntries.Clear();
        }

        // In case a user ignore the documentation, and decides to write rules in http?://* format.
        private string CleanPath(string path)
        {
            // The url could have http?:// within it (like redirections). That is why we use StartsWith(). We only want to skip http? from start.
            if(path.StartsWith("https://") || path.StartsWith("http://"))
            {
                Match match = Regex.Match(path, @"http.?:\/\/(.+)", RegexOptions.IgnoreCase);
                if (match.Success)
                    return match.Groups[1].Value;
            }

            return path;
        }

        // Fill the dictionaries.
        private void PopulateMainDictionaries(string[] entries, Dictionary<string, string> Entries, Dictionary<string, string> WildCardEntries)
        {
            if (entries != null && entries.Length > 0)
            {
                foreach (string entry in entries)
                {
                    if (!entry.StartsWith(";") && entry.Contains(";") && entry.Length > 6) // Skip comments (lines starting with ;) and incomplete rules.
                    {
                        string link = CleanPath(entry.Split(';')[0].ToLower());
                        string target = entry.Split(';')[1];

                        if (link.EndsWith("***"))
                        {
                            string s = link.Replace("***", "");
                            if (!WildCardEntries.ContainsKey(s))
                                WildCardEntries.Add(s, target);
                        }
                        else
                        {
                            if (!Entries.ContainsKey(link))
                                Entries.Add(link, target);
                        }
                    }
                }
            }
        }

        // Populate regex dictionaries.
        private void PopulateOtherDictionary(string[] entries, Dictionary<string, string> Entries)
        {
            if (entries != null && entries.Length > 0)
            {
                foreach (string entry in entries)
                {
                    if (!entry.StartsWith(";") && entry.Contains(";") && entry.Length > 6)
                    {
                        string link = CleanPath(entry.Split(';')[0].ToLower());
                        string target = entry.Split(';')[1];
                        if (IsRegexValid(link) && !Entries.ContainsKey(link))
                            Entries.Add(link, target);
                    }
                }
            }
        }

        // Mainly for the blocking list.
        private void PopulateMainList(string[] entries, List<string> Entries)
        {
            if (entries != null && entries.Length > 0)
            {
                foreach (string entry in entries)
                {
                    if (!entry.StartsWith(";") && !Entries.Contains(entry) && entry.Length > 2)
                        Entries.Add(CleanPath(entry));
                }
            }
        }

        // Some users may write invalid regex patterns. That is why we need to verify them.
        private static bool IsRegexValid(string pattern)
        {
            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        // Save settings into the disk, and set/refresh globals.
        public void SaveSettings(string proxyexcptions, bool decrypthttps, bool usemachinestore, bool hideblocked, bool autocapture, bool allowremote, bool parserulescapture, bool usealternativeproxy)
        {
            ProxyExceptions = "<-loopback>;" + proxyexcptions;
            DecryptHTTPSEnabled = decrypthttps;
            UseMachineStoreEnabled = usemachinestore;
            HideBlockedEnabled = hideblocked;
            AutoCaptureEnabled = autocapture;
            AllowRemoteEnabled = allowremote;
            ParseRulesCapture = parserulescapture;
            UseAlternativeProxyEnabled = usealternativeproxy;

            List<string> settings = new List<string>();

            settings.Add("proxyexceptions=" + proxyexcptions);
            settings.Add("descrypthttps=" + decrypthttps.ToString().ToLower());
            settings.Add("usemachinestore=" + usemachinestore.ToString().ToLower());
            settings.Add("hideblocked=" + hideblocked.ToString().ToLower());
            settings.Add("autocapture=" + autocapture.ToString().ToLower());
            settings.Add("allowremote=" + allowremote.ToString().ToLower());
            settings.Add("parserulescapture=" + parserulescapture.ToString().ToLower());
            settings.Add("usealternativeproxy=" + usealternativeproxy.ToString().ToLower());

            File.WriteAllLines(SettingsPath, settings);
        }

        public string GetProxyExceptions()
        {
            return ProxyExceptions;
        }

        public string GetRulesFilesPath()
        {
            return RulesFilesPath;
        }

        // Used to get a wildcard match for GetMatch().
        private string GetWildcard(string urlParamsNoProtocol, Dictionary<string, string> WildCardEntries)
        {
            foreach (KeyValuePair<string, string> entry in WildCardEntries)
            {
                if (urlParamsNoProtocol.StartsWith(entry.Key))
                    return entry.Value;
            }

            return null;
        }

        // Used to get regex match for GetMatch().
        private string GetRegex(string urlParamsNoProtocol, Dictionary<string, string> RegexEntries)
        {
            foreach (KeyValuePair<string, string> entry in RegexEntries)
            {
                Match match = Regex.Match(urlParamsNoProtocol, entry.Key, RegexOptions.IgnoreCase);

                if (match.Success)
                    return entry.Value;
            }

            return null;
        }

        // Acquire matches for rules by checking exact rules, then wildcard, and finally regex rules. 
        // Exact rules have priority over wildcard rules. Wildcard rules have priority than regex rules.
        private string GetMatch(string urlParamsNoProtocol, Dictionary<string, string> Entries, Dictionary<string, string> WildCardEntries, Dictionary<string, string> RegexEntries)
        {
            if (Entries.Count > 0 && Entries.ContainsKey(urlParamsNoProtocol))
                return Entries[urlParamsNoProtocol];

            if (WildCardEntries.Count > 0)
            {
                string wildresponse = GetWildcard(urlParamsNoProtocol, WildCardEntries);
                if (wildresponse != null)
                    return wildresponse;
            }

            if (RegexEntries.Count > 0)
            {
                string regexresponse = GetRegex(urlParamsNoProtocol, RegexEntries);
                if (regexresponse != null)
                    return regexresponse;
            }

            return null;
        }

        // Attempt to get a response for a url.
        public string GetResponse(string urlParamsNoProtocol)
        {
            return GetMatch(urlParamsNoProtocol, ResponseEntries, ResponseWildCardEntries, ResponseRegexEntries);
        }

        // Attempt to get a forward for a url.
        public string GetForward(string urlParamsNoProtocol)
        {
            return GetMatch(urlParamsNoProtocol, ForwardEntries, ForwardWildCardEntries, ForwardRegexEntries);
        }

        // Attempt to get a download match for a url.
        public string GetDownload(string urlParamsNoProtocol)
        {
            return GetMatch(urlParamsNoProtocol, DownloadEntries, DownloadWildCardEntries, DownloadRegexEntries);
        }

        public bool IsHostBlocked(string host)
        {
            return BlockingEntries.Count > 0 && BlockingEntries.Contains(host);
        }

        public bool IsDecryptHTTPS()
        {
            return DecryptHTTPSEnabled;
        }

        public bool IsUseMachineStore()
        {
            return UseMachineStoreEnabled;
        }

        public bool IsHideBlockedEnabled()
        {
            return HideBlockedEnabled;
        }

        public bool IsAutoCaptureMinimized()
        {
            return AutoCaptureEnabled;
        }

        public bool IsAllowRemote()
        {
            return AllowRemoteEnabled;
        }

        public bool IsParseRulesCapture()
        {
            return ParseRulesCapture;
        }

        public bool IsUseAlternativeProxy()
        {
            return UseAlternativeProxyEnabled;
        }
    }
}

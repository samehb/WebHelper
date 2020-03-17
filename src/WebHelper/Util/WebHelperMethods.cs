using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebHelper.Util
{
    // This class is for handling "method=*" on responses, forwards, and downloads. We have created this class to make proxy classes (e.g. TitanimumProxy) simple, 
    // without filling them with checks for methods. If a text response, for example, is not enough for your tests, you can create your own method in here and call 
    // it from ProcessResponse().
    class WebHelperMethods
    {
        // Forward methods. They, return the new url for forwards.
        public static string ProcessForward(string method, string url, string urlParamsNoProtocol)
        {
            method = method.ToLower();

            //switch (method)
            //{
            //
            //}

            return null;
        }

        // Response methods. They return a response.
        public static string ProcessResponse(string method, string url, string urlParamsNoProtocol)
        {
            method = method.ToLower();

            //switch (method)
            //{
            //    //case "ip":
            //    //    return GetExternalIP();
            //    //Add more.
            //}

            return null;
        }

        // Download methods. They allow you to process the files for download as you please.
        public static void ProcessDownload(string method, string host, string url, string urlParams, string requestMethod, string useragent, string program, string referer, byte[] responseBytes)
        {
            method = method.ToLower();

            switch (method)
            {
                case "log":
                    LogLinks(urlParams, requestMethod, useragent, program, responseBytes);
                    break;
                case "save":
                    SaveFiles(host, url, responseBytes);
                    break;
            }
        }

        public static string GetExternalIP()
        {
            try
            {
                WebClient client = new WebClient();
                string content = client.DownloadString(""); // Use your own url.

                string ipfilter = @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?";
                Match ipmatch = Regex.Match(content, ipfilter);

                if (ipmatch.Success)
                    return ipmatch.Value;
            }
            catch (WebException)
            {

            }

            return "";
        }

        // Simple method to log links and information about them (including result), into logs.txt
        public static void LogLinks(string url, string requestMethod, string useragent, string program, byte[] responseBytes)
        {
            List<string> text = new List<string>();
            text.Add("Request: " + url.Replace(Environment.NewLine, "*newline*") + ";" + requestMethod + ";" + useragent + ";" + program + Environment.NewLine);
            text.Add("Result: " + Encoding.UTF8.GetString(responseBytes) + Environment.NewLine);
            text.Add("*********************************************************************************************************************");
            text.Add("*********************************************************************************************************************" + Environment.NewLine);

            FileUtils.WriteLines(Application.StartupPath + "\\logs.txt", text);
        }

        // Save files into the disk while maintaining their paths. This method is not fully revised.
        public static void SaveFiles(string host, string url, byte[] responseBodyBytes)
        {
            string fileName = "index.htm"; // Default filename.
            string folder = Application.StartupPath + @"\download\" + host + @"\";

            Directory.CreateDirectory(folder);

            Match match = Regex.Match(url, @"[a-zA-Z]+:\/\/[^\/]+\/(.+)", RegexOptions.IgnoreCase); // match protocol://(*/*). (*/*) is the part we need.

            // We first try to match */*. If we cannot match it, that means the url is on root e.g. example.com or example.com/ In that case, the filename would be
            // index.htm and folder is the root folder (look above). In case we match it, that means we got something like example.com/(test), example.com/(t/test/)
            // example.com/(test/), example.com/(test/test/), example.com/(test/test/test) etc.
            if (match.Success)
            {
                string path = match.Groups[1].Value.Replace(@"/", @"\"); // Replace / with \ to form a valid path.

                if (path.EndsWith(@"\")) // We see if the url ends with \ making it a path without a file e.g. example.com/(test/), example.com/(test/test/) etc.
                {
                    folder += path; // fileName remains the same.
                }
                else // Path with file.
                {
                    fileName = path.Substring(path.LastIndexOf(@"\") + 1);
                    folder += path.Remove(path.LastIndexOf(@"\") + 1);
                }

                if (fileName.Contains("?")) // Some links may have "?" in their files.
                    fileName = fileName.Split('?')[0];

                Directory.CreateDirectory(folder);
            }

            //File.WriteAllBytes(folder + fileName, responseBodyBytes);
            FileUtils.WriteBytes(folder + fileName, responseBodyBytes);
        }
    }
}

//using System;
//using System.IO;
//using System.Text;
//using Fiddler;
//using WebHelper.Helper;
//using WebHelper.Interface;
//using WebHelper.Model;
//using WebHelper.Util;

//namespace WebHelper.Network.Fiddler
//{
//    // The proxy class is the heart of the program that handles all requests and modifications.
//    class FiddlerProxy : IWebHelperProxy
//    {
//        private SettingsManager Settings;
//        private string FilterUrl; // Determines whether or not we need to display the link/information or not.
//        private WebHelperForm MainForm;

//        public FiddlerProxy(SettingsManager settings, WebHelperForm form)
//        {
//            Settings = settings;
//            MainForm = form; // Only used for passing the information, to be displayed on the gridview.
//        }

//        // Starts the capture.
//        public void Start(string filter)
//        {
//            FilterUrl = filter;

//            // If the user wants to allow/process HTTPS links, we handle it here.
//            if (Settings.IsDecryptHTTPS())
//                FiddlerCertManager.InstallCertificate(Settings.IsUseMachineStore());

//            FiddlerApplication.BeforeRequest += FiddlerProxy_BeforeRequest;
//            FiddlerApplication.BeforeResponse += FiddlerProxy_BeforeResponse;
//            FiddlerApplication.Startup(55555, true, Settings.IsDecryptHTTPS(), Settings.IsAllowRemote());
//        }

//        // Stops the capture.
//        public void Stop()
//        {
//            if (FiddlerApplication.IsStarted())
//            {
//                if (Settings.IsDecryptHTTPS())
//                    FiddlerCertManager.UninstallCertificate(); // Remove the certificates on stopping.

//                FiddlerApplication.Shutdown();
//            }
//        }

//        // This event fires when the client request is received by the proxy. If you want to modify the requests / play the man in the middle, this is the spot
//        // to handle it.
//        private void FiddlerProxy_BeforeRequest(Session oSession)
//        {
//            bool hasBlock;
//            bool hasForward;
//            bool hasResponse;

//            string forward = null;
//            string response = null;
//            string action;

//            string host = oSession.host.ToLower();
//            string url = oSession.fullUrl;

//            string requestMethod = oSession.RequestMethod;
//            string requestMethodMod = requestMethod == "POST" ? "||" : "?"; // The url may have both GET and POST requests on it. We support both requests by handling the requests based on the method. See respond.txt for more information.

//            string parameters = oSession.GetRequestBodyAsString();
//            string urlParams = parameters != "" ? url + requestMethodMod + parameters : url;
//            string urlParamsNoProtocol = urlParams.ToLower().Replace("https://", "").Replace("http://", "");

//            string userAgent = oSession.RequestHeaders["User-Agent"]; // Sometimes the user agent may not be available.
//            userAgent = userAgent != null ? userAgent.Replace("User-Agent: ", "") : "N/A";

//            string program = oSession["X-PROCESSINFO"] != null ? oSession["X-PROCESSINFO"].Split(':')[0] + ".exe" : "N/A"; // Sometimes the program name may not be available. This happens when a remote device sends requests to the proxy.

//            bool reportMessage = FilterUrl == "" || urlParams.Contains(FilterUrl);

//            CONFIG.IgnoreServerCertErrors = true; // Ignore certificates errors.
//            if (!Settings.IsDecryptHTTPS())
//                oSession["x-no-decrypt"] = "do not care."; // Skip traffic decryption, when it is not needed.

//            // Blocking has priority over forwards. Forwards have priority over responses. Responses have priority over the rest.
//            hasBlock = Settings.IsHostBlocked(host);
//            if(!hasBlock)
//            {
//                forward = Settings.GetForward(urlParamsNoProtocol);
//                if (forward == null)
//                    response = Settings.GetResponse(urlParamsNoProtocol);
//            }
//            hasForward = forward != null;
//            hasResponse = response != null;

//            // This is the main segment of the proxy where we handle request modifications. Blocking has priority over forwards. Forwards have priority over responses.
//            // Responses have priority over the rest. We use the action for reporting. We want the user to know what modification happened exactly.
//            if (hasBlock) 
//            {
//                action = HandleBlocking(oSession);
//                if (Settings.IsHideBlockedEnabled()) // Disable reporting if the user selected the option to hide blocked requests.
//                    reportMessage = false;
//            }
//            else if (hasForward)
//            {
//                action = HandleForward(url, urlParamsNoProtocol, forward, oSession);
//            }
//            else if (hasResponse)
//            {
//                action = HandleResponse(url, urlParamsNoProtocol, response, oSession);
//            }
//            else // In case we need to do something with those in the future.
//            {
//                action = "None";
//            }

//            // The reason for urlParams.Replace(Environment.NewLine, "*newline*") is that some urls include multiline parameters.
//            if (reportMessage)
//                MainForm.UpdateGridView(new Information(urlParams.Replace(Environment.NewLine, "*newline*"), requestMethod, userAgent, program, action));
//        }

//        // This event fires when the server response is received by the proxy. This is the spot, if you want to read the responose or download it.
//        private void FiddlerProxy_BeforeResponse(Session oSession)
//        {
//            string host = oSession.host.ToLower();
//            string url = oSession.fullUrl;

//            string requestMethod = oSession.RequestMethod;
//            string requestMethodMod = requestMethod == "POST" ? "||" : "?";

//            string parameters = oSession.GetRequestBodyAsString();
//            string urlParams = parameters != "" ? url + requestMethodMod + parameters : url;
//            string urlParamsNoProtocol = urlParams.ToLower().Replace("https://", "").Replace("http://", "");

//            string userAgent = oSession.RequestHeaders["User-Agent"];
//            userAgent = userAgent != null ? userAgent.Replace("User-Agent: ", "") : "N/A";

//            string referer = oSession.RequestHeaders["Referer"];

//            string program = oSession["X-PROCESSINFO"] != null ? oSession["X-PROCESSINFO"].Split(':')[0] + ".exe" : "";

//            CONFIG.IgnoreServerCertErrors = true;

//            oSession.oResponse.headers.Remove("Strict-Transport-Security"); // Some sites use HTTP Strict Transport Security (HSTS). You may be prevented from seeing Fiddler responses, on those sites, depending on the browser. This line helps with those sites but it requires a further step. This step requires you to edit some browser files.

//            if ((requestMethod == "GET" || requestMethod == "POST") && oSession.responseBodyBytes.Length > 0) // Using the commented condition may cause skipping of some files -> oSession.responseCode == 200 
//                HandleDownload(host, url, urlParams, urlParamsNoProtocol, requestMethod, userAgent, program, referer, oSession);
//        }

//        // Handle blocking of a host.
//        private string HandleBlocking(Session oSession)
//        {
//            oSession.LoadResponseFromStream(new MemoryStream(Encoding.UTF8.GetBytes("Request blocked based on block.txt rules.")), "");
//            return "Blocked";
//        }

//        // Handle forwarding a url to another.
//        private string HandleForward(string url, string urlParamsNoProtocol, string forward, Session oSession)
//        {
//            string action = "Redirected";

//            if (url.EndsWith(":443")) // Skip CONNECT requests.
//                return "None";

//            if (oSession.isHTTPS) // Force http on https requests. Since protocol is skipped on forward.txt, we assume all the target urls are http. Then, the target site can handle the transfer from http to https.
//                oSession.oRequest.headers.UriScheme = "http";

//            if (forward.StartsWith("method=")) // See WebHelperMethods for more information.
//            {
//                action = "Method (F)"; // Method Forwarding.
//                string method = forward.Split('=')[1];
//                oSession.fullUrl = WebHelperMethods.ProcessForward(method, url, urlParamsNoProtocol);
//            }
//            else // Normal forwards.
//            {
//                oSession.fullUrl = "http://" + forward;
//            }

//            return action;
//        }

//        // Handle response for a url. This is mainly where you play man in the middle. You can feed the requesting client the information you want.
//        private string HandleResponse(string url, string urlParamsNoProtocol, string response, Session oSession)
//        {
//            string action;

//            if (url.EndsWith(":443")) // Handle connects and reply with fake connects.
//            {
//                action = "Handshake";
//                if (oSession.HTTPMethodIs("CONNECT"))
//                    oSession.oFlags["X-ReplyWithTunnel"] = "HTTPS Tunnel";
//            }
//            else
//            {
//                if (response.StartsWith("file=")) // If you want to provide a response from a file, make sure the file is placd at rules/files folder.
//                {
//                    string filePath = Settings.GetRulesFilesPath() + response.Split('=')[1];
//                    if (File.Exists(filePath))
//                    {
//                        action = "File";
//                        oSession.LoadResponseFromFile(filePath);
//                    }
//                    else
//                        action = "File (Missing)";
//                }
//                else if (response.StartsWith("method=")) // See WebHelperMethods for more information.
//                {
//                    action = "Method (R)";
//                    string method = response.Split('=')[1];
//                    oSession.LoadResponseFromStream(new MemoryStream(Encoding.UTF8.GetBytes(WebHelperMethods.ProcessResponse(method, url, urlParamsNoProtocol))), "");
//                }
//                else // Normal text response from respond.txt.
//                {
//                    action = "Text";
//                    oSession.LoadResponseFromStream(new MemoryStream(Encoding.UTF8.GetBytes(response)), "");
//                }
//            }

//            return action;
//        }

//        // Handle the downloading of files.
//        private void HandleDownload(string host, string url, string urlParams, string urlParamsNoProtocol, string requestMethod, string userAgent, string program, string referer, Session oSession)
//        {
//            string download = Settings.GetDownload(urlParamsNoProtocol);

//            if (download != null)
//            {
//                //if (url.EndsWith(".htm") || url.EndsWith(".html") || url.EndsWith(".js"))
//                oSession.utilDecodeResponse();

//                string method = download.Split('=')[1];
//                WebHelperMethods.ProcessDownload(method, host, url, urlParams, requestMethod, userAgent, program, referer, oSession.responseBodyBytes);
//            }
//        }
//    }   
//}

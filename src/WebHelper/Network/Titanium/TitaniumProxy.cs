using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using WebHelper.Helper;
using WebHelper.Interface;
using WebHelper.Model;
using WebHelper.Util;

namespace WebHelper.Network.Titanium
{
    // The proxy class is the heart of the program that handles all requests and modifications.
    class TitaniumProxy : IWebHelperProxy
    {
        private SettingsManager Settings;
        private string FilterUrl; // Determines whether or not we need to display the link/information or not.
        private WebHelperForm MainForm;
        private ProxyServer TitaniumProxyServer;
        private ExplicitProxyEndPoint TitaniumExplicitEndPoint;

        public TitaniumProxy(SettingsManager settings, WebHelperForm form)
        {
            Settings = settings;
            MainForm = form; // Only used for passing the information, to be displayed on the gridview.
        }

        // Starts the capture.
        public void Start(string filter)
        {
            FilterUrl = filter;

            TitaniumProxyServer = new ProxyServer();

            // If the user wants to allow/process HTTPS links, we handle it here.
            if (Settings.IsDecryptHTTPS())
            {
                TitaniumProxyServer.CertificateManager.CreateRootCertificate(false);
                TitaniumProxyServer.CertificateManager.TrustRootCertificate(Settings.IsUseMachineStore());
                //TitaniumProxyServer.CertificateManager.TrustRootCertificateAsAdmin();
            }

            TitaniumProxyServer.BeforeRequest += TitaniumProxy_BeforeRequest;
            TitaniumProxyServer.BeforeResponse += TitaniumProxy_BeforeResponse;
            TitaniumProxyServer.ServerCertificateValidationCallback += TitaniumProxy_OnCertificateValidation;

            TitaniumExplicitEndPoint = new ExplicitProxyEndPoint(Settings.IsAllowRemote() ? IPAddress.Any : IPAddress.Parse("127.0.0.1"), 55558, Settings.IsDecryptHTTPS());

            TitaniumProxyServer.AddEndPoint(TitaniumExplicitEndPoint);
            TitaniumProxyServer.Start();

            TitaniumProxyServer.SetAsSystemHttpProxy(TitaniumExplicitEndPoint);
            TitaniumProxyServer.SetAsSystemHttpsProxy(TitaniumExplicitEndPoint);
        }

        // Stops the capture.
        public void Stop()
        {
            TitaniumProxyServer.CertificateManager.RemoveTrustedRootCertificate(Settings.IsUseMachineStore()); // Remove the cert on stopping.

            TitaniumProxyServer.BeforeRequest -= TitaniumProxy_BeforeRequest;
            TitaniumProxyServer.BeforeResponse -= TitaniumProxy_BeforeResponse;
            TitaniumProxyServer.ServerCertificateValidationCallback -= TitaniumProxy_OnCertificateValidation;
            TitaniumProxyServer.Stop();
        }

        // This event fires when the client request is received by the proxy. If you want to modify the requests / play the man in the middle, this is the spot
        // to handle it.
        private async Task TitaniumProxy_BeforeRequest(object sender, SessionEventArgs e)
        {
            bool hasBlock;
            bool hasForward;
            bool hasResponse;

            string forward = null;
            string response = null;
            string action;

            string host = e.HttpClient.Request.Host.ToLower();
            string url = e.HttpClient.Request.Url;

            string requestMethod = e.HttpClient.Request.Method;
            string requestMethodMod = requestMethod == "POST" ? "||" : "?"; // The url may have both GET and POST requests on it. We support both requests by handling the requests based on the method. See respond.txt for more information.

            string parameters = e.HttpClient.Request.HasBody ? await e.GetRequestBodyAsString() : "";
            string urlParams = parameters != "" ? url + requestMethodMod + parameters : url;
            string urlParamsNoProtocol = urlParams.ToLower().Replace("https://", "").Replace("http://", "");

            List<HttpHeader> userAgentHeader = e.HttpClient.Request.Headers.GetHeaders("User-Agent"); // Sometimes the user agent may not be available.
            string userAgent = userAgentHeader != null ? userAgentHeader[0].Value.Replace("User-Agent: ", "") : "N/A";

            string program = e.HttpClient.ProcessId.Value != -1 ? Process.GetProcessById(e.HttpClient.ProcessId.Value).ProcessName + ".exe" : "N/A"; // Sometimes the program name may not be available. If the ProcessId is == -1, that means it is not availble. This happens when a remote device sends requests to the proxy.

            bool reportMessage = FilterUrl == "" || urlParams.Contains(FilterUrl);

            // Blocking has priority over forwards. Forwards have priority over responses. Responses have priority over the rest.
            hasBlock = Settings.IsHostBlocked(host);
            if (!hasBlock)
            {
                forward = Settings.GetForward(urlParamsNoProtocol);
                if (forward == null)
                    response = Settings.GetResponse(urlParamsNoProtocol);
            }
            hasForward = forward != null;
            hasResponse = response != null;

            // This is the main segment of the proxy where we handle request modifications. Blocking has priority over forwards. Forwards have priority over responses.
            // Responses have priority over the rest. We use the action for reporting. We want the user to know what modification happened exactly.
            if (hasBlock)
            {
                action = HandleBlocking(e);
                if (Settings.IsHideBlockedEnabled()) // Disable reporting if the user selected the option to hide blocked requests.
                    reportMessage = false;
            }
            else if (hasForward)
            {
                action = HandleForward(url, urlParamsNoProtocol, forward, e);
            }
            else if (hasResponse)
            {
                action = HandleResponse(url, urlParamsNoProtocol, response, e);
            }
            else // In case we need to do something with those in the future.
            {
                action = "None";
            }

            // The reason for urlParams.Replace(Environment.NewLine, "*newline*") is that some urls include multiline parameters.
            if (reportMessage)
                MainForm.UpdateGridView(new Information(urlParams.Replace(Environment.NewLine, "*newline*"), requestMethod, userAgent, program, action));
        }

        // This event fires when the server response is received by the proxy. This is the spot, if you want to read the responose or download it.
        private async Task TitaniumProxy_BeforeResponse(object sender, SessionEventArgs e)
        {
            string host = e.HttpClient.Request.Host.ToLower();
            string url = e.HttpClient.Request.Url;

            string requestMethod = e.HttpClient.Request.Method;
            string requestMethodMod = requestMethod == "POST" ? "||" : "?";

            string parameters = e.HttpClient.Request.HasBody ? await e.GetRequestBodyAsString() : "";
            string urlParams = parameters != "" ? url + requestMethodMod + parameters : url;
            string urlParamsNoProtocol = urlParams.ToLower().Replace("https://", "").Replace("http://", "");

            List<HttpHeader> userAgentHeader = e.HttpClient.Request.Headers.GetHeaders("User-Agent");
            string userAgent = userAgentHeader != null ? userAgentHeader[0].Value.Replace("User-Agent: ", "") : "N/A";

            List<HttpHeader> refererHeader = e.HttpClient.Request.Headers.GetHeaders("Referer");
            string referer = refererHeader != null ? refererHeader[0].Value : "N/A";

            string program = e.HttpClient.ProcessId.Value != -1 ? Process.GetProcessById(e.HttpClient.ProcessId.Value).ProcessName + ".exe" : "N/A";

            if ((requestMethod == "GET" || requestMethod == "POST")) // Using the commented conditions may cause skipping of some files -> && e.HttpClient.Response.StatusCode == 200 && e.HttpClient.Response.ContentType != null
            {
                byte[] bodyBytes = await e.GetResponseBody(); // await e.GetResponseBodyAsString();

                if (bodyBytes.Length > 0)
                    HandleDownload(host, url, urlParams, urlParamsNoProtocol, requestMethod, userAgent, program, referer, bodyBytes);
            }
        }

        // We use this to force validity on all certificates.
        private Task TitaniumProxy_OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            // Let us assume all are valid.
            //if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            e.IsValid = true;

            return Task.CompletedTask;
        }

        // Handle blocking of a host.
        private string HandleBlocking(SessionEventArgs e)
        {
            e.Ok("Request blocked based on block.txt rules.");
            return "Blocked";
        }

        // Handle forwarding a url to another.
        private string HandleForward(string url, string urlParamsNoProtocol, string forward, SessionEventArgs e)
        {
            string action = "Redirected";

            if (forward.StartsWith("method=")) // See WebHelperMethods for more information.
            {
                action = "Method (F)"; // Method Forwarding.
                string method = forward.Split('=')[1];
                e.Redirect(WebHelperMethods.ProcessForward(method, url, urlParamsNoProtocol));
            }
            else // Normal forwards.
            {
                e.Redirect("http://" + forward);
            }

            return action;
        }

        private string HandleResponse(string url, string urlParamsNoProtocol, string response, SessionEventArgs e)
        {
            string action;

            if (url.EndsWith(":443"))
            {
                action = "Handshake";
            }
            else
            {
                if (response.StartsWith("file=")) // If you want to provide a response from a file, make sure the file is placd at rules/files folder.
                {
                    string filePath = Settings.GetRulesFilesPath() + response.Split('=')[1];
                    if (File.Exists(filePath))
                    {
                        action = "File";
                        e.Ok(File.ReadAllText(filePath));
                    }
                    else
                        action = "File (Missing)";
                }
                else if (response.StartsWith("method=")) // See WebHelperMethods for more information.
                {
                    action = "Method (R)";
                    string method = response.Split('=')[1];
                    e.Ok(WebHelperMethods.ProcessResponse(method, url, urlParamsNoProtocol));
                }
                else // Normal text response from respond.txt.
                {
                    action = "Text";
                    e.Ok(response);
                }
            }

            return action;
        }

        // Handle the downloading of files.
        private void HandleDownload(string host, string url, string urlParams, string urlParamsNoProtocol, string requestMethod, string userAgent, string program, string referer, byte[] bodyBytes)
        {
            string download = Settings.GetDownload(urlParamsNoProtocol);

            if (download != null)
            {
                string method = download.Split('=')[1];
                WebHelperMethods.ProcessDownload(method, host, url, urlParams, requestMethod, userAgent, program, referer, bodyBytes);
            }
        }
    }
}

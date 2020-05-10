using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EASendMail.Oauth;
using System.Net;
using System.Net.Sockets;


namespace AppProject1
{
    /*
       Because Web Browser control is used for OAUTH, 
       Web browser control uses IE7 rendering mode by default, 
       it doesn't support latest Google Web Login Page.

       You should install IE 10/IE11 (recommended) or later version on your current machine,
       and then add/mergin the following registry values to use IE 10 mode. 

       "AppProject1.exe" is your executable file name.
       In current project, it is "AppProject1.exe"
       If you debug it in VS, please also add "AppProject1.vshost.exe"

       Windows Registry Editor Version 5.00 
       [HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION] 
       "AppProject1.exe"=dword:00002AF9
       "AppProject1.vshost.exe"=dword:00002AF9

       [HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
       "AppProject1.exe"=dword:00002AF9
       "AppProject1.vshost.exe"=dword:00002AF9

       Appendix - Web Browser Control Mode:

       11001 (0x2AF9) Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive. 
       11000 (0x2AF8) Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 mode. 
       10001 (0x2711) Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive. 
       10000 (0x2710) Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. 
       9999 (0x270F) Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive. 
       9000 (0x2328) Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. 
       8888 (0x22B8) Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive. 
       8000 (0x1F40) Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. 
       7000 (0x1B58) Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. This mode is kind of pointless since it's the default.

        */
    public partial class FormOauth : Form
    {
        public OauthDesktopWrapper OauthWrapper = null;
        HttpListener _httpListener = null;
        
        public FormOauth()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        // if HttpListener is not used, you can remove async
        private async void FormOauth_Load(object sender, EventArgs e)
        {
            OauthBrowser.DocumentTitleChanged += (x, y) => { this.Text = OauthBrowser.DocumentTitle; };

            //start to open Web OAUTH login web page.
            OauthWrapper.AuthorizationCode = "";

            // Http Listener is Google recommended solution for desktop app, 
            // and MS OAUTH supports it as well, but you need to add http://127.0.0.1 to 
            // Azure portal -> Your app -> Authentication -> Mobile and desktop applications: redirect Uri, please check the following URI.
            if (!OauthWrapper.Provider.UseHttpListener)
            {
                OauthWrapper.Provider.ResetLocalRedirectUri();
                OauthBrowser.Navigate(OauthWrapper.Provider.GetFullAuthUri());
                return;
            }

            string httpRedirectUri = GetHttpRedirectUri();
            OauthWrapper.Provider.RedirectUri = httpRedirectUri;
            OauthBrowser.Navigate(OauthWrapper.Provider.GetFullAuthUri());

            try
            {
                await GetCodefromHttpListener(httpRedirectUri);
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
            finally
            {
                this.Close();
            }
        }

        async Task GetCodefromHttpListener(string httpRedirectUri)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(httpRedirectUri);
            _httpListener.Start();

            try
            {
                var context = await _httpListener.GetContextAsync();
                
                string responseString = string.Format("<html><head><title>Authorization is completed</title></head><body>AuthorizationCode is returned, please close current window and return to the app.</body></html>");
                var buffer = Encoding.UTF8.GetBytes(responseString);

                var response = context.Response;
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;

                await responseOutput.WriteAsync(buffer, 0, buffer.Length);

                responseOutput.Close();
                _httpListener.Stop();

                OauthBrowser.Stop();

                if (context.Request.QueryString.Get("error") != null)
                {
                    MessageBox.Show("OAuth authorization error: {0}.", context.Request.QueryString.Get("error"));
                    return;
                }

                var code = context.Request.QueryString.Get("code");
                OauthWrapper.AuthorizationCode = code;

            }
            catch (ObjectDisposedException)
            {
                // this exception is thrown by closing window, don't handle it.
            }
        }

        private void FormOauth_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_httpListener != null)
            {
                _httpListener.Close();
            }
        }

        static string GetHttpRedirectUri()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();

            return string.Format("http://127.0.0.1:{0}/", port);
        }
      
        private void OauthBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            if (OauthWrapper.AuthorizationCode.Length > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (!OauthWrapper.Provider.ParseAuthorizationCodeInHtml)
            {
                return;
            }

            HtmlElement elment = OauthBrowser.Document.GetElementById("code");
            if (elment == null)
            {
                return;
            }

            OauthWrapper.AuthorizationCode = elment.GetAttribute("value");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OauthBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string code = _parseCodeFromUri(e.Url.Query, "approvalCode="); // only for Google
            if (code.Length == 0)
            {
                code = _parseCodeFromUri(e.Url.Query, "code=");
            }

            if (code.Length == 0)
            {
                return;
            }

            // Close form in OauthBrowser_DocumentCompleted event instead of here
            // If close form here, Google OAUTH Page may open a default web browser window.
            OauthWrapper.AuthorizationCode = code;
        }


        static string _parseCodeFromUri(string input, string key)
        {
            int pos = input.IndexOf('?');
            if (pos != -1)
            {
                input = input.Substring(pos + 1);
            }

            string[] parameters = input.Split(new char[] { '&' });
            for (int i = 0; i < parameters.Length; i++)
            {
                string parameter = parameters[i];
                if (string.Compare(parameter, 0, key, 0, key.Length, true) == 0)
                {
                    return parameter.Substring(key.Length);
                }
            }

            return string.Empty;

        }
    }
}

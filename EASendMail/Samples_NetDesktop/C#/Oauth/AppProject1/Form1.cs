using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using EASendMail;
using EASendMail.Oauth;

namespace AppProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _initialize();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            _initHtmlEditor();
            _initControlOffset();
        }

        private List<string> _attachments = new List<string>();
        private bool _isCancelSending = false;

        #region	EASendMail EventHandler
        void OnIdle(object sender, ref bool cancel)
        {
            cancel = _isCancelSending;
            if (!cancel)
            {
                //Current object is waiting server reponse or connecting server, 
                //that means current object is idle. Application.DoEvents
                //can processes all Windows(form) messages(events) currently in the message queue. 
                //If you don't invoke this method, the application will not respond the Cancel and other
                //events.
                Application.DoEvents();
            }
        }

        void OnConnected(object sender, ref bool cancel)
        {
            _setStatus("Connected");
            cancel = _isCancelSending;
        }

        void OnSendingDataStream(object sender, int sent, int total, ref bool cancel)
        {
            if (ProgressBarSend.Value == 0)
            {
                _setStatus("Sending ...");
            }

            _setProgressBar(sent, total);
            cancel = _isCancelSending;
            if (sent == total)
                _setStatus("Disconnecting ...");
        }

        void OnAuthorized(object sender, ref bool cancel)
        {
            _setStatus("Authorized");
            cancel = _isCancelSending;
        }

        void OnSecuring(object sender, ref bool cancel)
        {
            _setStatus("Securing ...");
            cancel = _isCancelSending;
        }

        #endregion

        #region Initialize Encoding/Protocols/Ports

        private string[,] _charsets = new string[,] {
            {"Arabic(Windows)", "windows-1256"},
            {"Baltic(ISO)", "iso-8859-4"},
            {"Baltic(Windows)", "windows-1257"},
            {"Central Euporean(ISO)", "iso-8859-2"},
            {"Central Euporean(Windows)", "windows-1250"},
            {"Chinese Simplified(GB18030)", "GB18030"},
            {"Chinese Simplified(GB2312)", "gb2312"},
            {"Chinese Simplified(HZ)", "hz-gb-2312"},
            {"Chinese Traditional(Big5)", "big5"},
            {"Cyrillic(ISO)", "iso-8859-5"},
            {"Cyrillic(KOI8-R)", "koi8-r"},
            {"Cyrillic(KOI8-U)", "koi8-u"},
            {"Cyrillic(Windows)", "windows-1251"},
            {"Greek(ISO)", "iso-8859-7"},
            {"Greek(Windows)", "windows-1253"},
            {"Hebrew(Windows)", "windows-1255"},
            {"Japanese(JIS)", "iso-2022-jp"},
            {"Korean", "ks_c_5601-1987"},
            {"Korean(EUC)", "euc-kr"},
            {"Latin 9(ISO)", "iso-8859-15"},
            {"Thai(Windows)", "windows-874"},
            {"Turkish(ISO)", "iso-8859-9"},
            {"Turkish(Windows)", "windows-1254"},
            {"Unicode(UTF-7)", "utf-7"},
            {"Unicode(UTF-8)", "utf-8"},
            {"Vietnames(Windows)", "windows-1258"},
            {"Western European(ISO)", "iso-8859-1"},
            {"Western European(Windows)", "Windows-1252"}
        };

        private void _initCharsets()
        {
            int selectIndex = 24; //utf-8
            for (int i = 0; i < _charsets.GetLength(0); i++)
            {
                ComboBoxEncoding.Items.Add(_charsets[i, 0]);
            }

            ComboBoxEncoding.SelectedIndex = selectIndex;
        }


        private void _initialize()
        {
            _initCharsets();
            _initProtocols();
        }

        private void _initProtocols()
        {
            ComboBoxPorts.Items.AddRange(new string[] { "25", "587", "465" });
            ComboBoxPorts.SelectedIndex = 1;

            ComboBoxProviders.Items.AddRange(new string[] { "Google OAUTH + SMTP", "Live OAUTH + SMTP (Hotmail)", "MS OAUTH + EWS (Office365)", "Gmail Reset Api + OAUTH" });
            ComboBoxProviders.SelectedIndex = 0;
        }

        private string _getServerAddressByProvider(int index)
        {
            switch(index)
            {
                case OauthProvider.GoogleSmtpProvider:
                    return "smtp.gmail.com";
                case OauthProvider.MsLiveProvider:
                    return "smtp.live.com";
                case OauthProvider.MsOffice365Provider:
                    return "outlook.office365.com";
                case OauthProvider.GoogleGmailApiProvider:
                    return "https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=media";
                default:
                    throw new Exception("Invalid OAUTH provider!");
            }
        }

        OauthDesktopWrapper _oauthWrapper = null;
        private void ComboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxServer.Text = _getServerAddressByProvider(ComboBoxProviders.SelectedIndex);
            ComboBoxPorts.Enabled = (
                ComboBoxProviders.SelectedIndex == OauthProvider.MsLiveProvider ||
                ComboBoxProviders.SelectedIndex == OauthProvider.GoogleSmtpProvider);

            switch (ComboBoxProviders.SelectedIndex)
            {
                case OauthProvider.GoogleSmtpProvider:
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateGoogleSmtpProvider());
                    break;
                case OauthProvider.MsLiveProvider:
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateMsLiveProvider());
                    break;
                case OauthProvider.MsOffice365Provider:
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateMsOffice365Provider());
                    break;
                case OauthProvider.GoogleGmailApiProvider:
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateGmailApiProvider());
                    break;
                default:
                    throw new Exception("Invalid OAUTH provider!");
            }
        }

        private void CheckBoxListener_CheckedChanged(object sender, EventArgs e)
        {
            if (_oauthWrapper != null)
            {
                _oauthWrapper.Provider.ClearToken();
                _oauthWrapper.Provider.UseHttpListener = CheckBoxListener.Checked;
            }
        }

        #endregion

        #region Sign and Encrypt E-mail by Digital Certificate
        private void _signEmail(SmtpMail mail)
        {
            if (!CheckBoxSignature.Checked)
            {
                return;
            }

            try
            {
                //search the signature certificate.
                mail.From.Certificate.FindSubject(mail.From.Address,
                    Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                    "My");
            }
            catch (Exception exp)
            {
                // rewrite the exception information to specify it is about signer certificate.
                throw new Exception("No signer certificate found for <" + mail.From.Address + ">: " + exp.Message);
            }
        }

        private void _encryptEmail(SmtpMail mail)
        {
            if (!CheckBoxEncrypt.Checked)
            {
                return;
            }

            // try to lookup certificate in addressbook and my 
            for (int i = 0; i < mail.To.Count; i++)
            {
                MailAddress oAddress = mail.To[i] as MailAddress;
                try
                {
                    oAddress.Certificate.FindSubject(oAddress.Address,
                        Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                        "AddressBook");
                }
                catch
                {
                    try
                    {
                        oAddress.Certificate.FindSubject(oAddress.Address,
                            Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                            "My");
                    }
                    catch (Exception exp)
                    {
                        // rewrite the exception information to specify it is about recipient certificate.
                        throw new Exception("No encryption certificate found for <" + oAddress.Address + ">: " + exp.Message);
                    }
                }
            }

            for (int i = 0; i < mail.Cc.Count; i++)
            {
                MailAddress oAddress = mail.Cc[i] as MailAddress;
                try
                {
                    oAddress.Certificate.FindSubject(oAddress.Address,
                        Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                        "AddressBook");
                }
                catch
                {
                    try
                    {
                        oAddress.Certificate.FindSubject(oAddress.Address,
                            Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                            "My");
                    }
                    catch (Exception exp)
                    {
                        // rewrite the exception information to specify it is about recipient certificate.
                        throw new Exception("No encryption certificate found for <" + oAddress.Address + ">:" + exp.Message);
                    }
                }
            }
        }

        private void _signAndEncryptEmail(SmtpMail mail)
        {
            _signEmail(mail);
            _encryptEmail(mail);
        }
        #endregion

        #region Create SmtpMail and SmtpServer instance based on Settings of Form Controls
        private SmtpMail _createMail()
        {
            //For evaluation usage, please use "TryIt" as the license code, otherwise the 
            //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            //"trial version expired" exception will be thrown.

            //For licensed uasage, please use your license code instead of "TryIt", then the object
            //will never expire
            SmtpMail mail = new SmtpMail("TryIt");

            mail.From = TextBoxFrom.Text;
            // if from address is different with authenticated user, change from to authenticated user
            // and change from address to replyto.
            if (string.Compare(mail.From.Address, _oauthWrapper.Provider.UserEmail, true) != 0)
            {
                mail.ReplyTo = mail.From;
                mail.From = _oauthWrapper.Provider.UserEmail;
            }

            mail.To = TextBoxTo.Text;

            mail.Cc = TextBoxCc.Text;
            mail.Subject = TextBoxSubject.Text;
            mail.Charset = _charsets[ComboBoxEncoding.SelectedIndex, 1];

            string htmlBody = _buildHtmlBody();
            htmlBody = htmlBody.Replace("[$from]", _encodeAddressToHtml(mail.From.ToString()));
            htmlBody = htmlBody.Replace("[$to]", _encodeAddressToHtml(mail.To.ToString()));
            htmlBody = htmlBody.Replace("[$subject]", _encodeAddressToHtml(mail.Subject));

            mail.ImportHtml(htmlBody,
                Application.ExecutablePath,
                ImportHtmlBodyOptions.ImportLocalPictures);

            int count = _attachments.Count;
            for (int i = 0; i < count; i++)
            {
                //Add attachment
                mail.AddAttachment(_attachments[i] as string);
            }

            //Add digital signature and encrypt email on demanded
            _signAndEncryptEmail(mail);

            return mail;
        }

        private SmtpServer _createSmtpServer()
        {
            SmtpServer server = new SmtpServer(TextBoxServer.Text);
            if (server.Server.Length == 0)
            {
                server.Server = _getServerAddressByProvider(ComboBoxProviders.SelectedIndex);
            }
            
            int[] ports = { 25, 587, 465 };
            server.Port = ports[ComboBoxPorts.SelectedIndex];
            server.ConnectType = SmtpConnectType.ConnectSSLAuto;

            // Office365 OAUTH only supports EWS protocol. Google and Live OAUTH support SMTP protocol.
            switch (ComboBoxProviders.SelectedIndex)
            {   case OauthProvider.MsOffice365Provider:
                    server.Protocol = ServerProtocol.ExchangeEWS;
                    break;
                case OauthProvider.GoogleGmailApiProvider:
                    server.Protocol = ServerProtocol.GmailApi;
                    break;
                default:
                    server.Protocol = ServerProtocol.SMTP;
                    break;
            }
            
            server.AuthType = SmtpAuthType.XOAUTH2;
            server.User = _oauthWrapper.Provider.UserEmail;
            server.Password = _oauthWrapper.Provider.AccessToken;

            return server;
        }

        #endregion

       
        void _doOauth()
        {
            // AccessToken is existed, if it is not expired, use it directly, otherwise refresh it.
            if (!string.IsNullOrEmpty(_oauthWrapper.Provider.AccessToken))
            {
                if (!_oauthWrapper.IsAccessTokenExpired)
                {
                    return;
                }

                StatusBarSend.Text = "Refreshing access token ...";
                try
                {
                    _oauthWrapper.RefreshAccessToken();
                }
                catch
                {
                    StatusBarSend.Text = "Failed to refresh access token, try to get a new access token ...";
                }
            }

            using (FormOauth DialogOauth = new FormOauth())
            {
                _oauthWrapper.Provider.UseHttpListener = CheckBoxListener.Checked;

                DialogOauth.OauthWrapper = _oauthWrapper;
                DialogOauth.ShowDialog();
            }

            StatusBarSend.Text = "Requesting access token ...";
            _oauthWrapper.RequestAccessTokenAndUserEmail();
            StatusBarSend.Text = "Oauth is completed, ready to send email.";
        }

        private void ButtonSend_Click(object sender, System.EventArgs e)
        {
            

            ButtonSend.Enabled = false;
            ButtonCancel.Enabled = true;
            ButtonClearToken.Enabled = false;
            _isCancelSending = false;

            try
            {
       
                // request access token if it is not existed.
                _doOauth();

                if (TextBoxTo.Text.Length == 0 &&
                        TextBoxCc.Text.Length == 0)
                {
                    throw new ArgumentNullException("To, Cc", "Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients");
                }

               // If You didn't input from address, authenticated user's email address is used automatically.");
                
                SmtpClient smtp = new SmtpClient();

                // To generate a log file for SMTP transaction, please use
                // oSmtp.LogFileName = "c:\\smtp.log";

                // Catching the following events is not necessary, 
                // just make the application more user friendly.

                // If you use the object in asp.net/windows service or non-gui application, 
                // You need not to catch the following events.
                // To learn more detail, please refer to the code in EASendMail EventHandler region
                smtp.OnIdle += new SmtpClient.OnIdleEventHandler(OnIdle);
                smtp.OnAuthorized += new SmtpClient.OnAuthorizedEventHandler(OnAuthorized);
                smtp.OnConnected += new SmtpClient.OnConnectedEventHandler(OnConnected);
                smtp.OnSecuring += new SmtpClient.OnSecuringEventHandler(OnSecuring);
                smtp.OnSendingDataStream += new SmtpClient.OnSendingDataStreamEventHandler(OnSendingDataStream);

                // create SmtpServer based on settings of Form Control.
                SmtpServer server = _createSmtpServer();

                // create SmtpMail based on settings of Form Controls.
                SmtpMail mail = _createMail();

                StatusBarSend.Text = "Connecting ... ";
                ProgressBarSend.Value = 0;

                smtp.SendMail(server, mail);

                MessageBox.Show(string.Format("The message was sent to {0} successfully!",
                    smtp.CurrentSmtpServer.Server));

                StatusBarSend.Text = "Completed";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                StatusBarSend.Text = string.Format("Exception: {0}", exp.Message);
                if (exp is ArgumentNullException)
                {
                    TextBoxTo.Select();
                }
                else
                {
                    _oauthWrapper.Provider.ClearToken();
                }
            }

            ButtonSend.Enabled = true;
            ButtonCancel.Enabled = false;
            ButtonClearToken.Enabled = (_oauthWrapper.Provider.AccessToken.Length != 0);
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            ButtonCancel.Enabled = false;
            _isCancelSending = true;
        }

        private void ButtonClearToken_Click(object sender, EventArgs e)
        {
            _oauthWrapper.Provider.ClearToken();
            _oauthWrapper.AuthorizationCode = "";
            ButtonClearToken.Enabled = false;
        }

        private void ButtonAddAttachment_Click(object sender, System.EventArgs e)
        {
            HtmlEditor.Focus();

            OpenFileDialogAttachment.Reset();
            OpenFileDialogAttachment.Multiselect = true;
            OpenFileDialogAttachment.CheckFileExists = true;
            OpenFileDialogAttachment.CheckPathExists = true;

            if (OpenFileDialogAttachment.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] attachments = OpenFileDialogAttachment.FileNames;
            for (int i = 0; i < attachments.Length; i++)
            {
                _attachments.Add(attachments[i]);

                string fileName = attachments[i];
                int pos = fileName.LastIndexOf("\\");
                if (pos != -1)
                    fileName = fileName.Substring(pos + 1);

                TextBoxAttachments.Text += fileName;
                TextBoxAttachments.Text += ";";
            }
        }

        private void ButtonClearAttachments_Click(object sender, System.EventArgs e)
        {
            _attachments.Clear();
            TextBoxAttachments.Text = "";
        }

        #region HTML Editor

        mshtml.IHTMLDocument2 _htmlDoc = null;

        private void ButtonBold_Click(object sender, System.EventArgs e)
        {
            _htmlDoc.execCommand("Bold", false, null);
            HtmlEditor.Focus();
        }

        private void ButtonItalic_Click(object sender, System.EventArgs e)
        {
            _htmlDoc.execCommand("Italic", false, null);
            HtmlEditor.Focus();
        }

        private void ButtonUnderline_Click(object sender, System.EventArgs e)
        {
            _htmlDoc.execCommand("Underline", false, null);
            HtmlEditor.Focus();
        }

        private void ButtonColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialogForeColor.ShowDialog() == DialogResult.OK)
            {
                string v = string.Format("#{0:x2}{1:x2}{2:x2}", ColorDialogForeColor.Color.R,
                    ColorDialogForeColor.Color.G,
                    ColorDialogForeColor.Color.B);
                _htmlDoc.execCommand("ForeColor", false, v);
            }
            HtmlEditor.Focus();
        }

        private void _initFonts()
        {
            string[] fonts = new string[] {
                        "Choose Font Style ...",
                        "Arial",
                        "Calibri",
                        "Comic Sans MS",
                        "Consolas",
                        "Courier New",
                        "Helvetica",
                        "Times New Roman",
                        "Tahoma",
                        "Verdana",
                        "Segoe UI" };

            int nCount = fonts.Length;
            for (int i = 0; i < nCount; i++)
            {
                ComboBoxFont.Items.Add(fonts[i]);
            }
            ComboBoxFont.SelectedIndex = 0;

            ComboBoxSize.Items.Add("Font Size ... ");
            for (int i = 1; i < 7; i++)
            {
                ComboBoxSize.Items.Add(i);
            }
            ComboBoxSize.SelectedIndex = 0;
        }

        private void ComboBoxFont_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string font = ComboBoxFont.Text;
            ComboBoxFont.SelectedIndex = 0;
            _htmlDoc.execCommand("fontname", false, font);
            HtmlEditor.Focus();
        }

        private void ComboBoxSize_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string size = ComboBoxSize.Text;
            ComboBoxSize.SelectedIndex = 0;
            _htmlDoc.execCommand("fontsize", false, size);
            HtmlEditor.Focus();
        }

        private void ButtonInsertImage_Click(object sender, System.EventArgs e)
        {
            _htmlDoc.execCommand("InsertImage", true, null);
        }

        private string _encodeAddressToHtml(string v)
        {
            v = v.Replace(">", "&gt;");
            v = v.Replace("<", "&lt;");
            return v;
        }

        private void _initHtmlEditor()
        {
            HtmlEditor.Navigate("about:blank");
            _htmlDoc = HtmlEditor.Document.DomDocument as mshtml.IHTMLDocument2;
            _htmlDoc.designMode = "on";
            _initFonts();
        }

        string _defaultBodyFont = "Calibri";
        private void HtmlEditor_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<div>This sample demonstrates how to send html email using OAUTH 2.0.</div><div>&nbsp;</div>");
            s.Append("<div>From: [$from]</div>");
            s.Append("<div>To: [$to]</div>");
            s.Append("<div>Subject: [$subject]</div><div>&nbsp;</div>");
            s.Append("<div>This sample project demonstrates how to send email using Gmail/MS Live/MS Office365 XOAUTH2, ");
            s.Append("Please create your client_id and client_secret introduced in Form OauthProvider.cs.");
            s.Append("</div>");
            s.Append("<div>If you got \"This app isn't verified\" information, please click \"advanced\" -> Go to ... for test.</div>");
            s.Append("<div>&nbsp;</div>");

            _htmlDoc.body.style.fontFamily = _defaultBodyFont;
            _htmlDoc.body.innerHTML = s.ToString();
        }

        private string _buildHtmlBody()
        {
            string html = string.Format(
                "<html><title>{0}</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset={1}\"><META content=\"MSHTML 6.00.2900.2769\" name=GENERATOR><body style=\"font-family: \'{2}\';\">",
                TextBoxSubject.Text, _charsets[ComboBoxEncoding.SelectedIndex, 1], _defaultBodyFont);

            html += _htmlDoc.body.innerHTML;
            html += "</body></html>";

            return html;
        }

        #endregion

        #region AutoSize Control

        bool _isFormLoaded = false;

        int _textBoxFromWidthOffset = 0;
        int _textBoxAttachmentsWidthOffset = 0;

        int _buttonAddAttachmentLeftOffset = 0;
        int _buttonClearAttachmentsLeftOffset = 0;

        int _serverGroupBoxOffset = 0;

        int _textBoxBodyHeightOffset = 0;

        int _progressBarSendTopOffset = 0;
        int _progressBarSendWidthOffset = 0;

        int _buttonSendTopOffset = 0;
        int _buttonSendLeftOffset = 0;
        int _buttonCancelLeftOffset = 0;

        private void _initControlOffset()
        {
            // no smaller than design time size
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            _isFormLoaded = true;

            _textBoxAttachmentsWidthOffset = this.Width - TextBoxAttachments.Width;

            _textBoxBodyHeightOffset = this.Height - HtmlEditor.Height;
            _progressBarSendTopOffset = this.Height - ProgressBarSend.Top;
            _buttonSendTopOffset = this.Height - ButtonSend.Top;

            _textBoxFromWidthOffset = this.Width - TextBoxFrom.Width;
            _progressBarSendWidthOffset = this.Width - ProgressBarSend.Width;

            _buttonAddAttachmentLeftOffset = this.Width - ButtonAddAttachment.Left;
            _buttonClearAttachmentsLeftOffset = this.Width - ButtonClearAttachments.Left;

            _serverGroupBoxOffset = this.Width - GroupBoxServer.Left;
            _buttonSendLeftOffset = this.Width - ButtonSend.Left;
            _buttonCancelLeftOffset = this.Width - ButtonCancel.Left;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!_isFormLoaded)
            {
                return;
            }

            TextBoxAttachments.Width = this.Width - _textBoxAttachmentsWidthOffset;

            ButtonAddAttachment.Left = this.Width - _buttonAddAttachmentLeftOffset;
            ButtonClearAttachments.Left = this.Width - _buttonClearAttachmentsLeftOffset;

            HtmlEditor.Width = this.Width - 40;
            HtmlEditor.Height = this.Height - _textBoxBodyHeightOffset;

            ProgressBarSend.Top = this.Height - _progressBarSendTopOffset;
            ButtonSend.Top = this.Height - _buttonSendTopOffset;
            ButtonCancel.Top = ButtonSend.Top;

            ProgressBarSend.Width = this.Width - _progressBarSendWidthOffset;
            GroupBoxServer.Left = this.Width - _serverGroupBoxOffset;

            ButtonSend.Left = this.Width - _buttonSendLeftOffset;
            ButtonCancel.Left = this.Width - _buttonCancelLeftOffset;

            TextBoxFrom.Width = this.Width - _textBoxFromWidthOffset;
            TextBoxTo.Width = TextBoxFrom.Width;
            TextBoxCc.Width = TextBoxFrom.Width;
            TextBoxSubject.Width = TextBoxFrom.Width;
        }

        #endregion

        #region Cross Thread Access Control
        protected delegate void SetStatusDelegate(string v);
        protected delegate void SetProgressDelegate(int sent, int total);

        private long _eventTicks = 0;
        protected void _setProgressBarCallback(int sent, int total)
        {
            long scale = (sent * 100) / total;
            ProgressBarSend.Value = (int)scale;

            long tick = DateTime.Now.Ticks;
            // call DoEvents every 0.2 second 
            if (tick - _eventTicks > 2000000)
            {
                // Do not call DoEvents too frequently in a very fast lan + larg email.
                _eventTicks = tick;
                Application.DoEvents();
            }
        }

        protected void _setStatusCallback(string v)
        {
            StatusBarSend.Text = v;
        }

        // Why we need to change the status text by this function.
        // Because some the events are fired on another
        // thread, to change the control value safety, we used this function to 
        // update control value. more detail, please refer to Control.BeginInvoke method
        // in MSDN
        protected void _setStatus(string v)
        {
            if (InvokeRequired)
            {
                object[] args = new object[1];
                args[0] = v;

                SetStatusDelegate d = new SetStatusDelegate(_setStatusCallback);
                BeginInvoke(d, args);
            }
            else
            {
                _setStatusCallback(v);
            }
        }

        protected void _setProgressBar(int sent, int total)
        {
            if (InvokeRequired)
            {
                object[] args = new object[2];
                args[0] = sent;
                args[1] = total;

                SetProgressDelegate d = new SetProgressDelegate(_setProgressBarCallback);
                BeginInvoke(d, args);
            }
            else
            {
                _setProgressBarCallback(sent, total);
            }
        }
        #endregion


    }
}

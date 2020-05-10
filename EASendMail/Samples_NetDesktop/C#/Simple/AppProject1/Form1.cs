using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EASendMail;

namespace AppProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _initialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                // Current object is waiting server reponse or connecting server, 
                // that means current object is idle. Application.DoEvents
                // can processes all Windows(form) messages(events) currently in the message queue. 
                // If you don't invoke this method, the application will not respond the Cancel and other
                // events.
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

        private void _initProtocols()
        {
            ComboBoxProtocols.Items.AddRange(
                new string[] {
                "SMTP Protocol - Recommended",
                "Exchange Web Service - 2007-2019/Office365",
                "Exchange WebDav - 2000/2003"}
                );
            ComboBoxProtocols.SelectedIndex = 0;

            ComboBoxPorts.Items.AddRange(new string[] { "25", "587", "465" });
            ComboBoxPorts.SelectedIndex = 0;
        }

        private void _initialize()
        {
            StringBuilder s = new StringBuilder();
            s.Append("This sample demonstrates how to send simple email.\r\n\r\n");
            s.Append("From: [$from]\r\n");
            s.Append("To: [$to]\r\n");
            s.Append("Subject: [$subject]\r\n\r\n");
            s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly. ");
            s.Append("However, if you don't have a static IP address, ");
            s.Append("many anti-spam filters will mark it as a junk-email.\r\n\r\n");
            s.Append("If \"Digitial Signature\" was checked, please make sure you have the certificate for the sender address installed on ");
            s.Append("Local User Certificate Store.\r\n\r\n");
            s.Append("If \"Encrypt\" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.\r\n");

            TextBoxBody.Text = s.ToString();

            _initCharsets();
            _initProtocols();
            _checkBoxAuthChanged();
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

        #region Send E-mail without SMTP server to multiple recipients
        // It is not recommended, most email providers will reject the email due to anti-spam policy.

        private SmtpMail _createMailForDirectSend(MailAddress address)
        {
            //For evaluation usage, please use "TryIt" as the license code, otherwise the 
            //"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            //"trial version expired" exception will be thrown.

            //For licensed uasage, please use your license code instead of "TryIt", then the object
            //will never expire
            SmtpMail mail = new SmtpMail("TryIt");

            // If you want to specify a reply address
            // mail.ReplyTo = "reply@mydomain";

            // From is a MailAddress object, in c#, it supports implicit converting from string.
            // The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

            // The example code without implicit converting
            // mail.From = new MailAddress( "Tester", "test@adminsystem.com" )
            // mail.From = new MailAddress( "Tester<test@adminsystem.com>" )
            // mail.From = new MailAddress( "test@adminsystem.com" )
            mail.From = TextBoxFrom.Text;

            // To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
            // multiple address are separated with (,;)
            // The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

            // The example code without implicit converting
            // mail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
            // mail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");

            mail.To.Add(address);
            mail.Subject = TextBoxSubject.Text;
            mail.Charset = _charsets[ComboBoxEncoding.SelectedIndex, 1];

            // To send email to the recipient directly(simulating the smtp server), 
            // please add a Received header, 
            // otherwise, many anti-spam filter will make it as junk email.
            SmtpServer server = new SmtpServer("");
            System.Globalization.CultureInfo cur = new System.Globalization.CultureInfo("en-US");
            string gmtDateTime = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur);
            gmtDateTime.Remove(gmtDateTime.Length - 3, 1);
            string receivedHeader = string.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
                server.HeloDomain,
                gmtDateTime);

            mail.Headers.Insert(0, new HeaderItem("Received", receivedHeader));

            string body = TextBoxBody.Text;
            body = body.Replace("[$from]", mail.From.ToString());
            body = body.Replace("[$to]", mail.To.ToString());
            body = body.Replace("[$subject]", mail.Subject);

            if (CheckBoxHtml.Checked)
            {
                mail.HtmlBody = body;
            }
            else
            {
                mail.TextBody = body;
            }

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
        private void _directSendEmail()
        {
            ButtonSend.Enabled = false;
            ButtonCancel.Enabled = true;
            _isCancelSending = false;

            AddressCollection recipients = new AddressCollection();
            recipients.AddRange(new AddressCollection(TextBoxTo.Text));
            recipients.AddRange(new AddressCollection(TextBoxCc.Text));

            if (recipients.Count == 0)
            {
                MessageBox.Show("No recipient found!");
                StatusBarSend.Text = "No recipient found!";

                ButtonSend.Enabled = true;
                ButtonCancel.Enabled = false;
                return;
            }

            // because each recipient might have different SMTP server, 
            // so we have to send email to each recipient one by one.
            for (int i = 0; i < recipients.Count; i++)
            {
                MailAddress address = recipients[i] as MailAddress;

                try
                {
                    SmtpServer server = new SmtpServer("");
                    // If remote SMTP server supports TLS, use TLS, then use plain TCP connection.
                    server.ConnectType = SmtpConnectType.ConnectTryTLS;

                    // create SmtpMail instance for each recipient
                    SmtpMail mail = _createMailForDirectSend(address);
                    StatusBarSend.Text = string.Format("Connecting server for {0} ... ", address.Address);
                    ProgressBarSend.Value = 0;

                    SmtpClient smtp = new SmtpClient();

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

                    smtp.SendMail(server, mail);

                    MessageBox.Show(string.Format("The message to <{0}> was sent to {1} successfully!",
                                    address.Address,
                                    smtp.CurrentSmtpServer.Server));

                    StatusBarSend.Text = "Completed";
                }
                catch (SmtpTerminatedException exp)
                {
                    StatusBarSend.Text = exp.Message;
                    break;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(string.Format("The message was unable to delivery to <{0}> due to \r\n{1}",
                            address.Address, exp.Message));

                    StatusBarSend.Text = string.Format("Exception: {0}", exp.Message);
                }
            }

            ButtonSend.Enabled = true;
            ButtonCancel.Enabled = false;
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
            
            // If you want to specify a reply address
            // mail.ReplyTo = "reply@mydomain";

            // From is a MailAddress object, in c#, it supports implicit converting from string.
            // The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"

            // The example code without implicit converting
            // mail.From = new MailAddress( "Tester", "test@adminsystem.com" )
            // mail.From = new MailAddress( "Tester<test@adminsystem.com>" )
            // mail.From = new MailAddress( "test@adminsystem.com" )
            mail.From = TextBoxFrom.Text;

            // To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
            // multiple address are separated with (,;)
            // The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

            // The example code without implicit converting
            // mail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
            // mail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");

            mail.To = TextBoxTo.Text;
            // You can add more recipient by Add method
            // mail.To.Add( new MailAddress( "tester", "test@adminsystem.com"));

            mail.Cc = TextBoxCc.Text;
            mail.Subject = TextBoxSubject.Text;
            mail.Charset = _charsets[ComboBoxEncoding.SelectedIndex, 1];

            string body = TextBoxBody.Text;
            body = body.Replace("[$from]", mail.From.ToString());
            body = body.Replace("[$to]", mail.To.ToString());
            body = body.Replace("[$subject]", mail.Subject);

            if (CheckBoxHtml.Checked)
            {
                mail.HtmlBody = body;
            }
            else
            {
                mail.TextBody = body;
            }

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
            server.Protocol = (ServerProtocol)ComboBoxProtocols.SelectedIndex;

            // If remote SMTP server supports TLS, then use TLS, otherwise use plain TCP/IP.
            server.ConnectType = SmtpConnectType.ConnectTryTLS;

            if (server.Server.Length != 0)
            {
                int[] ports = { 25, 587, 465 };
                server.Port = ports[ComboBoxPorts.SelectedIndex];

                if (CheckBoxAuth.Checked)
                {
                    server.User = TextBoxUser.Text;
                    server.Password = TextBoxPassword.Text;
                }

                if (CheckBoxSsl.Checked)
                {
                    server.ConnectType = SmtpConnectType.ConnectSSLAuto;
                }
            }

            return server;
        }

        #endregion

        private void ButtonSend_Click(object sender, System.EventArgs e)
        {
            if (TextBoxFrom.Text.Length == 0)
            {
                MessageBox.Show("Please input From!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>");
                return;
            }

            if (TextBoxTo.Text.Length == 0 &&
                TextBoxCc.Text.Length == 0)
            {
                MessageBox.Show("Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients");
                return;
            }

            if (CheckBoxAuth.Checked &&
                (TextBoxUser.Text.Length == 0 || TextBoxPassword.Text.Length == 0))
            {
                MessageBox.Show("Please input user/password for authentication!");
                return;
            }

            if (TextBoxServer.Text.Length == 0)
            {
                // Send email without specified SMTP server, it is not recommended.
                // Due to anti-spam policy, most email providers would reject or detect your email as junk email.
                _directSendEmail();

                return;
            }

            ButtonSend.Enabled = false;
            ButtonCancel.Enabled = true;
            _isCancelSending = false;

            try
            {
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
            }

            ButtonSend.Enabled = true;
            ButtonCancel.Enabled = false;
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            ButtonCancel.Enabled = false;
            _isCancelSending = true;
        }

        private void ButtonAddAttachment_Click(object sender, System.EventArgs e)
        {
            openFileDialogAttachment.Reset();
            openFileDialogAttachment.Multiselect = true;
            openFileDialogAttachment.CheckFileExists = true;
            openFileDialogAttachment.CheckPathExists = true;

            if (openFileDialogAttachment.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] attachments = openFileDialogAttachment.FileNames;
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

        //Why we need to change the status text by this function.
        //Because some the events are fired on another
        //thread, to change the control value safety, we used this function to 
        //update control value. more detail, please refer to Control.BeginInvoke method
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

        #region Guess SMTP server for popular email provider

        bool _autoChangeServer = true;
        bool _autoChangeUser = true;
        private void TextBoxUser_KeyUp(object sender, KeyEventArgs e)
        {
            // If user input user manually, do not change user automatically except the server value is empty string.
            _autoChangeUser = (TextBoxUser.Text.Length == 0);
        }

        private void TextBoxServer_KeyUp(object sender, KeyEventArgs e)
        {
            // If user input server manually, do not change server automatically except the server value is empty string.
            _autoChangeServer = (TextBoxServer.Text.Length == 0);
        }

        private void TextBoxFrom_TextChanged(object sender, EventArgs e)
        {
            MailAddress address = new MailAddress(TextBoxFrom.Text);
            if (_autoChangeUser)
            {
                TextBoxUser.Text = address.Address;
            }

            if (_autoChangeServer)
            {
                string domain = address.GetAddressDomain();
                if (string.Compare(domain, "hotmail.com", true) == 0)
                {
                    TextBoxServer.Text = "smtp.live.com";
                }
                else if (string.Compare(domain, "gmail.com", true) == 0)
                {
                    TextBoxServer.Text = "smtp.gmail.com";
                }
                else if (string.Compare(domain, "yahoo.com", true) == 0)
                {
                    TextBoxServer.Text = "smtp.mail.yahoo.com";
                }
                else if (string.Compare(domain, "aol.com", true) == 0)
                {
                    TextBoxServer.Text = "smtp.aol.com";
                }

                ChangeSettingforWellKnownServer(TextBoxServer.Text);
            }
        }

        private void ChangeSettingforWellKnownServer(string server)
        {
            if (string.Compare(server, "smtp.gmail.com", true) == 0 ||
                string.Compare(server, "smtp.live.com", true) == 0 ||
                string.Compare(server, "smtp.mail.yahoo.com", true) == 0 ||
                string.Compare(server, "smtp.office365.com", true) == 0 ||
                string.Compare(server, "smtp.aol.com", true) == 0)
            {
                ComboBoxPorts.SelectedIndex = 1; //587 port, you can also use 25, 465
                CheckBoxSsl.Checked = true;
                CheckBoxAuth.Checked = true;
            }
        }

        private void TextBoxServer_TextChanged(object sender, EventArgs e)
        {
            ChangeSettingforWellKnownServer(TextBoxServer.Text);
        }

        private void _checkBoxAuthChanged()
        {
            TextBoxUser.Enabled = CheckBoxAuth.Checked;
            TextBoxPassword.Enabled = CheckBoxAuth.Checked;
        }

        private void CheckBoxAuth_CheckedChanged(object sender, System.EventArgs e)
        {
            _checkBoxAuthChanged();
        }

        private void ComboBoxProtocols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxProtocols.SelectedIndex == 1)
            {
                // by default EWS protocol requires SSL connection and user authentication.
                CheckBoxSsl.Checked = true;
                ComboBoxPorts.Enabled = false;
                CheckBoxAuth.Checked = true;
                _checkBoxAuthChanged();
            }
            else
            {
                ComboBoxPorts.Enabled = true;
            }
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

            _textBoxBodyHeightOffset = this.Height - TextBoxBody.Height;
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

            TextBoxBody.Width = this.Width - 50;
            TextBoxBody.Height = this.Height - _textBoxBodyHeightOffset;

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EASendMail;

namespace AppProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _intialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _initControlOffset();
        }

        private List<string> _attachments = new List<string>();
        private bool _isCancelSending = false;

        private int _total = 0;
        private int _succeeded = 0;
        private int _failed = 0;

        #region Cross Thread Access List Item

        private delegate void SetRecipientStatusDelegate(int recipientIndex, string status);
        private void _updateRecipientStatus(int recipientIndex, string status)
        {
            ListViewTo.Items[recipientIndex].SubItems[2].Text = status;
        }

        // Why we need to change the list item text by this function.
        // Because with BeginSendMail method, all the events are fired on another
        // thread, to change the list item safety, we used this function to 
        // update list item. more detail, please refer to Control.BeginInvoke method
        // in MSDN
        private void _crossThreadUpdateRecipientStatus(int recipientIndex, string status)
        {
            if (InvokeRequired)
            {
                object[] args = new object[2];
                args[0] = recipientIndex;
                args[1] = status;
                SetRecipientStatusDelegate d = new SetRecipientStatusDelegate(_updateRecipientStatus);
                BeginInvoke(d, args);
            }
            else
            {
                _updateRecipientStatus(recipientIndex, status);
            }
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
            ComboBoxProtocols.Items.Add("SMTP Protocol - Recommended");
            ComboBoxProtocols.Items.Add("Exchange Web Service - 2007-2019/Office365");
            ComboBoxProtocols.Items.Add("Exchange WebDav - 2000/2003");
            ComboBoxProtocols.SelectedIndex = 0;

            ComboBoxPorts.Items.Add("25");
            ComboBoxPorts.Items.Add("587");
            ComboBoxPorts.Items.Add("465");
            ComboBoxPorts.SelectedIndex = 0;
        }

        private void _intialize()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Hi [$name], \r\nThis sample demonstrates how to send email to mutilple recipients.\r\n\r\n");
            s.Append("From: [$from]\r\n");
            s.Append("To: <[$address]>\r\n");
            s.Append("Subject: [$subject]\r\n\r\n");

            s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly.\r\n");
            s.Append("However, if you don't have a static IP address, ");
            s.Append("many anti-spam filters will mark it as a junk-email.\r\n\r\n");

            s.Append("If \"Test Email Address\" was checked, then only the recipient address will be tested and no message will be sent.\r\n");

            TextBoxBody.Text = s.ToString();

            _initCharsets();
            _initProtocols();
            _checkBoxAuthChanged();
        }
        #endregion

        #region Verify and disable control

        private bool _verifyInput()
        {
            if (TextBoxFrom.Text.Length == 0)
            {
                MessageBox.Show("Please input From, the format can be test@domain.com or Tester<test@domain.com>");
                TextBoxFrom.Focus();
                return false;
            }

            if (ListViewTo.Items.Count == 0)
            {
                MessageBox.Show("Please add a recipient at least!");
                ButtonAddRecipient.Focus();
                return false;
            }

            if (CheckBoxAuth.Checked &&
                (TextBoxUser.Text.Length == 0 || TextBoxPassword.Text.Length == 0))
            {
                MessageBox.Show("Please input user/password for authentication!");
                TextBoxUser.Focus();
                return false;
            }

            return true;
        }

        private void _disableControlForSending(bool isDisabled)
        {
            ButtonSend.Enabled = !isDisabled;
            ButtonSimpleSend.Enabled = !isDisabled;

            ButtonAddAttachment.Enabled = !isDisabled;
            ButtonClearAttachments.Enabled = !isDisabled;

            ButtonAddRecipient.Enabled = !isDisabled;
            ButtonClearRecipients.Enabled = !isDisabled;

            CheckBoxTestRecipient.Enabled = !isDisabled;
            CheckBoxAuth.Enabled = !isDisabled;
            CheckBoxSsl.Enabled = !isDisabled;

            TextBoxFrom.Enabled = !isDisabled;
            TextBoxSubject.Enabled = !isDisabled;
            ComboBoxEncoding.Enabled = !isDisabled;
            ComboBoxPorts.Enabled = !isDisabled;
            ComboBoxProtocols.Enabled = !isDisabled;

            ButtonCancel.Enabled = isDisabled;
        }

        #endregion

        #region Statistic Counter

        private void _resetStatisticCounter()
        {
            _total = ListViewTo.Items.Count;
            _succeeded = 0;
            _failed = 0;

            for (int i = 0; i < _total; i++)
            {
                _crossThreadUpdateRecipientStatus(i, "Ready");
            }

            ListViewTo.TopItem = ListViewTo.Items[0];
        }

        private void _updateStatisticCounter()
        {
            StatusBarSend.Text = string.Format("Total {0}, Finished {1}, Succeeded {2}, Failed {3}",
                _total, _succeeded + _failed, _succeeded, _failed);

            Application.DoEvents();
        }

        private delegate void UpdateResultCounterDelegate(bool isSucceeded);
        private void _crossThreadUpdateStatisticCounter(bool isSucceeded)
        {
            if (InvokeRequired)
            {
                object[] args = new object[1];
                args[0] = isSucceeded;
                UpdateResultCounterDelegate d = new UpdateResultCounterDelegate(_updateResultCounter);
                BeginInvoke(d, args);
            }
            else
            {
                _updateResultCounter(isSucceeded);
            }
        }

        private void _updateResultCounter(bool isSucceeded)
        {
            if (isSucceeded)
            {
                _succeeded++;
            }
            else
            {
                _failed++;
            }

            _updateStatisticCounter();
        }

        #endregion

        #region Create SmtpMail and SmtpServer instance based on Settings of Form Controls

        // You can even use different server and add different attachment based on recipient address.
        private SmtpMail _createMail(SmtpServer server, string recipientName, string recipientAddress)
        {
            // For evaluation usage, please use "TryIt" as the license code, otherwise the 
            // "invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            // "trial version expired" exception will be thrown.

            // For licensed uasage, please use your license code instead of "TryIt", then the object
            // will never expire
            SmtpMail mail = new SmtpMail("TryIt");

            mail.From = TextBoxFrom.Text;
            mail.To.Add(new MailAddress(recipientName, recipientAddress));

            mail.Subject = TextBoxSubject.Text;
            mail.Charset = _charsets[ComboBoxEncoding.SelectedIndex, 1];

            //replace keywords in body text.
            string body = TextBoxBody.Text;
            body = body.Replace("[$subject]", mail.Subject);
            body = body.Replace("[$from]", mail.From.ToString());
            body = body.Replace("[$name]", recipientName);
            body = body.Replace("[$address]", recipientAddress);

            mail.TextBody = body;

            int count = _attachments.Count;
            for (int i = 0; i < count; i++)
            {
                mail.AddAttachment(_attachments[i] as string);
            }

            if (server == null || string.IsNullOrEmpty(server.Server))
            {
                // To send email to the recipient directly(simulating the smtp server), 
                // please add a Received header, 
                // otherwise, many anti-spam filter will make it as junk email.
                // we don't suggest that you send email directly without SMTP server.
                // Most email providers will reject your message or detet it as junk email.
                System.Globalization.CultureInfo cur = new System.Globalization.CultureInfo("en-US");
                string gmtDateTime = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur);
                gmtDateTime.Remove(gmtDateTime.Length - 3, 1);
                string receivedHeader = string.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
                    server.HeloDomain,
                    gmtDateTime);

                mail.Headers.Insert(0, new HeaderItem("Received", receivedHeader));
            }

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
            if (!_verifyInput())
            {
                return;
            }

            _disableControlForSending(true);
            _isCancelSending = false;

            _resetStatisticCounter();
            _updateStatisticCounter();

            SendThreadPool mailThreadPool = new SendThreadPool();
            mailThreadPool.UpdateRecipientStatus = _crossThreadUpdateRecipientStatus;
            mailThreadPool.UpdateResultCounter = _crossThreadUpdateStatisticCounter;

            mailThreadPool.Reset((int)NumericUpDownConnections.Value, 0);
            int recipientIndex = 0;

            while (recipientIndex < _total && !_isCancelSending)
            {
                ListViewItem item = ListViewTo.Items[recipientIndex];
                string recipientName = item.Text;
                string recipientAddress = item.SubItems[1].Text;

                SmtpServer server = _createSmtpServer();
                SmtpMail mail = _createMail(server, recipientName, recipientAddress);

                while (!mailThreadPool.SubmitMessage(server, 
                    mail, 
                    CheckBoxTestRecipient.Checked, 
                    recipientIndex))
                {
                    Application.DoEvents();
                }

                if(_isCancelSending)
                {
                    break;
                }

                recipientIndex++;
            }

            if (_isCancelSending)
            {
                mailThreadPool.CancelAll();
                for (; recipientIndex < _total; recipientIndex++)
                {
                    _crossThreadUpdateRecipientStatus(recipientIndex, "Operation was cancelled");
                }
            }

            while (mailThreadPool.UnfinishedMessages != 0)
            {
                Application.DoEvents();
            }

            _disableControlForSending(false);
        }

        #region Add/Clear Attachments
        private void ButtonAddAttachment_Click(object sender, System.EventArgs e)
        {
            attachmentDlg.Reset();
            attachmentDlg.Multiselect = true;
            attachmentDlg.CheckFileExists = true;
            attachmentDlg.CheckPathExists = true;

            if (attachmentDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] attachments = attachmentDlg.FileNames;
            for (int i = 0; i < attachments.Length; i++)
            {
                string fileName = attachments[i];
                _attachments.Add(fileName);

                int pos = fileName.LastIndexOf("\\");
                if (pos != -1)
                {
                    fileName = fileName.Substring(pos + 1);
                }

                TextBoxAttachments.Text += fileName;
                TextBoxAttachments.Text += ";";
            }
        }

        private void ButtonClearAttachments_Click(object sender, System.EventArgs e)
        {
            _attachments.Clear();
            TextBoxAttachments.Text = "";
        }

        #endregion

        private void ButtonAddRecipient_Click(object sender, System.EventArgs e)
        {
            using (FormRecipient formRecipient = new FormRecipient())
            {
                if (formRecipient.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                string recipientName = formRecipient.TextBoxName.Text.Trim();
                string recipientAddress = formRecipient.TextBoxAddress.Text.Trim();
               // for (int i = 0; i < 20; i++) // this is for test
                {
                    ListViewItem item = ListViewTo.Items.Add(recipientName);
                    item.SubItems.Add(recipientAddress);
                    item.SubItems.Add("Ready");
                }
            }
        }

        private void ButtonClearRecipients_Click(object sender, System.EventArgs e)
        {
            ListViewTo.Items.Clear();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            _isCancelSending = true;
            ButtonCancel.Enabled = false;
        }

        private void CheckBoxTestRecipient_CheckedChanged(object sender, System.EventArgs e)
        {
            TextBoxServer.Enabled = (!CheckBoxTestRecipient.Checked);
            ComboBoxProtocols.SelectedIndex = 0;
            ComboBoxProtocols.Enabled = (!CheckBoxTestRecipient.Checked);
        }

        #region Send Mass E-mail with Simple Code(single thread)

        #region	EASendMail EventHandler for Simple Send

        void OnIdle(object sender, ref bool cancel)
        {
            cancel = _isCancelSending;
            if (!cancel)
            {
                Application.DoEvents();//waiting server reponse or connecting server.
            }
        }

        void OnConnected(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            _crossThreadUpdateRecipientStatus(recipientIndex, "Connected");
            cancel = _isCancelSending;
        }

        void OnSendingDataStream(object sender, int sent, int total, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            _crossThreadUpdateRecipientStatus(recipientIndex, 
                (sent != total) ?
                string.Format("Sending {0}/{1} ... ", sent, total) :
                "Disconnecting ..."
                );

            cancel = _isCancelSending;
        }

        void OnAuthorized(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            _crossThreadUpdateRecipientStatus(recipientIndex, "Authorized");
            cancel = _isCancelSending;
        }

        void OnSecuring(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            _crossThreadUpdateRecipientStatus(recipientIndex, "Securing ...");
            cancel = _isCancelSending;
        }

        #endregion

        private void ButtonSimpleSend_Click(object sender, System.EventArgs e)
        {
            if (!_verifyInput())
            {
                return;
            }

            MessageBox.Show(
                "Simple Send will send email with single thread, the code is vey simple.\r\nIf you don't want the extreme performance, the code is recommended to beginer!");

            _disableControlForSending(true);
            _isCancelSending = false;

            _resetStatisticCounter();
            _updateStatisticCounter();

            int recipientIndex = 0;
            while (recipientIndex < _total && !_isCancelSending)
            {
                try
                {
                    ListViewItem item = ListViewTo.Items[recipientIndex];
                    string recipientName = item.Text;
                    string recipientAddress = item.SubItems[1].Text;

                    SmtpServer server = _createSmtpServer();
                    SmtpMail mail = _createMail(server, recipientName, recipientAddress);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Tag = recipientIndex;

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

                    // To generate a log file for SMTP transaction, please use
                    // smtp.LogFileName = "c:\\my folder\\smtp.txt"; // my folder must be an existed folder.
                    // you can use different log file name for different recipient.

                    _crossThreadUpdateRecipientStatus(recipientIndex, "Connecting...");
                    if (!CheckBoxTestRecipient.Checked)
                    {
                        smtp.SendMail(server, mail);
                        _crossThreadUpdateRecipientStatus(recipientIndex, "Completed");
                    }
                    else
                    {
                        smtp.TestRecipients(server, mail);
                        _crossThreadUpdateRecipientStatus(recipientIndex, "PASS");
                    }

                    _succeeded++;
                }
                catch (Exception exp)
                {
                    _crossThreadUpdateRecipientStatus(recipientIndex, string.Format("Error: {0}", exp.Message));
                    _failed++;
                }
                finally
                {
                    _updateStatisticCounter();
                }

                recipientIndex++;
            }

            if (_isCancelSending)
            {
                for (; recipientIndex < _total; recipientIndex++)
                {
                    _crossThreadUpdateRecipientStatus(recipientIndex, "Operation was cancelled");
                }
            }

            _disableControlForSending(false);
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

        int _buttonSendTopOffset = 0;
        int _buttonSendLeftOffset = 0;
        int _buttonSimpleSendLeftOffset = 0;
        int _buttonCancelLeftOffset = 0;

        private void _initControlOffset()
        {
            // no smaller than design time size
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
            _isFormLoaded = true;

            _textBoxAttachmentsWidthOffset = this.Width - TextBoxAttachments.Width;

            _textBoxBodyHeightOffset = this.Height - TextBoxBody.Height;
            _buttonSendTopOffset = this.Height - ButtonSend.Top;

            _textBoxFromWidthOffset = this.Width - TextBoxFrom.Width;

            _buttonAddAttachmentLeftOffset = this.Width - ButtonAddAttachment.Left;
            _buttonClearAttachmentsLeftOffset = this.Width - ButtonClearAttachments.Left;

            _serverGroupBoxOffset = this.Width - GroupBoxServer.Left;
            _buttonSendLeftOffset = this.Width - ButtonSend.Left;
            _buttonSimpleSendLeftOffset = this.Width - ButtonSimpleSend.Left;
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

            ButtonSend.Top = this.Height - _buttonSendTopOffset;
            ButtonCancel.Top = ButtonSend.Top;
            ButtonSimpleSend.Top = ButtonSend.Top;

            GroupBoxServer.Left = this.Width - _serverGroupBoxOffset;

            ButtonSend.Left = this.Width - _buttonSendLeftOffset;
            ButtonSimpleSend.Left = this.Width - _buttonSimpleSendLeftOffset;
            ButtonCancel.Left = this.Width - _buttonCancelLeftOffset;

            TextBoxFrom.Width = this.Width - _textBoxFromWidthOffset;
            TextBoxSubject.Width = TextBoxFrom.Width;
            ListViewTo.Width = TextBoxFrom.Width;
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

    }
}

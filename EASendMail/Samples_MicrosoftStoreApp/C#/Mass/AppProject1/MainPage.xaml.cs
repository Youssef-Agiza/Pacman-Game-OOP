using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading;
using System.Threading.Tasks;

using EASendMail;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppProject1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public class RecipientData : System.ComponentModel.INotifyPropertyChanged
        {
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }

            public RecipientData(string address, string status, int index)
            {
                _address = address;
                _index = index;
                _status = status;

            }
            private string _address = "";
            private string _status = "";
            private int _index = 0;

            public string Address
            {
                get
                {
                    return _address;
                }
            }

            public string Index
            {
                get
                {
                    if (_index == 0)
                    {
                        return "Seq";
                    }
                    else
                    {
                        return _index.ToString();
                    }
                }
            }

            public string Status
            {
                get
                {
                    return _status;
                }
                set
                {
                    if (this._status != value)
                    {
                        this._status = value.Trim("\r\n".ToArray());
                        this.OnPropertyChanged("Status");
                    }
                }
            }

            public double Width
            {
                get
                {
                    return Window.Current.Bounds.Width;
                }
            }

            public SolidColorBrush Color
            {
                get
                {
                    if (_index == 0)
                        return new SolidColorBrush(Windows.UI.Colors.LightGray);

                    return new SolidColorBrush(Windows.UI.Colors.White);
                }
            }
        }

        private bool _isHtmlInited = false;
        private List<string> _attachments = new List<string>();
        private CancellationTokenSource _cancellationToken = null;

        private int _total = 0;
        private int _success = 0;
        private int _failed = 0;

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextEditor.Height = Editor.ActualHeight - EditorMenu.ActualHeight;
            HtmlEditor.Height = Editor.ActualHeight - EditorMenu.ActualHeight;
            if (this.ActualWidth < 600)
            {
                IObservableVector<ICommandBarElement> commands = EditorMenu.PrimaryCommands;
                while (commands.Count > 5)
                {
                    ICommandBarElement item = commands[commands.Count - 1];
                    commands.RemoveAt(commands.Count - 1);

                    EditorMenu.SecondaryCommands.Insert(0, item);
                }
            }
            else
            {
                IObservableVector<ICommandBarElement> commands = EditorMenu.SecondaryCommands;
                while (commands.Count > 0)
                {
                    ICommandBarElement item = commands[0];
                    commands.RemoveAt(0);
                    EditorMenu.PrimaryCommands.Add(item);
                }
            }
        }

        private void RootPage_Loaded(object sender, RoutedEventArgs e)
        {
            TextEditor.Text = "Hi,\r\n\r\nThis is a simple test email sent from C# + Universal Windows Platform (UWP) project.\r\nPlease do not reply.";
        }

        #region Html Editor
        private async void CheckHtml_Toggled(object sender, RoutedEventArgs e)
        {
            if (CheckHtml.IsOn == true)
            {
                TextEditor.Visibility = Visibility.Collapsed;
                HtmlEditor.Visibility = Visibility.Visible;
                if (!_isHtmlInited)
                {
                    _isHtmlInited = true;
                    HtmlEditor.Navigate(new Uri("ms-appx-web:///Assets/Editor.html"));
                }
                else
                {
                    await HtmlEditor.InvokeScriptAsync("setText", new string[] { TextEditor.Text });
                }
            }
            else
            {
                TextEditor.Visibility = Visibility.Visible;
                HtmlEditor.Visibility = Visibility.Collapsed;
                TextEditor.Text = await HtmlEditor.InvokeScriptAsync("getText", null);
            }
        }

        private async void HtmlEditor_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string cmd = "document.designMode = \"On\";"
                + "document.contentEditable = true;"
                + "document.body.innerHTML =\"<div>&nbsp;</div>\";"
                + "document.body.style.fontFamily = \"Calibri\";"
                + "document.body.style.fontSize = \"15pt\";"
                + "document.charset = \"utf-8\";";

            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            await HtmlEditor.InvokeScriptAsync("setText", new string[] { TextEditor.Text });
        }

        private async void FontMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            string cmd = string.Format(
                "document.execCommand(\"fontname\", false, \"{0}\");", item.Text);

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private async void FontSizeMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            string cmd = string.Format(
                "document.execCommand(\"fontsize\", false, \"{0}\");", item.Text);

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private async void FontStyle_Click(object sender, RoutedEventArgs e)
        {
            HtmlEditor.Focus(FocusState.Programmatic);
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            string cmd = string.Format(
                "document.execCommand(\"{0}\", false, \"\");", item.Text);

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });
            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private void AttachMenuFlyout_Opening(object sender, object e)
        {
            MenuFlyout attachMenu = sender as MenuFlyout;
            IList<MenuFlyoutItemBase> items = attachMenu.Items;
            while (items.Count > 3)
            {
                items.RemoveAt(3);
            }

            for (int i = 0; i < _attachments.Count; i++)
            {
                MenuFlyoutItem item = new MenuFlyoutItem();
                item.Text = _attachments[i];
                items.Add(item);
            }

            items[1].IsEnabled = (_attachments.Count > 0);
        }

        private async void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add("*");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files.Count == 0)
                return;

            foreach (StorageFile file in files)
            {
                _attachments.Add(file.Path);
            }
        }

        private void RemoveAttach_Click(object sender, RoutedEventArgs e)
        {
            _attachments.Clear();
        }

        private async void StoreSelection(object sender, RoutedEventArgs e)
        {
            await HtmlEditor.InvokeScriptAsync("storeSelection", null);
        }

        private void ColorsMenuFlyout_Opening(object sender, object e)
        {
            MenuFlyout colorsMenu = sender as MenuFlyout;
            colorsMenu.Items.Clear();
            var colors = typeof(Windows.UI.Colors).GetRuntimeProperties();
            foreach (var color in colors)
            {
                MenuFlyoutItem item = new MenuFlyoutItem();
                item.Text = color.Name;

                Windows.UI.Color c = (Windows.UI.Color)color.GetValue(null);
                item.DataContext = string.Format("#{0}{1}{2}", c.R.ToString("x2"), c.G.ToString("x2"), c.B.ToString("x2"));

                item.Background = new SolidColorBrush(c);

                item.FontFamily = new FontFamily("Segoe UI");
                item.FontSize = 15;
                item.Height = 40;
                item.Margin = new Thickness(0, 1, 0, 0);

                item.Click += ColorMenu_Click;
                colorsMenu.Items.Add(item);
            }
        }

        private async void ColorMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;

            string color = item.DataContext as string;
            string cmd = string.Format(
                "document.execCommand(\"ForeColor\", false, \"{0}\");", color);

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private async void Align_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            string cmd = string.Format(
                "document.execCommand(\"Justify{0}\", false, \"\");", item.Text);

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private int _listStyleIndex = 0;
        private async void ChangeList_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton item = sender as AppBarButton;

            string[] listcommand = { "InsertUnorderedList", "InsertOrderedList", "InsertOrderedList" };

            string cmd = string.Format(
                "document.execCommand(\"{0}\", false, \"\");", listcommand[_listStyleIndex]);
            _listStyleIndex++;
            if (_listStyleIndex >= 3)
                _listStyleIndex = 0;

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private async void InsertLink_Click(object sender, RoutedEventArgs e)
        {
            string cmd = string.Format(
                "document.execCommand(\"CreateLink\", false, \"{0}\");", TextLink.Text);

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("eval",
                new string[] { cmd });

            FlyoutLink.Hide();
            HtmlEditor.Focus(FocusState.Programmatic);
        }

        private async void Insert_Image(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".gif");
            openPicker.FileTypeFilter.Add(".bmp");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file == null)
                return;

            string ext = file.Path;
            int pos = ext.LastIndexOf('.');
            if (pos != -1)
            {
                ext = ext.Substring(pos + 1);
            }

            string ct = "image/jpeg";
            if (string.Compare(ext, "png", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/png";
            }
            else if (string.Compare(ext, "gif", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/gif";
            }
            else if (string.Compare(ext, "bmp", StringComparison.OrdinalIgnoreCase) == 0)
            {
                ct = "image/bmp";
            }

            string errorDescription = "";
            string dataSource = "";
            try
            {
                StorageFile f = await StorageFile.GetFileFromPathAsync(file.Path);
                using (var fs = await f.OpenStreamForReadAsync())
                { 
                    byte[] buffer = new byte[fs.Length];
                    int readSize = await fs.ReadAsync(buffer, 0, (int)fs.Length);
                    dataSource = Convert.ToBase64String(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ep)
            {
                errorDescription = ep.Message;
            }

            if (errorDescription.Length > 0)
            {
                MessageDialog dlg = new MessageDialog(errorDescription);
                await dlg.ShowAsync();
                return;
            }

            await HtmlEditor.InvokeScriptAsync("restoreSelection", null);
            await HtmlEditor.InvokeScriptAsync("insertImage", new string[] { file.Path, file.Name, dataSource, ct });
            HtmlEditor.Focus(FocusState.Programmatic);
        }
        #endregion

        #region EASendMail Event Handler
        private void OnSecuring(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            SmtpClient smtp = sender as SmtpClient;
            int index = (int)smtp.Tag;
            UpdateRecipientItem(index, "Securing ... ");
        }

        private void OnAuthorized(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            SmtpClient smtp = sender as SmtpClient;
            int index = (int)smtp.Tag;
            UpdateRecipientItem(index, "Authorized");
        }

        public void OnConnected(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            SmtpClient smtp = sender as SmtpClient;
            int index = (int)smtp.Tag;
            UpdateRecipientItem(index, "Connected");
        }

        public void OnSendingDataStream(
            object sender,
            SmtpDataStreamEventArgs e
        )
        {
            SmtpClient smtp = sender as SmtpClient;
            int index = (int)smtp.Tag;
            UpdateRecipientItem(index, string.Format("{0}/{1} sent", e.Sent, e.Total));
        }

        #endregion

        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            _total = 0; _success = 0; _failed = 0;
            if (TextFrom.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input from address!");
                await dlg.ShowAsync();
                TextFrom.Text = "";
                TextFrom.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            if (TextTo.Text.Trim("\r\n \t".ToCharArray()).Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input a recipient at least!");
                await dlg.ShowAsync();
                TextTo.Text = "";
                TextTo.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            if (TextServer.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input server address!");
                await dlg.ShowAsync();
                TextServer.Text = "";
                TextServer.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            bool isUserAuthencation = CheckAuthentication.IsOn;
            if (isUserAuthencation)
            {
                if (TextUser.Text.Trim().Length == 0)
                {
                    MessageDialog dlg = new MessageDialog("Please input user name!");
                    await dlg.ShowAsync();
                    TextUser.Text = "";
                    TextUser.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                    return;
                }

                if (TextPassword.Password.Trim().Length == 0)
                {
                    MessageDialog dlg = new MessageDialog("Please input password!");
                    await dlg.ShowAsync();
                    TextPassword.Password = "";
                    TextPassword.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                    return;
                }
            }

            ButtonSend.IsEnabled = false;
            ButtonSend.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Visible;
            ButtonCancel.IsEnabled = true;
            ButtonClose.Visibility = Visibility.Collapsed;
            ButtonClose.IsEnabled = false;
            ButtonAttach.Visibility = Visibility.Collapsed;

            ListRecipients.Items.Clear();
            PageViewer.Visibility = Visibility.Collapsed;
            StatusViewer.Visibility = Visibility.Visible;

            _cancellationToken = new CancellationTokenSource();

            List<Task> tasks = new List<Task>();

            string[] toLines = TextTo.Text.Trim("\r\n \t".ToCharArray()).Split("\n".ToCharArray());
            List<string> recipients = new List<string>();

            for (int i = 0; i < toLines.Length; i++)
            {
                string address = toLines[i].Trim("\r\n \t".ToCharArray());
                if (address.Length > 0)
                {
                    recipients.Add(address);
                }
            }

            int n = recipients.Count;
            toLines = recipients.ToArray();

            _total = n;
            TextStatus.Text = string.Format("Total {0}, success: {1}, failed {2}",
                _total, _success, _failed);

            ButtonCancel.IsEnabled = true;

            n = 0;
            ListRecipients.Items.Add(new RecipientData("Email", "Status", 0));
            n++;
            for (int i = 0; i < toLines.Length; i++)
            {
                int maxThreads = (int)WorkerThreads.Value;
                while (tasks.Count >= maxThreads)
                {
                    Task[] currentTasks = tasks.ToArray();
                    Task taskFinished = await Task.WhenAny(currentTasks);
                    tasks.Remove(taskFinished);

                    TextStatus.Text = string.Format(
                        "Total {0}, success: {1}, failed {2}",
                        _total, _success, _failed
                        );
                }

                string addr = toLines[i];
                int index = n;
                ListRecipients.Items.Add(new RecipientData(addr, "Queued", n));

                if (_cancellationToken.Token.IsCancellationRequested)
                {
                    n++;
                    UpdateRecipientItem(index, "Operation was cancelled!");
                    continue;
                }

                SmtpServer server = new SmtpServer(TextServer.Text);
                int[] ports = new int[] { 25, 587, 465 };
                server.Port = ports[ListPorts.SelectedIndex];

                bool isSslConnection = CheckSsl.IsOn;
                if (isSslConnection)
                {
                    // use SSL/TLS based on server port
                    server.ConnectType = SmtpConnectType.ConnectSSLAuto;
                }
                else
                {
                    // Most mordern SMTP servers require SSL/TLS connection now
                    // ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
                    server.ConnectType = SmtpConnectType.ConnectTryTLS;
                }

                server.Protocol = (ServerProtocol)ListProtocols.SelectedIndex;
                if (isUserAuthencation)
                {
                    server.User = TextUser.Text;
                    server.Password = TextPassword.Password;
                }

                // For evaluation usage, please use "TryIt" as the license code, otherwise the 
                // "Invalid License Code" exception will be thrown. However, the trial object only can be used 
                // with developer license

                // For licensed usage, please use your license code instead of "TryIt", then the object
                // can used with published windows store application.
                SmtpMail mail = new SmtpMail("TryIt");

                mail.From = new MailAddress(TextFrom.Text);
                // If your Exchange Server is 2007 and used Exchange Web Service protocol, please add the following line;
                // oMail.Headers.RemoveKey("From");
                mail.To = new AddressCollection(addr);
                mail.Subject = TextSubject.Text;

                string bodyText = "";
                bool htmlBody = false;
                if (CheckHtml.IsOn)
                {
                    bodyText = await HtmlEditor.InvokeScriptAsync("getHtml", null);
                    htmlBody = true;
                }
                else
                {
                    bodyText = TextEditor.Text;
                }

                int count = _attachments.Count;
                string[] attachments = new string[count];
                for (int x = 0; x < count; x++)
                {
                    attachments[x] = _attachments[x];
                }

                Task task = Task.Factory.StartNew(() =>
                    SubmitMail(server, mail, attachments, bodyText, htmlBody, index).Wait()
                );

                tasks.Add(task);
                n++;
            }

            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks.ToArray());
            }

            TextStatus.Text = string.Format("Total {0}, success: {1}, failed {2}",
                   _total, _success, _failed);

            ButtonSend.IsEnabled = false;
            ButtonSend.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonCancel.IsEnabled = false;
            ButtonClose.Visibility = Visibility.Visible;
            ButtonClose.IsEnabled = true;
            ButtonAttach.Visibility = Visibility.Collapsed;
        }

        private void UpdateRecipientItem(int index, string status)
        {
            if (this.Dispatcher.HasThreadAccess)
            {
                var data = ListRecipients.Items[index] as RecipientData;
                data.Status = status;
                return;
            }

            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {
                        var data = ListRecipients.Items[index] as RecipientData;
                        data.Status = status;
                    }).AsTask().Wait();

        }

        private async Task SubmitMail(
             SmtpServer server, SmtpMail mail, string[] attachments,
             string bodyText, bool htmlBody, int index)
        {

            SmtpClient smtp = null;
            try
            {
                smtp = new SmtpClient();

                // add event handler
                smtp.Authorized += OnAuthorized;
                smtp.Connected += OnConnected;
                smtp.Securing += OnSecuring;
                smtp.SendingDataStream += OnSendingDataStream;

                UpdateRecipientItem(index, "Preparing ...");

                if (!htmlBody)
                {
                    mail.TextBody = bodyText;
                }
                else
                {
                    string html = bodyText;
                    html = "<html><head><meta charset=\"utf-8\" /></head><body style=\"font-family:Calibri;font-size: 15px;\">" + html + "<body></html>";
                    await mail.ImportHtmlAsync(html,
                        Windows.ApplicationModel.Package.Current.InstalledLocation.Path,
                        ImportHtmlBodyOptions.ErrorThrowException | ImportHtmlBodyOptions.ImportLocalPictures
                        | ImportHtmlBodyOptions.ImportHttpPictures | ImportHtmlBodyOptions.ImportCss);
                }

                int count = attachments.Length;
                for (int i = 0; i < count; i++)
                {
                    await mail.AddAttachmentAsync(attachments[i]);
                }

                UpdateRecipientItem(index, string.Format("Connecting {0} ...", server.Server));
                smtp.Tag = index;

                // You can genereate a log file by the following code.
                // smtp.LogFileName = "ms-appdata:///local/smtp.txt";

                IAsyncAction asynCancelSend = smtp.SendMailAsync(server, mail);
                _cancellationToken.Token.Register(() => asynCancelSend.Cancel());
                await asynCancelSend;

                Interlocked.Increment(ref _success);
                UpdateRecipientItem(index, "Completed");

            }
            catch (Exception ep)
            {
                smtp.Close();
                string errDescription = ep.Message;
                UpdateRecipientItem(index, errDescription);
                Interlocked.Increment(ref _failed);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonCancel.IsEnabled = false;
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel(true);
            }
        }

        bool _autoChangeServer = true;
        bool _autoChangeUser = true;

        private void TextServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeSettingforWellKnownServer(TextServer.Text);
        }

        private void ChangeSettingforWellKnownServer(string server)
        {
            if (string.Compare(server, "smtp.gmail.com", true) == 0 ||
                string.Compare(server, "smtp.live.com", true) == 0 ||
                string.Compare(server, "smtp.mail.yahoo.com", true) == 0 ||
                string.Compare(server, "smtp.office365.com", true) == 0 ||
                string.Compare(server, "smtp.aol.com", true) == 0)
            {
                ListPorts.SelectedIndex = 1; //587 port, you can also use 25, 465
                CheckSsl.IsOn = true;
                CheckAuthentication.IsOn = true;
            }
        }

        private void ListProtocols_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ListProtocols.SelectedIndex)
            {
                case 1:
                    CheckAuthentication.IsOn = true;
                    CheckSsl.IsOn = true;
                    ListPorts.IsEnabled = false;
                    break;
                case 2:
                    CheckAuthentication.IsOn = true;
                    ListPorts.IsEnabled = false;
                    break;
                default:
                    ListPorts.IsEnabled = true;
                    break;
            }
        }

        private void TextFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            var address = new MailAddress(TextFrom.Text);
            string domain = address.GetAddressDomain();

            if (_autoChangeUser)
            {
                TextUser.Text = address.Address;
            }

            if (_autoChangeServer)
            {
                if (string.Compare(domain, "hotmail.com", true) == 0)
                {
                    TextServer.Text = "smtp.live.com";
                }
                else if (string.Compare(domain, "gmail.com", true) == 0)
                {
                    TextServer.Text = "smtp.gmail.com";
                }
                else if (string.Compare(domain, "yahoo.com", true) == 0)
                {
                    TextServer.Text = "smtp.mail.yahoo.com";
                }
                else if (string.Compare(domain, "aol.com", true) == 0)
                {
                    TextServer.Text = "smtp.aol.com";
                }
            }
        }

        private void TextUser_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            _autoChangeUser = string.IsNullOrWhiteSpace(TextUser.Text);
        }

        private void TextServer_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            _autoChangeServer = string.IsNullOrWhiteSpace(TextServer.Text);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonSend.IsEnabled = true;
            ButtonSend.Visibility = Visibility.Visible;
            ButtonCancel.Visibility = Visibility.Collapsed;
            ButtonCancel.IsEnabled = false;
            ButtonClose.Visibility = Visibility.Collapsed;
            ButtonClose.IsEnabled = false;
            ButtonAttach.Visibility = Visibility.Visible;

            PageViewer.Visibility = Visibility.Visible;
            StatusViewer.Visibility = Visibility.Collapsed;
        }
    }
}

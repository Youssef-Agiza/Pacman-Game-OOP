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

        int _listMenuIndex = 0;
        bool _isHtmlInited = false;
        List<string> _attachments = new List<string>();
        IAsyncAction _asyncCall = null;

        bool _autoChangeServer = true;
        bool _autoChangeUser = true;

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
                var item = new MenuFlyoutItem();
                item.Text = _attachments[i];
                items.Add(item);
            }

            items[1].IsEnabled = (_attachments.Count > 0);
        }

        private async void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
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
                var item = new MenuFlyoutItem();
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

        private async void ChangeList_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton item = sender as AppBarButton;

            string[] listcommand = { "InsertUnorderedList", "InsertOrderedList", "InsertOrderedList" };
            string cmd = string.Format(
                "document.execCommand(\"{0}\", false, \"\");", listcommand[_listMenuIndex]);

            _listMenuIndex++;
            if (_listMenuIndex >= 3)
            { 
                _listMenuIndex = 0;
            }

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
            var openPicker = new FileOpenPicker();
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
            TextStatus.Text = "Securing ... ";
        }

        private void OnAuthorized(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            TextStatus.Text = "Authorized";
        }

        public void OnConnected(
            object sender,
            SmtpStatusEventArgs e
        )
        {
            TextStatus.Text = "Connected";
        }

        public void OnSendingDataStream(
            object sender,
            SmtpDataStreamEventArgs e
        )
        {
            TextStatus.Text = string.Format("{0}/{1} sent", e.Sent, e.Total);
            ProgressBarSending.Maximum = e.Total;
            ProgressBarSending.Value = e.Sent;
        }

        #endregion

        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            if (TextFrom.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input from address!");
                await dlg.ShowAsync();
                TextFrom.Text = "";
                TextFrom.Focus(FocusState.Programmatic);
                return;
            }

            if (TextTo.Text.Trim().Length == 0 && TextCc.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input a recipient at least!");
                await dlg.ShowAsync();
                TextTo.Text = ""; TextCc.Text = "";
                TextTo.Focus(FocusState.Programmatic);
                return;
            }

            if (TextServer.Text.Trim().Length == 0)
            {
                MessageDialog dlg = new MessageDialog("Please input server address!");
                await dlg.ShowAsync();
                TextServer.Text = "";
                TextServer.Focus(FocusState.Programmatic);
                return;
            }

            bool isAuthenticationRequired = CheckAuthentication.IsOn;
            if (isAuthenticationRequired)
            {
                if (TextUser.Text.Trim().Length == 0)
                {
                    MessageDialog dlg = new MessageDialog("Please input user name!");
                    await dlg.ShowAsync();
                    TextUser.Text = "";
                    TextUser.Focus(FocusState.Programmatic);
                    return;
                }

                if (TextPassword.Password.Trim().Length == 0)
                {
                    MessageDialog dlg = new MessageDialog("Please input password!");
                    await dlg.ShowAsync();
                    TextPassword.Password = "";
                    TextPassword.Focus(FocusState.Programmatic);
                    return;
                }
            }

            ButtonSend.IsEnabled = false;
            ProgressBarSending.Value = 0;
            ProgressBarSending.Visibility = Visibility.Visible;
            pageViewer.ChangeView(0, pageViewer.ScrollableHeight, 1);
            this.Focus(FocusState.Programmatic);

            try
            {
                var smtp = new SmtpClient();

                // add event handler
                smtp.Authorized += OnAuthorized;
                smtp.Connected += OnConnected;
                smtp.Securing += OnSecuring;
                smtp.SendingDataStream += OnSendingDataStream;

                var server = new SmtpServer(TextServer.Text);
                int[] ports = { 25, 587, 465 };
                server.Port = ports[ListPorts.SelectedIndex];

                bool useSslConnection = CheckSsl.IsOn;
                if (useSslConnection)
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
                if (isAuthenticationRequired)
                {
                    server.User = TextUser.Text;
                    server.Password = TextPassword.Password;
                }

                // For evaluation usage, please use "TryIt" as the license code, otherwise the 
                // "Invalid License Code" exception will be thrown. However, the trial object only can be used 
                // with developer license

                // For licensed usage, please use your license code instead of "TryIt", then the object
                // can used with published windows store application.
                var mail = new SmtpMail("TryIt");

                mail.From = new MailAddress(TextFrom.Text);
                // If your Exchange Server is 2007 and used Exchange Web Service protocol, please add the following line;
                // oMail.Headers.RemoveKey("From");
                mail.To = new AddressCollection(TextTo.Text);
                mail.Cc = new AddressCollection(TextCc.Text);
                mail.Subject = TextSubject.Text;

                if (!CheckHtml.IsOn)
                {
                    mail.TextBody = TextEditor.Text;
                }
                else
                {
                    string html = await HtmlEditor.InvokeScriptAsync("getHtml", null);
                    html = "<html><head><meta charset=\"utf-8\" /></head><body style=\"font-family:Calibri;font-size: 15px;\">" + html + "<body></html>";
                    await mail.ImportHtmlAsync(html,
                        Windows.ApplicationModel.Package.Current.InstalledLocation.Path,
                        ImportHtmlBodyOptions.ErrorThrowException | ImportHtmlBodyOptions.ImportLocalPictures
                        | ImportHtmlBodyOptions.ImportHttpPictures | ImportHtmlBodyOptions.ImportCss);
                }

                int count = _attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    await mail.AddAttachmentAsync(_attachments[i]);
                }

                ButtonCancel.IsEnabled = true;
                TextStatus.Text = string.Format("Connecting {0} ...", server.Server);
                // You can genereate a log file by the following code.
                // smtp.LogFileName = "ms-appdata:///local/smtp.txt";
                _asyncCall = smtp.SendMailAsync(server, mail);
                await _asyncCall;

                TextStatus.Text = "Completed";

            }
            catch (Exception ep)
            {
                TextStatus.Text = "Error:  " + ep.Message.TrimEnd("\r\n".ToArray());
            }

            ProgressBarSending.Visibility = Visibility.Collapsed;

            _asyncCall = null;
            ButtonSend.IsEnabled = true;
            ButtonCancel.IsEnabled = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonCancel.IsEnabled = false;
            if (_asyncCall != null)
            {
                _asyncCall.Cancel();
            }
        }

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

        // change server address by well known email domain
        private void TextFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            var address = new MailAddress(TextFrom.Text);
            string domain = address.GetAddressDomain();

            if(_autoChangeUser)
            { 
                TextUser.Text = address.Address;
            }

            if(_autoChangeServer)
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
    }
}

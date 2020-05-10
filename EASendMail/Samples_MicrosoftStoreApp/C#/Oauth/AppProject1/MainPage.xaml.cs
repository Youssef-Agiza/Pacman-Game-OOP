using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using EASendMail;
using EASendMail.Oauth;
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

        private int _listStypleIndex = 0;
        private bool _isHtmlInited = false;
        private List<string> _attachments = new List<string>();
        private IAsyncAction _asyncCancel = null;


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
            TextEditor.Text = "Hi,\r\n\r\nThis is a simple test email sent from C# + Universal Windows Platform (UWP) project.\r\nPlease do not reply.\r\n\r\n" +
                "You must apply for your client id and client secret, don't use the client id in the sample project, because it is limited now.\r\n" +
                "If you got \"This app isn't verified\" information, please click \"advanced\"->Go to... for test.";

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
                "document.execCommand(\"{0}\", false, \"\");", listcommand[_listStypleIndex]);

            _listStypleIndex++;
            if (_listStypleIndex >= 3)
            {
                _listStypleIndex = 0;
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

        private async Task _sendEmailAsync()
        {
            var smtp = new SmtpClient();

            // add event handler
            smtp.Authorized += OnAuthorized;
            smtp.Connected += OnConnected;
            smtp.Securing += OnSecuring;
            smtp.SendingDataStream += OnSendingDataStream;

            var server = new SmtpServer(TextServer.Text);
            int[] ports = new int[] { 25, 587, 465 };
            server.Port = ports[ListPorts.SelectedIndex];
            server.ConnectType = SmtpConnectType.ConnectSSLAuto;

            server.Protocol = ServerProtocol.SMTP;
            if (ListProviders.SelectedIndex == OauthProvider.MsOffice365Provider)
            {
                server.Protocol = ServerProtocol.ExchangeEWS;
            }

            server.User = _oauthWrapper.Provider.UserEmail;
            server.Password = _oauthWrapper.Provider.AccessToken;
            server.AuthType = SmtpAuthType.XOAUTH2;

            // For evaluation usage, please use "TryIt" as the license code, otherwise the 
            // "Invalid License Code" exception will be thrown. However, the trial object only can be used 
            // with developer license

            // For licensed usage, please use your license code instead of "TryIt", then the object
            // can used with published windows store application.
            var mail = new SmtpMail("TryIt");
            mail.From = new MailAddress(_oauthWrapper.Provider.UserEmail);
            var replyTo = new MailAddress(TextFrom.Text);
            if (string.Compare(replyTo.Address, _oauthWrapper.Provider.UserEmail, StringComparison.OrdinalIgnoreCase) != 0)
            {
                mail.ReplyTo = new MailAddress(TextFrom.Text);
            }

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
            TextStatus.Text = String.Format("Connecting {0} ...", server.Server);
            // You can genereate a log file by the following code.
            // oSmtp.LogFileName = "ms-appdata:///local/smtp.txt";
            _asyncCancel = smtp.SendMailAsync(server, mail);
            await _asyncCancel;

            TextStatus.Text = "Completed";
        }

        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            string errorDescription = string.Empty;

            try
            {
                await _doOauthAsync();
                TextStatus.Text = "Oauth is completed, ready to send email.";
            }
            catch (Exception ep)
            {
                errorDescription = string.Format("Failed to do oauth, exception: {0}", ep.Message);
            }

            if (!string.IsNullOrWhiteSpace(errorDescription))
            {
                await new MessageDialog(errorDescription).ShowAsync();
                TextStatus.Text = errorDescription;
                return;
            }

            if (string.IsNullOrWhiteSpace(TextTo.Text) && string.IsNullOrWhiteSpace(TextCc.Text))
            {
                MessageDialog dlg = new MessageDialog("Please input a recipient at least!");
                await dlg.ShowAsync();
                TextTo.Text = ""; TextCc.Text = "";
                TextTo.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextServer.Text))
            {
                MessageDialog dlg = new MessageDialog("Please input server address!");
                await dlg.ShowAsync();
                TextServer.Text = "";
                TextServer.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                return;
            }

            ButtonSend.IsEnabled = false;
            ProgressBarSending.Value = 0;
            ProgressBarSending.Visibility = Visibility.Visible;
            PageViewer.ChangeView(0, PageViewer.ScrollableHeight, 1);
            this.Focus(FocusState.Programmatic);

            try
            {
                await _sendEmailAsync();
            }
            catch (Exception ep)
            {
                errorDescription = string.Format("Failed to do oauth, exception: {0}", ep.Message);
            }

            if (!string.IsNullOrWhiteSpace(errorDescription))
            {
                await new MessageDialog(errorDescription).ShowAsync();
                TextStatus.Text = errorDescription;
            }

            ProgressBarSending.Visibility = Visibility.Collapsed;
            _asyncCancel = null;

            ButtonSend.IsEnabled = true;
            ButtonCancel.IsEnabled = false;
            ButtonClear.IsEnabled = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonCancel.IsEnabled = false;
            if (_asyncCancel != null)
            {
                _asyncCancel.Cancel();
            }
        }

        #region Web Oauth
        OauthDesktopWrapper _oauthWrapper = null;

        private void ListProviders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ListProviders.SelectedIndex)
            {
                case OauthProvider.GoogleProvider:
                    TextServer.Text = "smtp.gmail.com";
                    ListPorts.IsEnabled = true;
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateGoogleProvider());
                    break;
                case OauthProvider.MsLiveProvider:
                    TextServer.Text = "smtp.live.com";
                    ListPorts.IsEnabled = true;
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateMsLiveProvider());
                    break;
                case OauthProvider.MsOffice365Provider:
                    TextServer.Text = "outlook.office365.com";
                    ListPorts.IsEnabled = false;
                    _oauthWrapper = new OauthDesktopWrapper(OauthProvider.CreateMsOffice365Provider());
                    break;
                default:
                    throw new Exception("Invalid OAUTH provider!");
            }
        }

        private async Task _doOauthAsync()
        {
            // AccessToken is existed, if it is not expired, use it directly, otherwise refresh it.
            if (!string.IsNullOrEmpty(_oauthWrapper.Provider.AccessToken))
            {
                if (!_oauthWrapper.IsAccessTokenExpired)
                {
                    return;
                }

                TextStatus.Text = "Refreshing access token ...";
                try
                {
                    await _oauthWrapper.RefreshAccessTokenAsync();
                    return;
                }
                catch
                {
                    TextStatus.Text = "Failed to refresh access token, try to get a new access token ...";
                }
            }

            OauthBrowser.Navigate(
                 new Uri(_oauthWrapper.Provider.GetFullAuthUri())
                 );

            ShowOauthPanel(true);
            while (OauthViewer.Visibility != Visibility.Collapsed)
            {
                await Task.Delay(100);
            }

            TextStatus.Text = "Requesting access token ...";
            await _oauthWrapper.RequestAccessTokenAndUserEmailAsync();
        }

        private async void OauthBrowser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (OauthViewer.Visibility == Visibility.Collapsed)
                return;


            OauthProgress.Visibility = Visibility.Collapsed;
            if (!args.IsSuccess)
            {
                MessageDialog dlg = new MessageDialog(args.WebErrorStatus.ToString() + ": " + args.Uri.AbsoluteUri);
                await dlg.ShowAsync();
                ShowOauthPanel(false);
                return;
            }

            if (string.IsNullOrWhiteSpace(args.Uri.Query))
            {
                return;
            }

            string code = string.Empty;
            try
            {
                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(args.Uri.Query);
                if (ListProviders.SelectedIndex == OauthProvider.GoogleProvider)
                {
                    code = decoder.GetFirstValueByName("approvalCode");
                }
                else
                {
                    code = decoder.GetFirstValueByName("code");
                }
            }
            catch { }

            if(string.IsNullOrWhiteSpace(code))
            {
                return;
            }

            _oauthWrapper.AuthorizationCode = code;
            ShowOauthPanel(false);
        }

        private void ShowOauthPanel(bool showOauthView)
        {
            if (showOauthView)
            {
                PageViewer.Visibility = Visibility.Collapsed;
                ButtonAttachment.Visibility = Visibility.Collapsed;
                ButtonSend.Visibility = Visibility.Collapsed;
                ButtonCancel.Visibility = Visibility.Collapsed;
                ButtonClear.Visibility = Visibility.Collapsed;

                ButtonClose.Visibility = Visibility.Visible;
                OauthViewer.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonClose.Visibility = Visibility.Collapsed;
                OauthViewer.Visibility = Visibility.Collapsed;

                PageViewer.Visibility = Visibility.Visible;
                ButtonAttachment.Visibility = Visibility.Visible;
                ButtonSend.Visibility = Visibility.Visible;
                ButtonCancel.Visibility = Visibility.Visible;
                ButtonClear.Visibility = Visibility.Visible;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ShowOauthPanel(false);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ButtonClear.IsEnabled = false;
            _oauthWrapper.Provider.ClearToken();
            _oauthWrapper.AuthorizationCode = "";
        }
        #endregion


    }
}

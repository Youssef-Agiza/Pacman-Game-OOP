Imports Windows.UI.Xaml.Navigation
Imports Windows.UI.Popups
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Net
Imports EASendMail

Partial Public NotInheritable Class MainPage
    Inherits Page
    Public Sub New()
        Me.InitializeComponent()
    End Sub

    Private _listStyleIndex As Integer = 0
    Private _isHtmlInited As Boolean = False
    Private _attachments As New List(Of String)()
    Private _asyncCancel As IAsyncAction = Nothing

    Private Sub Page_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        TextEditor.Height = Editor.ActualHeight - EditorMenu.ActualHeight
        HtmlEditor.Height = Editor.ActualHeight - EditorMenu.ActualHeight

        If Me.ActualWidth < 600 Then
            Dim commands As IObservableVector(Of ICommandBarElement) = EditorMenu.PrimaryCommands
            While commands.Count > 5
                Dim item As ICommandBarElement = commands(commands.Count - 1)
                commands.RemoveAt(commands.Count - 1)
                EditorMenu.SecondaryCommands.Insert(0, item)
            End While
        Else
            Dim commands As IObservableVector(Of ICommandBarElement) = EditorMenu.SecondaryCommands
            While commands.Count > 0
                Dim item As ICommandBarElement = commands(0)
                commands.RemoveAt(0)
                EditorMenu.PrimaryCommands.Add(item)
            End While
        End If

    End Sub

    Private Sub RootPage_Loaded(sender As Object, e As RoutedEventArgs)
        TextEditor.Text = "Hi," & vbCr & vbLf & vbCr & vbLf & "This is a simple test email sent from C# + Universal Windows Platform (UWP) project." &
            vbCr & vbLf & "Please do not reply." & vbCr & vbLf & vbCr & vbLf &
            "You must apply for your client id and client secret, don't use the client id in the sample project, because sample client_id is limited." & vbCr & vbLf &
            "If you got ""This app isn't verified"" information, please click ""advanced"" -> Go to... for test."
    End Sub

#Region "Html Editor"
    Private Async Sub CheckHtml_Toggled(sender As Object, e As RoutedEventArgs)
        If CheckHtml.IsOn = True Then
            TextEditor.Visibility = Visibility.Collapsed
            HtmlEditor.Visibility = Visibility.Visible
            If Not _isHtmlInited Then
                _isHtmlInited = True
                HtmlEditor.Navigate(New Uri("ms-appx-web:///Assets/Editor.html"))
            Else
                Await HtmlEditor.InvokeScriptAsync("setText", New String() {TextEditor.Text})
            End If
        Else
            TextEditor.Visibility = Visibility.Visible
            HtmlEditor.Visibility = Visibility.Collapsed
            TextEditor.Text = Await HtmlEditor.InvokeScriptAsync("getText", Nothing)
        End If
    End Sub

    Private Async Sub HtmlEditor_NavigationCompleted(sender As WebView, args As WebViewNavigationCompletedEventArgs)
        Dim cmd As String = "document.designMode = ""On"";" +
            "document.contentEditable = true;" +
            "document.body.innerHTML =""<div>&nbsp;</div>"";" +
            "document.body.style.fontFamily = ""Calibri"";" +
            "document.body.style.fontSize = ""15pt"";" +
            "document.charset = ""utf-8"";"

        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})
        Await HtmlEditor.InvokeScriptAsync("setText", New String() {TextEditor.Text})
    End Sub

    Private Async Sub FontMenuFlyoutItem_Click(sender As Object, e As RoutedEventArgs)
        Dim item As MenuFlyoutItem = TryCast(sender, MenuFlyoutItem)
        Dim cmd As String = String.Format("document.execCommand(""fontname"", false, ""{0}"");", item.Text)

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})

        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub

    Private Async Sub FontSizeMenuFlyoutItem_Click(sender As Object, e As RoutedEventArgs)
        Dim item As MenuFlyoutItem = TryCast(sender, MenuFlyoutItem)
        Dim cmd As String = String.Format("document.execCommand(""fontsize"", false, ""{0}"");", item.Text)

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})

        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub

    Private Async Sub FontStyle_Click(sender As Object, e As RoutedEventArgs)
        HtmlEditor.Focus(FocusState.Programmatic)
        Dim item As MenuFlyoutItem = TryCast(sender, MenuFlyoutItem)
        Dim cmd As String = String.Format("document.execCommand(""{0}"", false, """");", item.Text)

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})
        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub

    Private Sub AttachMenuFlyout_Opening(sender As Object, e As Object)
        Dim attachMenu As MenuFlyout = TryCast(sender, MenuFlyout)
        Dim items As IList(Of MenuFlyoutItemBase) = attachMenu.Items
        While items.Count > 3
            items.RemoveAt(3)
        End While

        For i As Integer = 0 To _attachments.Count - 1
            Dim item As New MenuFlyoutItem()
            item.Text = _attachments(i)
            items.Add(item)
        Next

        items(1).IsEnabled = (_attachments.Count > 0)

    End Sub

    Private Async Sub AttachFile_Click(sender As Object, e As RoutedEventArgs)
        Dim openPicker As New FileOpenPicker()
        openPicker.ViewMode = PickerViewMode.List
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary
        openPicker.FileTypeFilter.Add("*")

        Dim files As IReadOnlyList(Of StorageFile) = Await openPicker.PickMultipleFilesAsync()
        If files.Count = 0 Then
            Return
        End If

        For Each file As StorageFile In files
            _attachments.Add(file.Path)
        Next
    End Sub

    Private Sub RemoveAttach_Click(sender As Object, e As RoutedEventArgs)
        _attachments.Clear()
    End Sub

    Private Async Sub StoreSelection(sender As Object, e As RoutedEventArgs)
        Await HtmlEditor.InvokeScriptAsync("storeSelection", Nothing)
    End Sub

    Private Sub ColorsMenuFlyout_Opening(sender As Object, e As Object)
        Dim colorsMenu As MenuFlyout = TryCast(sender, MenuFlyout)
        colorsMenu.Items.Clear()
        Dim colors = GetType(Windows.UI.Colors).GetRuntimeProperties()
        For Each color In colors
            Dim item As New MenuFlyoutItem()
            item.Text = color.Name

            Dim c As Windows.UI.Color = color.GetValue(Nothing)
            item.DataContext = String.Format("#{0}{1}{2}", c.R.ToString("x2"), c.G.ToString("x2"), c.B.ToString("x2"))

            item.Background = New SolidColorBrush(c)

            item.FontFamily = New FontFamily("Segoe UI")
            item.FontSize = 15
            item.Height = 40
            item.Margin = New Thickness(0, 1, 0, 0)

            AddHandler item.Click, AddressOf ColorMenu_Click
            colorsMenu.Items.Add(item)
        Next
    End Sub


    Private Async Sub ColorMenu_Click(sender As Object, e As RoutedEventArgs)
        Dim item As MenuFlyoutItem = TryCast(sender, MenuFlyoutItem)

        Dim color As String = TryCast(item.DataContext, String)
        Dim cmd As String = String.Format("document.execCommand(""ForeColor"", false, ""{0}"");", color)

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})
        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub

    Private Async Sub Align_Click(sender As Object, e As RoutedEventArgs)
        Dim item As MenuFlyoutItem = TryCast(sender, MenuFlyoutItem)
        Dim cmd As String = String.Format("document.execCommand(""Justify{0}"", false, """");", item.Text)

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})

        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub

    Private Async Sub ChangeList_Click(sender As Object, e As RoutedEventArgs)
        Dim item As AppBarButton = TryCast(sender, AppBarButton)

        Dim listcommand As String() = {"InsertUnorderedList", "InsertOrderedList", "InsertOrderedList"}

        Dim cmd As String = String.Format("document.execCommand(""{0}"", false, """");", listcommand(_listStyleIndex))
        _listStyleIndex += 1
        If _listStyleIndex >= 3 Then
            _listStyleIndex = 0
        End If

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})

        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub

    Private Async Sub InsertLink_Click(sender As Object, e As RoutedEventArgs)
        Dim cmd As String = String.Format("document.execCommand(""CreateLink"", false, ""{0}"");", TextLink.Text)

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("eval", New String() {cmd})

        FlyoutLink.Hide()
        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub


    Private Async Sub Insert_Image(sender As Object, e As RoutedEventArgs)
        Dim openPicker As New FileOpenPicker()
        openPicker.ViewMode = PickerViewMode.List
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary
        openPicker.FileTypeFilter.Add(".jpg")
        openPicker.FileTypeFilter.Add(".png")
        openPicker.FileTypeFilter.Add(".gif")
        openPicker.FileTypeFilter.Add(".bmp")

        Dim file As StorageFile = Await openPicker.PickSingleFileAsync()
        If file Is Nothing Then
            Return
        End If

        Dim ext As String = file.Path
        Dim pos As Integer = ext.LastIndexOf("."c)
        If pos <> -1 Then
            ext = ext.Substring(pos + 1)
        End If

        Dim ct As String = "image/jpeg"
        If String.Compare(ext, "png", StringComparison.OrdinalIgnoreCase) = 0 Then
            ct = "image/png"
        ElseIf String.Compare(ext, "gif", StringComparison.OrdinalIgnoreCase) = 0 Then
            ct = "image/gif"
        ElseIf String.Compare(ext, "bmp", StringComparison.OrdinalIgnoreCase) = 0 Then
            ct = "image/bmp"
        End If

        Dim dataSource As String = ""
        Dim errorDescription As String = ""

        Try
            Dim f As StorageFile = Await StorageFile.GetFileFromPathAsync(file.Path)
            Using fs As Stream = Await f.OpenStreamForReadAsync()
                Dim buffer As Byte() = New Byte(fs.Length - 1) {}
                Dim bufferSize As Integer = Await fs.ReadAsync(buffer, 0, fs.Length)
                dataSource = Convert.ToBase64String(buffer, 0, buffer.Length)
            End Using
        Catch ex As Exception
            errorDescription = ex.Message
        End Try

        If errorDescription.Length > 0 Then
            Dim dlg As New MessageDialog(errorDescription)
            Await dlg.ShowAsync()
            Return
        End If

        Await HtmlEditor.InvokeScriptAsync("restoreSelection", Nothing)
        Await HtmlEditor.InvokeScriptAsync("insertImage", New String() {file.Path, file.Name, dataSource, ct})
        HtmlEditor.Focus(FocusState.Programmatic)
    End Sub
#End Region

#Region "EASendMail Event Handler"
    Private Sub OnSecuring(sender As Object, e As SmtpStatusEventArgs)
        TextStatus.Text = "Securing ... "
    End Sub

    Private Sub OnAuthorized(sender As Object, e As SmtpStatusEventArgs)
        TextStatus.Text = "Authorized"
    End Sub

    Public Sub OnConnected(sender As Object, e As SmtpStatusEventArgs)
        TextStatus.Text = "Connected"
    End Sub

    Public Sub OnSendingDataStream(sender As Object, e As SmtpDataStreamEventArgs)
        TextStatus.Text = String.Format("{0}/{1} sent", e.Sent, e.Total)
        ProgressBarSending.Maximum = e.Total
        ProgressBarSending.Value = e.Sent
    End Sub
#End Region


    Private Async Function _sendEmailAsync() As Task

        Dim smtp = New SmtpClient()

        ' add event handler
        AddHandler smtp.Authorized, AddressOf OnAuthorized
        AddHandler smtp.Connected, AddressOf OnConnected
        AddHandler smtp.Securing, AddressOf OnSecuring
        AddHandler smtp.SendingDataStream, AddressOf OnSendingDataStream

        Dim server = New SmtpServer(TextServer.Text)
        Dim ports As Integer() = New Integer() {25, 587, 465}
        server.Port = ports(ListPorts.SelectedIndex)
        server.ConnectType = SmtpConnectType.ConnectSSLAuto
        server.Protocol = ServerProtocol.SMTP

        If ListProviders.SelectedIndex = OauthProvider.MsOffice365Provider Then
            server.Protocol = ServerProtocol.ExchangeEWS
        End If

        server.User = _oauthWrapper.Provider.UserEmail
        server.Password = _oauthWrapper.Provider.AccessToken
        server.AuthType = SmtpAuthType.XOAUTH2

        ' For evaluation usage, please use "TryIt" as the license code, otherwise the 
        ' "Invalid License Code" exception will be thrown. However, the trial object only can be used 
        ' with developer license

        ' For licensed usage, please use your license code instead of "TryIt", then the object
        ' can used with published windows store application.
        Dim mail = New SmtpMail("TryIt")
        mail.From = New MailAddress(_oauthWrapper.Provider.UserEmail)
        Dim replyTo = New MailAddress(TextFrom.Text)

        If String.Compare(replyTo.Address, _oauthWrapper.Provider.UserEmail, StringComparison.OrdinalIgnoreCase) <> 0 Then
            mail.ReplyTo = New MailAddress(TextFrom.Text)
        End If

        mail.[To] = New AddressCollection(TextTo.Text)
        mail.Cc = New AddressCollection(TextCc.Text)
        mail.Subject = TextSubject.Text

        If Not CheckHtml.IsOn Then
            mail.TextBody = TextEditor.Text
        Else
            Dim html As String = Await HtmlEditor.InvokeScriptAsync("getHtml", Nothing)
            html = "<html><head><meta charset=""utf-8"" /></head><body style=""font-family:Calibri;font-size: 15px;"">" & html & "<body></html>"
            Await mail.ImportHtmlAsync(html,
            Package.Current.InstalledLocation.Path,
            ImportHtmlBodyOptions.ErrorThrowException Or
            ImportHtmlBodyOptions.ImportLocalPictures Or
            ImportHtmlBodyOptions.ImportHttpPictures Or
            ImportHtmlBodyOptions.ImportCss)
        End If

        Dim count As Integer = _attachments.Count

        For i As Integer = 0 To count - 1
            Await mail.AddAttachmentAsync(_attachments(i))
        Next

        ButtonCancel.IsEnabled = True
        TextStatus.Text = String.Format("Connecting {0} ...", server.Server)
        _asyncCancel = smtp.SendMailAsync(server, mail)

        Await _asyncCancel
        TextStatus.Text = "Completed"
    End Function

    Private Async Sub ButtonSend_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim errorDescription As String = String.Empty

        Try
            Await _doOauthAsync()
            TextStatus.Text = "Oauth is completed, ready to send email."
        Catch ep As Exception
            errorDescription = String.Format("Failed to do oauth, exception: {0}", ep.Message)
        End Try

        If Not String.IsNullOrWhiteSpace(errorDescription) Then
            Await New MessageDialog(errorDescription).ShowAsync()
            TextStatus.Text = errorDescription
            Return
        End If

        If String.IsNullOrWhiteSpace(TextTo.Text) AndAlso String.IsNullOrWhiteSpace(TextCc.Text) Then
            Dim dlg As MessageDialog = New MessageDialog("Please input a recipient at least!")
            Await dlg.ShowAsync()
            TextTo.Text = ""
            TextCc.Text = ""
            TextTo.Focus(Windows.UI.Xaml.FocusState.Programmatic)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextServer.Text) Then
            Dim dlg As MessageDialog = New MessageDialog("Please input server address!")
            Await dlg.ShowAsync()
            TextServer.Text = ""
            TextServer.Focus(Windows.UI.Xaml.FocusState.Programmatic)
            Return
        End If

        ButtonSend.IsEnabled = False
        ProgressBarSending.Value = 0
        ProgressBarSending.Visibility = Visibility.Visible
        PageViewer.ChangeView(0, PageViewer.ScrollableHeight, 1)
        Me.Focus(FocusState.Programmatic)

        Try
            Await _sendEmailAsync()
        Catch ep As Exception
            errorDescription = String.Format("Failed to do oauth, exception: {0}", ep.Message)
        End Try

        If Not String.IsNullOrWhiteSpace(errorDescription) Then
            Await New MessageDialog(errorDescription).ShowAsync()
            TextStatus.Text = errorDescription
        End If

        ProgressBarSending.Visibility = Visibility.Collapsed
        _asyncCancel = Nothing
        ButtonSend.IsEnabled = True
        ButtonCancel.IsEnabled = False
        ButtonClear.IsEnabled = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As RoutedEventArgs)
        ButtonCancel.IsEnabled = False
        If _asyncCancel IsNot Nothing Then
            _asyncCancel.Cancel()
        End If
    End Sub

#Region "Web Oauth"

    Private _oauthWrapper As OauthDesktopWrapper = Nothing
    Private Sub ListProviders_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ListProviders.SelectionChanged
        Select Case ListProviders.SelectedIndex
            Case OauthProvider.GoogleProvider
                TextServer.Text = "smtp.gmail.com"
                ListPorts.IsEnabled = True
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateGoogleProvider())
            Case OauthProvider.MsLiveProvider
                TextServer.Text = "smtp.live.com"
                ListPorts.IsEnabled = True
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateMsLiveProvider())
            Case OauthProvider.MsOffice365Provider
                TextServer.Text = "outlook.office365.com"
                ListPorts.IsEnabled = False
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateMsOffice365Provider())
            Case Else
                Throw New Exception("Invalid OAUTH provider!")
        End Select
    End Sub

    Private Async Function _doOauthAsync() As Task

        If Not String.IsNullOrEmpty(_oauthWrapper.Provider.AccessToken) Then

            If Not _oauthWrapper.IsAccessTokenExpired Then
                Return
            End If

            TextStatus.Text = "Refreshing access token ..."

            Try
                Await _oauthWrapper.RefreshAccessTokenAsync()
                Return
            Catch
                TextStatus.Text = "Failed to refresh access token, try to get a new access token ..."
            End Try
        End If

        OauthBrowser.Navigate(New Uri(_oauthWrapper.Provider.GetFullAuthUri()))
        ShowOauthPanel(True)

        While OauthViewer.Visibility <> Visibility.Collapsed
            Await Task.Delay(100)
        End While

        TextStatus.Text = "Requesting access token ..."
        Await _oauthWrapper.RequestAccessTokenAndUserEmailAsync()
    End Function

    Private Async Sub OauthBrowser_NavigationCompleted(ByVal sender As WebView, ByVal args As WebViewNavigationCompletedEventArgs)
        If OauthViewer.Visibility = Visibility.Collapsed Then Return

        OauthProgress.Visibility = Visibility.Collapsed

        If Not args.IsSuccess Then
            Dim dlg As MessageDialog = New MessageDialog(args.WebErrorStatus.ToString() & ": " + args.Uri.AbsoluteUri)
            Await dlg.ShowAsync()
            ShowOauthPanel(False)
            Return
        End If

        If String.IsNullOrWhiteSpace(args.Uri.Query) Then
            Return
        End If

        Dim code As String = String.Empty

        Try
            Dim decoder As WwwFormUrlDecoder = New WwwFormUrlDecoder(args.Uri.Query)

            If ListProviders.SelectedIndex = OauthProvider.GoogleProvider Then
                code = decoder.GetFirstValueByName("approvalCode")
            Else
                code = decoder.GetFirstValueByName("code")
            End If

        Catch
        End Try

        If String.IsNullOrWhiteSpace(code) Then
            Return
        End If

        _oauthWrapper.AuthorizationCode = code
        ShowOauthPanel(False)
    End Sub

    Private Sub ShowOauthPanel(b As Boolean)
        If b Then
            PageViewer.Visibility = Visibility.Collapsed
            ButtonAttachment.Visibility = Visibility.Collapsed
            ButtonSend.Visibility = Visibility.Collapsed
            ButtonCancel.Visibility = Visibility.Collapsed
            ButtonClear.Visibility = Visibility.Collapsed

            ButtonClose.Visibility = Visibility.Visible
            OauthViewer.Visibility = Visibility.Visible
        Else
            ButtonClose.Visibility = Visibility.Collapsed
            OauthViewer.Visibility = Visibility.Collapsed

            PageViewer.Visibility = Visibility.Visible
            ButtonAttachment.Visibility = Visibility.Visible
            ButtonSend.Visibility = Visibility.Visible
            ButtonCancel.Visibility = Visibility.Visible
            ButtonClear.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As RoutedEventArgs)
        ShowOauthPanel(False)
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As RoutedEventArgs)
        ButtonClear.IsEnabled = False
        _oauthWrapper.Provider.ClearToken()
        _oauthWrapper.AuthorizationCode = ""
    End Sub


#End Region

End Class

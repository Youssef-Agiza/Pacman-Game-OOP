' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
Imports Windows.UI.Popups
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports System.Reflection
Imports EASendMail

Public NotInheritable Class MainPage
    Inherits Page

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
        TextEditor.Text = "Hi," & vbCr & vbLf & vbCr & vbLf &
            "This is a simple test email sent from VB + Universal Windows Platform (UWP) project." &
            vbCr & vbLf & "Please do not reply."
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

    Private Async Sub ButtonSend_Click(sender As Object, e As RoutedEventArgs)
        If TextFrom.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input from address!")
            Await dlg.ShowAsync()
            TextFrom.Text = ""
            TextFrom.Focus(FocusState.Programmatic)
            Return
        End If

        If TextTo.Text.Trim().Length = 0 AndAlso TextCc.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input a recipient at least!")
            Await dlg.ShowAsync()
            TextTo.Text = ""
            TextCc.Text = ""
            TextTo.Focus(FocusState.Programmatic)
            Return
        End If

        If TextServer.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input server address!")
            Await dlg.ShowAsync()
            TextServer.Text = ""
            TextServer.Focus(FocusState.Programmatic)
            Return
        End If

        Dim IsUserAuthenication = CheckAuthentication.IsOn
        If CheckAuthentication.IsOn Then
            If TextUser.Text.Trim().Length = 0 Then
                Dim dlg As New MessageDialog("Please input user name!")
                Await dlg.ShowAsync()
                TextUser.Text = ""
                TextUser.Focus(FocusState.Programmatic)
                Return
            End If

            If TextPassword.Password.Trim().Length = 0 Then
                Dim dlg As New MessageDialog("Please input password!")
                Await dlg.ShowAsync()
                TextPassword.Password = ""
                TextPassword.Focus(FocusState.Programmatic)
                Return
            End If
        End If

        ButtonSend.IsEnabled = False
        ProgressBarSending.Value = 0
        ProgressBarSending.Visibility = Visibility.Visible
        PageViewer.ChangeView(0, PageViewer.ScrollableHeight, 1)
        Me.Focus(FocusState.Programmatic)

        Try
            Dim oSmtp As New SmtpClient()

            ' Add event handler
            AddHandler oSmtp.Authorized, AddressOf OnAuthorized
            AddHandler oSmtp.Connected, AddressOf OnConnected
            AddHandler oSmtp.Securing, AddressOf OnSecuring
            AddHandler oSmtp.SendingDataStream, AddressOf OnSendingDataStream

            Dim oServer As New SmtpServer(TextServer.Text)
            Dim ports As Integer() = New Integer() {25, 587, 465}
            oServer.Port = ports(ListPorts.SelectedIndex)
            Dim bSSL As Boolean = CheckSsl.IsOn
            If bSSL Then
                ' use SSL/TLS based on server port
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
            Else
                ' Most mordern SMTP servers require SSL/TLS connection now
                ' ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
                oServer.ConnectType = SmtpConnectType.ConnectTryTLS
            End If

            oServer.Protocol = DirectCast(ListProtocols.SelectedIndex, ServerProtocol)

            If IsUserAuthenication Then
                oServer.User = TextUser.Text
                oServer.Password = TextPassword.Password
            End If

            ' For evaluation usage, please use "TryIt" as the license code, otherwise the 
            ' "Invalid License Code" exception will be thrown. However, the trial object only can be used 
            ' with developer license

            ' For licensed usage, please use your license code instead of "TryIt", then the object
            ' can used with published windows store application.
            Dim oMail As New SmtpMail("TryIt")

            oMail.From = New MailAddress(TextFrom.Text)
            ' If your Exchange Server is 2007 and used Exchange Web Service protocol, please add the following line;
            ' oMail.Headers.RemoveKey("From");
            oMail.To = New AddressCollection(TextTo.Text)
            oMail.Cc = New AddressCollection(TextCc.Text)
            oMail.Subject = TextSubject.Text

            If Not CheckHtml.IsOn Then
                oMail.TextBody = TextEditor.Text
            Else
                Dim html As String = Await HtmlEditor.InvokeScriptAsync("getHtml", Nothing)
                html = "<html><head><meta charset=""utf-8"" /></head><body style=""font-family:Calibri;font-size: 15px;"">" & html & "<body></html>"
                Await oMail.ImportHtmlAsync(html, Package.Current.InstalledLocation.Path,
                                            ImportHtmlBodyOptions.ErrorThrowException Or
                                            ImportHtmlBodyOptions.ImportLocalPictures Or
                                            ImportHtmlBodyOptions.ImportHttpPictures Or
                                            ImportHtmlBodyOptions.ImportCss)
            End If

            Dim count As Integer = _attachments.Count
            For i As Integer = 0 To count - 1
                Await oMail.AddAttachmentAsync(_attachments(i))
            Next
            ButtonCancel.IsEnabled = True

            TextStatus.Text = String.Format("Connecting {0} ...", oServer.Server)
            ' You can genereate a log file by the following code.
            ' oSmtp.LogFileName = "ms-appdata:///local/smtp.txt";
            _asyncCancel = oSmtp.SendMailAsync(oServer, oMail)

            Await _asyncCancel
            TextStatus.Text = "Completed"

        Catch ep As Exception
            TextStatus.Text = "Error:  " + ep.Message.TrimEnd(vbCr & vbLf.ToArray())
        End Try

        _asyncCancel = Nothing

        ProgressBarSending.Visibility = Visibility.Collapsed
        ButtonSend.IsEnabled = True
        ButtonCancel.IsEnabled = False
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As RoutedEventArgs)
        ButtonCancel.IsEnabled = False
        If _asyncCancel IsNot Nothing Then
            _asyncCancel.Cancel()
        End If
    End Sub

    Dim _autoChangeUser = True
    Dim _autoChangeServer = True

    Private Sub TextServer_TextChanged(sender As Object, e As TextChangedEventArgs)
        ChangeSettingforWellKnownServer(TextServer.Text)
    End Sub

    Private Sub ChangeSettingforWellKnownServer(server As String)
        If String.Compare(server, "smtp.gmail.com", True) = 0 OrElse
            String.Compare(server, "smtp.live.com", True) = 0 OrElse
            String.Compare(server, "smtp.mail.yahoo.com", True) = 0 OrElse
            String.Compare(server, "smtp.office365.com", True) = 0 OrElse
            String.Compare(server, "smtp.aol.com", True) = 0 Then
            ListPorts.SelectedIndex = 1
            '465 port, you can also use 25, 587
            CheckSsl.IsOn = True
            CheckAuthentication.IsOn = True
        End If
    End Sub

    Private Sub ListProtocols_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Select Case ListProtocols.SelectedIndex
            Case 1
                CheckAuthentication.IsOn = True
                CheckSsl.IsOn = True
                ListPorts.IsEnabled = False
                Exit Select
            Case 2
                CheckAuthentication.IsOn = True
                ListPorts.IsEnabled = False
                Exit Select
            Case Else
                ListPorts.IsEnabled = True
                Exit Select
        End Select

    End Sub

    ' Change server address by well known email domain
    Private Sub TextFrom_TextChanged(sender As Object, e As TextChangedEventArgs)
        Dim address = New MailAddress(TextFrom.Text)

        If _autoChangeUser Then
            TextUser.Text = address.Address
        End If

        If _autoChangeServer Then
            Dim domain As String = address.GetAddressDomain()
            If String.Compare(domain, "hotmail.com", True) = 0 Then
                TextServer.Text = "smtp.live.com"
            ElseIf String.Compare(domain, "gmail.com", True) = 0 Then
                TextServer.Text = "smtp.gmail.com"
            ElseIf String.Compare(domain, "yahoo.com", True) = 0 Then
                TextServer.Text = "smtp.mail.yahoo.com"
            ElseIf String.Compare(domain, "aol.com", True) = 0 Then
                TextServer.Text = "smtp.aol.com"
            End If
        End If
    End Sub

    Private Sub TextUser_KeyUp(sender As Object, e As KeyRoutedEventArgs)
        _autoChangeUser = String.IsNullOrWhiteSpace(TextUser.Text)
    End Sub

    Private Sub TextServer_KeyUp(sender As Object, e As KeyRoutedEventArgs)
        _autoChangeServer = String.IsNullOrWhiteSpace(TextServer.Text)
    End Sub
End Class

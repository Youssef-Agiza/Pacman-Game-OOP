Imports Windows.UI.Xaml.Navigation
Imports Windows.UI.Popups
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports EASendMail

Partial Public NotInheritable Class MainPage
    Inherits Page
    Public Sub New()
        Me.InitializeComponent()
    End Sub

    Public Class RecipientData
        Implements System.ComponentModel.INotifyPropertyChanged
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements _
            INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
        End Sub

        Public Sub New(address As String, status As String, index As Integer)
            _address = address
            _index = index
            _status = status
        End Sub
        Private _address As String = ""
        Private _status As String = ""
        Private _index As Integer = 0

        Public ReadOnly Property Address() As String
            Get
                Return _address
            End Get
        End Property

        Public ReadOnly Property Index() As String
            Get
                If _index = 0 Then
                    Return "Seq"
                Else
                    Return _index.ToString()
                End If
            End Get
        End Property

        Public Property Status() As String
            Get
                Return _status
            End Get
            Set
                If Me._status <> Value Then
                    Me._status = Value.Trim(vbCr & vbLf.ToArray())
                    Me.OnPropertyChanged("Status")
                End If
            End Set
        End Property

        Public ReadOnly Property Width() As Double
            Get
                Return Window.Current.Bounds.Width
            End Get
        End Property

        Public ReadOnly Property Color() As SolidColorBrush
            Get
                If _index = 0 Then
                    Return New SolidColorBrush(Windows.UI.Colors.LightGray)
                End If

                Return New SolidColorBrush(Windows.UI.Colors.White)
            End Get
        End Property
    End Class

    Private _listStyleIndex As Integer = 0
    Private _isHtmlInited As Boolean = False
    Private _attachments As New List(Of String)()

    Private m_cts As CancellationTokenSource = Nothing

    Private _total As Integer = 0
    Private _success As Integer = 0
    Private _failed As Integer = 0

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
        TextEditor.Text = "Hi," & vbCr & vbLf & vbCr & vbLf & "This is a simple test email sent from C# + Universal Windows Platform (UWP) project." & vbCr & vbLf & "Please do not reply."
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
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = oSmtp.Tag
        UpdateRecipientItem(index, "Securing ... ")
    End Sub

    Private Sub OnAuthorized(sender As Object, e As SmtpStatusEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = oSmtp.Tag
        UpdateRecipientItem(index, "Authorized")
    End Sub


    Public Sub OnConnected(sender As Object, e As SmtpStatusEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = oSmtp.Tag
        UpdateRecipientItem(index, "Connected")
    End Sub

    Public Sub OnSendingDataStream(sender As Object, e As SmtpDataStreamEventArgs)
        Dim oSmtp As SmtpClient = TryCast(sender, SmtpClient)
        Dim index As Integer = oSmtp.Tag
        UpdateRecipientItem(index, String.Format("{0}/{1} sent", e.Sent, e.Total))
    End Sub

#End Region

    Private Async Sub ButtonSend_Click(sender As Object, e As RoutedEventArgs)
        _total = 0
        _success = 0
        _failed = 0
        If TextFrom.Text.Trim().Length = 0 Then
            Dim dlg As New MessageDialog("Please input from address!")
            Await dlg.ShowAsync()
            TextFrom.Text = ""
            TextFrom.Focus(FocusState.Programmatic)
            Return
        End If

        If TextTo.Text.Trim(vbCr & vbLf & " " & vbTab.ToCharArray()).Length = 0 Then
            Dim dlg As New MessageDialog("Please input a recipient at least!")
            Await dlg.ShowAsync()
            TextTo.Text = ""
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

        Dim isUserAuthentication As Boolean = CheckAuthentication.IsOn
        If isUserAuthentication Then
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
        ButtonSend.Visibility = Visibility.Collapsed
        ButtonCancel.Visibility = Visibility.Visible
        ButtonCancel.IsEnabled = True
        ButtonClose.Visibility = Visibility.Collapsed
        ButtonClose.IsEnabled = False
        ButtonAttach.Visibility = Visibility.Collapsed

        ListRecipients.Items.Clear()
        PageViewer.Visibility = Visibility.Collapsed
        StatusViewer.Visibility = Visibility.Visible

        m_cts = New CancellationTokenSource()

        Dim tasks As New List(Of Task)()

        Dim toLines As String() = TextTo.Text.Trim(vbCr & vbLf & " " & vbTab.ToCharArray()).Split(vbLf.ToCharArray())
        Dim recipientList As New List(Of String)()

        For i As Integer = 0 To toLines.Length - 1
            Dim address As String = toLines(i).Trim(vbCr & vbLf & " " & vbTab.ToCharArray())
            If address.Length > 0 Then
                recipientList.Add(address)
            End If
        Next

        Dim n As Integer = recipientList.Count
        toLines = recipientList.ToArray()

        _total = n
        TextStatus.Text = String.Format("Total {0}, success: {1}, failed {2}", _total, _success, _failed)
        ButtonCancel.IsEnabled = True

        n = 0
        ListRecipients.Items.Add(New RecipientData("Email", "Status", 0))
        n += 1
        For i As Integer = 0 To toLines.Length - 1
            Dim maxThreads As Integer = CInt(WorkerThreads.Value)
            While tasks.Count >= maxThreads
                Dim taskFinished As Task = Await Task.WhenAny(tasks.ToArray())
                tasks.Remove(taskFinished)
                TextStatus.Text = String.Format("Total {0}, success: {1}, failed {2}", _total, _success, _failed)
            End While

            Dim address As String = toLines(i)
            Dim index As Integer = n
            ListRecipients.Items.Add(New RecipientData(address, "Queued", n))

            If m_cts.Token.IsCancellationRequested Then
                n += 1
                UpdateRecipientItem(index, "Operation was cancelled!")
                Continue For
            End If

            Dim oServer As New SmtpServer(TextServer.Text)
            Dim ports As Integer() = New Integer() {25, 587, 465}
            oServer.Port = ports(ListPorts.SelectedIndex)
            Dim isSslConnection As Boolean = CheckSsl.IsOn
            If isSslConnection Then
                ' use SSL/TLS based on server port
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto
            Else
                ' Most mordern SMTP servers require SSL/TLS connection now
                ' ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
                oServer.ConnectType = SmtpConnectType.ConnectTryTLS
            End If

            oServer.Protocol = DirectCast(ListProtocols.SelectedIndex, ServerProtocol)
            If isUserAuthentication Then
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
            oMail.[To] = New AddressCollection(address)
            oMail.Subject = TextSubject.Text

            Dim bodyText As String = ""
            Dim htmlBody As Boolean = False
            If CheckHtml.IsOn Then
                bodyText = Await HtmlEditor.InvokeScriptAsync("getHtml", Nothing)
                htmlBody = True
            Else
                bodyText = TextEditor.Text
            End If

            Dim count As Integer = _attachments.Count
            Dim attachments As String() = New String(count - 1) {}
            For x As Integer = 0 To count - 1
                attachments(x) = _attachments(x)
            Next

            Dim subtask As Task = Task.Factory.StartNew(
                Sub()
                    SubmitMail(oServer, oMail, attachments, bodyText, htmlBody, index).Wait()
                End Sub)

            tasks.Add(subtask)
            n += 1
        Next


        If tasks.Count > 0 Then
            Await Task.WhenAll(tasks.ToArray())
        End If

        TextStatus.Text = String.Format("Total {0}, success: {1}, failed {2}", _total, _success, _failed)

        ButtonSend.IsEnabled = False
        ButtonSend.Visibility = Visibility.Collapsed
        ButtonCancel.Visibility = Visibility.Collapsed
        ButtonCancel.IsEnabled = False
        ButtonClose.Visibility = Visibility.Visible
        ButtonClose.IsEnabled = True
        ButtonAttach.Visibility = Visibility.Collapsed
    End Sub

    Private Sub UpdateRecipientItem(index As Integer, status As String)
        If Me.Dispatcher.HasThreadAccess Then
            Dim data = TryCast(ListRecipients.Items(index), RecipientData)
            data.Status = status
            Return
        End If

        Me.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, Sub()
                                                                                  Dim data = TryCast(ListRecipients.Items(index), RecipientData)
                                                                                  data.Status = status

                                                                              End Sub).AsTask().Wait()

    End Sub

    Private Async Function SubmitMail(oServer As SmtpServer, oMail As SmtpMail, atts As String(), bodyText As String, htmlBody As Boolean, index As Integer) As Task

        Dim oSmtp As SmtpClient = Nothing
        Try
            oSmtp = New SmtpClient()

            ' add event handler
            AddHandler oSmtp.Authorized, AddressOf OnAuthorized
            AddHandler oSmtp.Connected, AddressOf OnConnected
            AddHandler oSmtp.Securing, AddressOf OnSecuring
            AddHandler oSmtp.SendingDataStream, AddressOf OnSendingDataStream

            UpdateRecipientItem(index, "Preparing ...")

            If Not htmlBody Then
                oMail.TextBody = bodyText
            Else
                Dim html As String = bodyText
                html = (Convert.ToString("<html><head><meta charset=""utf-8"" /></head><body style=""font-family:Calibri;font-size: 15px;"">") & html) + "<body></html>"
                Await oMail.ImportHtmlAsync(html, Windows.ApplicationModel.Package.Current.InstalledLocation.Path, ImportHtmlBodyOptions.ErrorThrowException Or ImportHtmlBodyOptions.ImportLocalPictures Or ImportHtmlBodyOptions.ImportHttpPictures Or ImportHtmlBodyOptions.ImportCss)
            End If

            Dim count As Integer = atts.Length
            For i As Integer = 0 To count - 1
                Await oMail.AddAttachmentAsync(atts(i))
            Next

            UpdateRecipientItem(index, String.Format("Connecting {0} ...", oServer.Server))
            oSmtp.Tag = index

            ' You can genereate a log file by the following code.
            ' oSmtp.LogFileName = "ms-appdata:///local/smtp.txt";
            Dim asyncObject As IAsyncAction = oSmtp.SendMailAsync(oServer, oMail)
            m_cts.Token.Register(Sub()
                                     asyncObject.Cancel()
                                 End Sub)
            Await asyncObject

            Interlocked.Increment(_success)

            UpdateRecipientItem(index, "Completed")
        Catch ep As Exception
            oSmtp.Close()
            Dim errDescription As String = ep.Message
            UpdateRecipientItem(index, errDescription)
            Interlocked.Increment(_failed)
        End Try
    End Function


    Private Sub ButtonCancel_Click(sender As Object, e As RoutedEventArgs)
        ButtonCancel.IsEnabled = False
        If m_cts IsNot Nothing Then
            m_cts.Cancel(True)
        End If

    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As RoutedEventArgs)
        ButtonSend.IsEnabled = True
        ButtonSend.Visibility = Visibility.Visible
        ButtonCancel.Visibility = Visibility.Collapsed
        ButtonCancel.IsEnabled = False
        ButtonClose.Visibility = Visibility.Collapsed
        ButtonClose.IsEnabled = False
        ButtonAttach.Visibility = Visibility.Visible

        PageViewer.Visibility = Visibility.Visible
        statusViewer.Visibility = Visibility.Collapsed
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

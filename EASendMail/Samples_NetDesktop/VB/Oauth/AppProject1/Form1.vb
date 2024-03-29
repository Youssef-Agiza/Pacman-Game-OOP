﻿Imports System.Text
Imports EASendMail

Public Class Form1
    Public Sub New()
        MyBase.New()

        InitializeComponent()
        _initialize()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _initHtmlEditor()
        _initControlOffset()
    End Sub

    Private _attachments As List(Of String) = New List(Of String)()
    Private _isCancelSending As Boolean = False

#Region "EASendMail EventHandler"

    Private Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = _isCancelSending

        If Not cancel Then
            Application.DoEvents()
        End If
    End Sub

    Private Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        _setStatus("Connected")
        cancel = _isCancelSending
    End Sub

    Private Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        If ProgressBarSend.Value = 0 Then
            _setStatus("Sending ...")
        End If

        _setProgressBar(sent, total)
        cancel = _isCancelSending
        If sent = total Then _setStatus("Disconnecting ...")
    End Sub

    Private Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        _setStatus("Authorized")
        cancel = _isCancelSending
    End Sub

    Private Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        _setStatus("Securing ...")
        cancel = _isCancelSending
    End Sub

#End Region

#Region "Initialize Encoding/Protocols/Ports"

    Private _charsets As String(,) = New String(,) {
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
    {"Western European(Windows)", "Windows-1252"}}

    Private Sub _initCharsets()
        Dim selectIndex As Integer = 24

        For i As Integer = 0 To _charsets.GetLength(0) - 1
            ComboBoxEncoding.Items.Add(_charsets(i, 0))
        Next

        ComboBoxEncoding.SelectedIndex = selectIndex
    End Sub

    Private Sub _initialize()
        _initCharsets()
        _initProtocols()
    End Sub

    Private Sub _initProtocols()
        ComboBoxPorts.Items.AddRange(New String() {"25", "587", "465"})
        ComboBoxPorts.SelectedIndex = 1

        ComboBoxProviders.Items.AddRange(New String() {"Google OAUTH + SMTP", "Live OAUTH + SMTP (Hotmail)", "MS OAUTH + EWS (Office365)", "Gmail Reset Api + OAUTH"})
        ComboBoxProviders.SelectedIndex = 0
    End Sub

    Private Function _getServerAddressByProvider(ByVal index As Integer) As String
        Select Case index
            Case OauthProvider.GoogleSmtpProvider
                Return "smtp.gmail.com"
            Case OauthProvider.MsLiveProvider
                Return "smtp.live.com"
            Case OauthProvider.MsOffice365Provider
                Return "outlook.office365.com"
            Case OauthProvider.GoogleGmailApiProvider
                Return "https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=media"
            Case Else
                Throw New Exception("Invalid OAUTH provider!")
        End Select
    End Function

    Private Sub CheckBoxListener_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxListener.CheckedChanged
        If _oauthWrapper IsNot Nothing Then
            _oauthWrapper.Provider.ClearToken()
            _oauthWrapper.Provider.UseHttpListener = CheckBoxListener.Checked
        End If
    End Sub

    Private _oauthWrapper As OauthDesktopWrapper = Nothing

    Private Sub ComboBoxProviders_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxProviders.SelectedIndexChanged
        TextBoxServer.Text = _getServerAddressByProvider(ComboBoxProviders.SelectedIndex)
        If (ComboBoxProviders.SelectedIndex = OauthProvider.MsOffice365Provider Or ComboBoxProviders.SelectedIndex = OauthProvider.GoogleGmailApiProvider) Then
            ComboBoxPorts.Enabled = False
        Else
            ComboBoxPorts.Enabled = True
        End If

        Select Case ComboBoxProviders.SelectedIndex
            Case OauthProvider.GoogleSmtpProvider
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateGoogleSmtpProvider())
            Case OauthProvider.MsLiveProvider
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateMsLiveProvider())
            Case OauthProvider.MsOffice365Provider
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateMsOffice365Provider())
            Case OauthProvider.GoogleGmailApiProvider
                _oauthWrapper = New OauthDesktopWrapper(OauthProvider.CreateGoogleGmailApiProvider())
            Case Else
                Throw New Exception("Invalid OAUTH provider!")
        End Select
    End Sub

#End Region

#Region "Sign and Encrypt E-mail by Digital Certificate"

    Private Sub _signEmail(ByVal mail As SmtpMail)
        If Not CheckBoxSignature.Checked Then
            Return
        End If

        Try
            mail.From.Certificate.FindSubject(mail.From.Address, Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, "My")
        Catch exp As Exception
            Throw New Exception("No signer certificate found for <" & mail.From.Address & ">: " + exp.Message)
        End Try
    End Sub

    Private Sub _encryptEmail(ByVal mail As SmtpMail)
        If Not CheckBoxEncrypt.Checked Then
            Return
        End If

        For i As Integer = 0 To mail.[To].Count - 1
            Dim oAddress As MailAddress = TryCast(mail.[To](i), MailAddress)

            Try
                oAddress.Certificate.FindSubject(oAddress.Address, Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, "AddressBook")
            Catch

                Try
                    oAddress.Certificate.FindSubject(oAddress.Address, Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, "My")
                Catch exp As Exception
                    Throw New Exception("No encryption certificate found for <" & oAddress.Address & ">: " + exp.Message)
                End Try
            End Try
        Next

        For i As Integer = 0 To mail.Cc.Count - 1
            Dim oAddress As MailAddress = TryCast(mail.Cc(i), MailAddress)

            Try
                oAddress.Certificate.FindSubject(oAddress.Address, Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, "AddressBook")
            Catch

                Try
                    oAddress.Certificate.FindSubject(oAddress.Address, Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER, "My")
                Catch exp As Exception
                    Throw New Exception("No encryption certificate found for <" & oAddress.Address & ">:" + exp.Message)
                End Try
            End Try
        Next
    End Sub

    Private Sub _signAndEncryptEmail(ByVal mail As SmtpMail)
        _signEmail(mail)
        _encryptEmail(mail)
    End Sub

#End Region

#Region "Create SmtpMail And SmtpServer instance based On Settings Of Form Controls"

    Private Function _createMail() As SmtpMail
        ' For evaluation usage, please use "TryIt" as the license code, otherwise the 
        ' "invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        ' "trial version expired" exception will be thrown.

        ' For licensed uasage, please use your license code instead of "TryIt", then the object
        ' will never expire
        Dim mail As SmtpMail = New SmtpMail("TryIt")

        ' From is a MailAddress object, it supports implicit converting from string.
        ' The syntax is like this: "test@adminsystem.com" or "Tester <test@adminsystem.com>"
        mail.From = TextBoxFrom.Text

        If String.Compare(mail.From.Address, _oauthWrapper.Provider.UserEmail, True) <> 0 Then
            mail.ReplyTo = mail.From
            mail.From = _oauthWrapper.Provider.UserEmail
        End If

        ' To, Cc and Bcc is a AddressCollection object, it supports implicit converting from string.
        ' multiple address are separated with (,;), The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"
        mail.[To] = TextBoxTo.Text
        mail.Cc = TextBoxCc.Text
        mail.Subject = TextBoxSubject.Text
        mail.Charset = _charsets(ComboBoxEncoding.SelectedIndex, 1)

        Dim htmlBody As String = _buildHtmlBody()
        htmlBody = htmlBody.Replace("[$from]", _encodeAddressToHtml(mail.From.ToString()))
        htmlBody = htmlBody.Replace("[$to]", _encodeAddressToHtml(mail.[To].ToString()))
        htmlBody = htmlBody.Replace("[$subject]", _encodeAddressToHtml(mail.Subject))
        mail.ImportHtml(htmlBody, Application.ExecutablePath, ImportHtmlBodyOptions.ImportLocalPictures)

        Dim count As Integer = _attachments.Count

        For i As Integer = 0 To count - 1
            mail.AddAttachment(TryCast(_attachments(i), String))
        Next

        _signAndEncryptEmail(mail)
        Return mail
    End Function

    Private Function _createSmtpServer() As SmtpServer
        Dim server As SmtpServer = New SmtpServer(TextBoxServer.Text)

        If server.Server.Length = 0 Then
            server.Server = _getServerAddressByProvider(ComboBoxProviders.SelectedIndex)
        End If

        Dim ports As Integer() = {25, 587, 465}
        server.Port = ports(ComboBoxPorts.SelectedIndex)
        server.ConnectType = SmtpConnectType.ConnectSSLAuto
        server.AuthType = SmtpAuthType.XOAUTH2

        ' Office365 OAUTH only supports EWS protocol. Google and Live OAUTH support SMTP protocol.
        Select Case ComboBoxProviders.SelectedIndex
            Case OauthProvider.MsOffice365Provider
                server.Protocol = ServerProtocol.ExchangeEWS
            Case OauthProvider.GoogleGmailApiProvider
                server.Protocol = ServerProtocol.GmailApi
            Case Else
                server.Protocol = ServerProtocol.SMTP
        End Select

        server.User = _oauthWrapper.Provider.UserEmail
        server.Password = _oauthWrapper.Provider.AccessToken
        Return server
    End Function

#End Region

    Private Sub _doOauth()
        If Not String.IsNullOrEmpty(_oauthWrapper.Provider.AccessToken) Then

            If Not _oauthWrapper.IsAccessTokenExpired Then
                Return
            End If

            StatusBarSend.Text = "Refreshing access token ..."

            Try
                _oauthWrapper.RefreshAccessToken()
            Catch
                StatusBarSend.Text = "Failed to refresh access token, try to get a new access token ..."
            End Try
        End If

        Using DialogOauth As FormOauth = New FormOauth()
            _oauthWrapper.Provider.UseHttpListener = CheckBoxListener.Checked
            DialogOauth.OauthWrapper = _oauthWrapper
            DialogOauth.ShowDialog()
        End Using

        StatusBarSend.Text = "Requesting access token ..."
        _oauthWrapper.RequestAccessTokenAndUserEmail()
        StatusBarSend.Text = "Oauth is completed, ready to send email."
    End Sub

    Private Sub ButtonSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSend.Click

        ' If TextBoxFrom.Text.Length = 0 Then
        ' MessageBox.Show("You didn't input from address, authenticated user's email address is used automatically.")
        ' End If

        ButtonSend.Enabled = False
        ButtonCancel.Enabled = True
        ButtonClearToken.Enabled = False
        _isCancelSending = False

        Try
            _doOauth()
            Dim smtp As SmtpClient = New SmtpClient()

            If TextBoxTo.Text.Length = 0 AndAlso TextBoxCc.Text.Length = 0 Then
                Throw New ArgumentNullException("To, Cc", "Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients")
            End If
            ' Catching the following events is not necessary, 
            ' just make the application more user friendly.
            ' If you use the object in asp.net/windows service or non-gui application, 
            ' You need not to catch the following events.
            ' To learn more detail, please refer to the code in EASendMail EventHandler region
            AddHandler smtp.OnIdle, AddressOf OnIdle
            AddHandler smtp.OnAuthorized, AddressOf OnAuthorized
            AddHandler smtp.OnConnected, AddressOf OnConnected
            AddHandler smtp.OnSecuring, AddressOf OnSecuring
            AddHandler smtp.OnSendingDataStream, AddressOf OnSendingDataStream

            Dim server As SmtpServer = _createSmtpServer()
            Dim mail As SmtpMail = _createMail()

            StatusBarSend.Text = "Connecting ... "
            ProgressBarSend.Value = 0

            smtp.SendMail(server, mail)

            MessageBox.Show(String.Format("The message was sent to {0} successfully!", smtp.CurrentSmtpServer.Server))
            StatusBarSend.Text = "Completed"
        Catch exp As Exception
            MessageBox.Show(exp.Message)
            StatusBarSend.Text = String.Format("Exception: {0}", exp.Message)

            If TypeOf exp Is ArgumentNullException Then
                TextBoxTo.Select()
            Else
                _oauthWrapper.Provider.ClearToken()
            End If

        End Try

        ButtonSend.Enabled = True
        ButtonCancel.Enabled = False
        ButtonClearToken.Enabled = (_oauthWrapper.Provider.AccessToken.Length <> 0)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        ButtonCancel.Enabled = False
        _isCancelSending = True
    End Sub

    Private Sub ButtonClearToken_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonClearToken.Click
        _oauthWrapper.Provider.ClearToken()
        _oauthWrapper.AuthorizationCode = ""
        ButtonClearToken.Enabled = False
    End Sub

    Private Sub ButtonAddAttachment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddAttachment.Click
        HtmlEditor.Focus()
        OpenFileDialogAttachment.Reset()
        OpenFileDialogAttachment.Multiselect = True
        OpenFileDialogAttachment.CheckFileExists = True
        OpenFileDialogAttachment.CheckPathExists = True

        If OpenFileDialogAttachment.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        Dim attachments As String() = OpenFileDialogAttachment.FileNames

        For i As Integer = 0 To attachments.Length - 1
            _attachments.Add(attachments(i))
            Dim fileName As String = attachments(i)
            Dim pos As Integer = fileName.LastIndexOf("\")
            If pos <> -1 Then fileName = fileName.Substring(pos + 1)
            TextBoxAttachments.Text += fileName
            TextBoxAttachments.Text += ";"
        Next
    End Sub

    Private Sub ButtonClearAttachments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClearAttachments.Click
        _attachments.Clear()
        TextBoxAttachments.Text = ""
    End Sub

#Region "Html Editor"

    Private _htmlDoc As mshtml.IHTMLDocument2 = Nothing

    Private Sub ButtonBold_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBold.Click
        _htmlDoc.execCommand("Bold", False, Nothing)
        HtmlEditor.Focus()
    End Sub

    Private Sub ButtonItalic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonItalic.Click
        _htmlDoc.execCommand("Italic", False, Nothing)
        HtmlEditor.Focus()
    End Sub

    Private Sub ButtonUnderline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUnderline.Click
        _htmlDoc.execCommand("Underline", False, Nothing)
        HtmlEditor.Focus()
    End Sub

    Private Sub ButtonColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonColor.Click
        If ColorDialogForeColor.ShowDialog() = DialogResult.OK Then
            Dim v As String = String.Format("#{0:x2}{1:x2}{2:x2}", ColorDialogForeColor.Color.R, ColorDialogForeColor.Color.G, ColorDialogForeColor.Color.B)
            _htmlDoc.execCommand("ForeColor", False, v)
        End If

        HtmlEditor.Focus()
    End Sub

    Private Sub _initFonts()
        Dim fonts As String() = New String() {"Choose Font Style ...", "Arial", "Calibri", "Comic Sans MS", "Consolas", "Courier New", "Helvetica", "Times New Roman", "Tahoma", "Verdana", "Segoe UI"}
        Dim nCount As Integer = fonts.Length

        For i As Integer = 0 To nCount - 1
            ComboBoxFont.Items.Add(fonts(i))
        Next

        ComboBoxFont.SelectedIndex = 0
        ComboBoxSize.Items.Add("Font Size ... ")

        For i As Integer = 1 To 7 - 1
            ComboBoxSize.Items.Add(i)
        Next

        ComboBoxSize.SelectedIndex = 0
    End Sub

    Private Sub ComboBoxFont_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxFont.SelectedIndexChanged
        Dim font As String = ComboBoxFont.Text
        ComboBoxFont.SelectedIndex = 0
        _htmlDoc.execCommand("fontname", False, font)
        HtmlEditor.Focus()
    End Sub

    Private Sub ComboBoxSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxSize.SelectedIndexChanged
        Dim size As String = ComboBoxSize.Text
        ComboBoxSize.SelectedIndex = 0
        _htmlDoc.execCommand("fontsize", False, size)
        HtmlEditor.Focus()
    End Sub

    Private Sub ButtonInsertImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonInsertImage.Click
        _htmlDoc.execCommand("InsertImage", True, Nothing)
    End Sub

    Private Function _encodeAddressToHtml(ByVal v As String) As String
        v = v.Replace(">", "&gt;")
        v = v.Replace("<", "&lt;")
        Return v
    End Function

    Private Sub _initHtmlEditor()
        HtmlEditor.Navigate("about:blank")
        _htmlDoc = TryCast(HtmlEditor.Document.DomDocument, mshtml.IHTMLDocument2)
        _htmlDoc.designMode = "on"
        _initFonts()
    End Sub

    Private _defaultBodyFont As String = "Calibri"

    Private Sub HtmlEditor_Navigated(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs) Handles HtmlEditor.Navigated
        Dim s As StringBuilder = New StringBuilder()
        s.Append("<div>This sample demonstrates how to send html email using OAUTH 2.0.</div><div>&nbsp;</div>")
        s.Append("<div>From: [$from]</div>")
        s.Append("<div>To: [$to]</div>")
        s.Append("<div>Subject: [$subject]</div><div>&nbsp;</div>")
        s.Append("<div>This sample project demonstrates how to send email using Gmail/MS Live/MS Office365 XOAUTH2, ")
        s.Append("Please create your client_id and client_secret introduced in Form OauthProvider.cs.</div>")
        s.Append("<div>If you got ""This app isn't verified"" information, please click ""advanced"" -> Go to ... for test.</div>")
        s.Append("<div>&nbsp;</div>")

        _htmlDoc.body.style.fontFamily = _defaultBodyFont
        _htmlDoc.body.innerHTML = s.ToString()
    End Sub

    Private Function _buildHtmlBody() As String
        Dim html As String = String.Format("<html><title>{0}</title><meta http-equiv=""Content-Type"" content=""text/html; charset={1}""><META content=""MSHTML 6.00.2900.2769"" name=GENERATOR><body style=""font-family: '{2}';"">", TextBoxSubject.Text, _charsets(ComboBoxEncoding.SelectedIndex, 1), _defaultBodyFont)
        html += _htmlDoc.body.innerHTML
        html += "</body></html>"
        Return html
    End Function

#End Region

#Region "AutoSize Controls"

    Private _isFormLoaded As Boolean = False
    Private _textBoxFromWidthOffset As Integer = 0
    Private _textBoxAttachmentsWidthOffset As Integer = 0
    Private _buttonAddAttachmentLeftOffset As Integer = 0
    Private _buttonClearAttachmentsLeftOffset As Integer = 0
    Private _serverGroupBoxOffset As Integer = 0
    Private _textBoxBodyHeightOffset As Integer = 0
    Private _progressBarSendTopOffset As Integer = 0
    Private _progressBarSendWidthOffset As Integer = 0
    Private _buttonSendTopOffset As Integer = 0
    Private _buttonSendLeftOffset As Integer = 0
    Private _buttonCancelLeftOffset As Integer = 0

    Private Sub _initControlOffset()
        Me.MinimumSize = New System.Drawing.Size(Me.Width, Me.Height)
        _isFormLoaded = True
        _textBoxAttachmentsWidthOffset = Me.Width - TextBoxAttachments.Width
        _textBoxBodyHeightOffset = Me.Height - HtmlEditor.Height
        _progressBarSendTopOffset = Me.Height - ProgressBarSend.Top
        _buttonSendTopOffset = Me.Height - ButtonSend.Top
        _textBoxFromWidthOffset = Me.Width - TextBoxFrom.Width
        _progressBarSendWidthOffset = Me.Width - ProgressBarSend.Width
        _buttonAddAttachmentLeftOffset = Me.Width - ButtonAddAttachment.Left
        _buttonClearAttachmentsLeftOffset = Me.Width - ButtonClearAttachments.Left
        _serverGroupBoxOffset = Me.Width - GroupBoxServer.Left
        _buttonSendLeftOffset = Me.Width - ButtonSend.Left
        _buttonCancelLeftOffset = Me.Width - ButtonCancel.Left
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        If Not _isFormLoaded Then
            Return
        End If

        TextBoxAttachments.Width = Me.Width - _textBoxAttachmentsWidthOffset
        ButtonAddAttachment.Left = Me.Width - _buttonAddAttachmentLeftOffset
        ButtonClearAttachments.Left = Me.Width - _buttonClearAttachmentsLeftOffset
        HtmlEditor.Width = Me.Width - 40
        HtmlEditor.Height = Me.Height - _textBoxBodyHeightOffset
        ProgressBarSend.Top = Me.Height - _progressBarSendTopOffset
        ButtonSend.Top = Me.Height - _buttonSendTopOffset
        ButtonCancel.Top = ButtonSend.Top
        ProgressBarSend.Width = Me.Width - _progressBarSendWidthOffset
        GroupBoxServer.Left = Me.Width - _serverGroupBoxOffset
        ButtonSend.Left = Me.Width - _buttonSendLeftOffset
        ButtonCancel.Left = Me.Width - _buttonCancelLeftOffset
        TextBoxFrom.Width = Me.Width - _textBoxFromWidthOffset
        TextBoxTo.Width = TextBoxFrom.Width
        TextBoxCc.Width = TextBoxFrom.Width
        TextBoxSubject.Width = TextBoxFrom.Width
    End Sub

    Protected Delegate Sub SetStatusDelegate(ByVal v As String)
    Protected Delegate Sub SetProgressDelegate(ByVal sent As Integer, ByVal total As Integer)
    Private _eventTicks As Long = 0

#End Region

#Region "Cross Thread Access Control"
    ' Because some the events are fired on another
    ' thread, to change the control value safety, we used the following functions to 
    ' update control value. more detail, please refer to Control.BeginInvoke method in MSDN
    Protected Sub _setProgressBarCallback(ByVal sent As Integer, ByVal total As Integer)
        Dim scale As Long = (sent * 100) / total
        ProgressBarSend.Value = CInt(scale)
        Dim tick As Long = DateTime.Now.Ticks

        If tick - _eventTicks > 2000000 Then
            _eventTicks = tick
            Application.DoEvents()
        End If
    End Sub

    Protected Sub _setStatusCallback(ByVal v As String)
        StatusBarSend.Text = v
    End Sub

    Protected Sub _setStatus(ByVal v As String)
        If InvokeRequired Then
            Dim args As Object() = New Object(0) {}
            args(0) = v
            Dim d As SetStatusDelegate = New SetStatusDelegate(AddressOf _setStatusCallback)
            BeginInvoke(d, args)
        Else
            _setStatusCallback(v)
        End If
    End Sub

    Protected Sub _setProgressBar(ByVal sent As Integer, ByVal total As Integer)
        If InvokeRequired Then
            Dim args As Object() = New Object(1) {}
            args(0) = sent
            args(1) = total
            Dim d As SetProgressDelegate = New SetProgressDelegate(AddressOf _setProgressBarCallback)
            BeginInvoke(d, args)
        Else
            _setProgressBarCallback(sent, total)
        End If
    End Sub

#End Region



End Class

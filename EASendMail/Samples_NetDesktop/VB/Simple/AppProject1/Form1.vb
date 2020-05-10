Imports System.Text
Imports EASendMail

Public Class Form1
    Inherits Form

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        _initialize()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
            {"Western European(Windows)", "Windows-1252"}
        }

    Private Sub _initCharsets()
        Dim selectIndex As Integer = 24

        For i As Integer = 0 To _charsets.GetLength(0) - 1
            ComboBoxEncoding.Items.Add(_charsets(i, 0))
        Next

        ComboBoxEncoding.SelectedIndex = selectIndex
    End Sub

    Private Sub _initProtocols()
        ComboBoxProtocols.Items.AddRange(New String() {"SMTP Protocol - Recommended", "Exchange Web Service - 2007-2019/Office365", "Exchange WebDav - 2000/2003"})
        ComboBoxProtocols.SelectedIndex = 0
        ComboBoxPorts.Items.AddRange(New String() {"25", "587", "465"})
        ComboBoxPorts.SelectedIndex = 0
    End Sub

    Private Sub _initialize()
        Dim s As StringBuilder = New StringBuilder()
        s.Append("This sample demonstrates how to send simple email." & vbCrLf & vbCrLf)
        s.Append("From: [$from]" & vbCrLf)
        s.Append("To: [$to]" & vbCrLf)
        s.Append("Subject: [$subject]" & vbCrLf & vbCrLf)
        s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly. ")
        s.Append("However, if you don't have a static IP address, ")
        s.Append("many anti-spam filters will mark it as a junk-email." & vbCrLf & vbCrLf)
        s.Append("If ""Digitial Signature"" was checked, please make sure you have the certificate for the sender address installed on ")
        s.Append("Local User Certificate Store." & vbCrLf & vbCrLf)
        s.Append("If ""Encrypt"" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store." & vbCrLf)

        TextBoxBody.Text = s.ToString()

        _initCharsets()
        _initProtocols()
        _checkBoxAuthChanged()
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

#Region "Send E-mail without SMTP server to multiple recipients"
    ' We don't suggest that you send email directly without SMTP server.
    ' Most email providers will reject your message or detet it as junk email.
    Private Function _createMailForDirectSend(ByVal address As MailAddress) As SmtpMail

        ' For evaluation usage, please use "TryIt" as the license code, otherwise the 
        ' "invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        ' "trial version expired" exception will be thrown.

        ' For licensed uasage, please use your license code instead of "TryIt", then the object
        ' will never expire
        Dim mail As SmtpMail = New SmtpMail("TryIt")

        ' From is a MailAddress object, it supports implicit converting from string.
        ' The syntax is like this: "test@adminsystem.com" or "Tester <test@adminsystem.com>"
        mail.From = TextBoxFrom.Text
        
        ' If you want to specify a reply address rather than From address
        ' mail.ReplyTo = "reply@mydomain";
        
        ' To, Cc and Bcc is a AddressCollection object, it supports implicit converting from string.
        ' multiple address are separated with (,;), The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"
        mail.[To].Add(address)
        mail.Subject = TextBoxSubject.Text
        mail.Charset = _charsets(ComboBoxEncoding.SelectedIndex, 1)

        Dim server As SmtpServer = New SmtpServer("")
        Dim cur As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
        Dim gmtDateTime As String = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur)
        gmtDateTime.Remove(gmtDateTime.Length - 3, 1)
        Dim receivedHeader As String = String.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;" & vbCrLf & vbTab & " {1}", server.HeloDomain, gmtDateTime)
        mail.Headers.Insert(0, New HeaderItem("Received", receivedHeader))

        Dim body As String = TextBoxBody.Text
        body = body.Replace("[$from]", mail.From.ToString())
        body = body.Replace("[$to]", mail.[To].ToString())
        body = body.Replace("[$subject]", mail.Subject)

        If CheckBoxHtml.Checked Then
            mail.HtmlBody = body
        Else
            mail.TextBody = body
        End If

        Dim count As Integer = _attachments.Count

        For i As Integer = 0 To count - 1
            mail.AddAttachment(TryCast(_attachments(i), String))
        Next

        _signAndEncryptEmail(mail)
        Return mail
    End Function

    Private Sub _directSendEmail()
        ButtonSend.Enabled = False
        ButtonCancel.Enabled = True
        _isCancelSending = False

        Dim recipients As AddressCollection = New AddressCollection()
        recipients.AddRange(New AddressCollection(TextBoxTo.Text))
        recipients.AddRange(New AddressCollection(TextBoxCc.Text))

        If recipients.Count = 0 Then
            MessageBox.Show("No recipient found!")
            StatusBarSend.Text = "No recipient found!"
            ButtonSend.Enabled = True
            ButtonCancel.Enabled = False
            Return
        End If

        For i As Integer = 0 To recipients.Count - 1
            Dim address As MailAddress = TryCast(recipients(i), MailAddress)

            Try
                Dim server As SmtpServer = New SmtpServer("")
                server.ConnectType = SmtpConnectType.ConnectTryTLS

                Dim mail As SmtpMail = _createMailForDirectSend(address)
                StatusBarSend.Text = String.Format("Connecting server for {0} ... ", address.Address)
                ProgressBarSend.Value = 0
                
                Dim smtp As SmtpClient = New SmtpClient()

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

                smtp.SendMail(server, mail)

                MessageBox.Show(String.Format("The message to <{0}> was sent to {1} successfully!", address.Address, smtp.CurrentSmtpServer.Server))
                StatusBarSend.Text = "Completed"
            Catch exp As SmtpTerminatedException
                StatusBarSend.Text = exp.Message
                Exit For
            Catch exp As Exception
                MessageBox.Show(String.Format("The message was unable to delivery to <{0}> due to " & vbCrLf & "{1}", address.Address, exp.Message))
                StatusBarSend.Text = String.Format("Exception: {0}", exp.Message)
            End Try
        Next

        ButtonSend.Enabled = True
        ButtonCancel.Enabled = False
    End Sub

#End Region

#Region "Create SmtpMail and SmtpServer instance based on Settings of Form Controls"

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

        ' If you want to specify a reply address rather than From address
        ' mail.ReplyTo = "reply@mydomain";
        
        ' To, Cc and Bcc is a AddressCollection object, it supports implicit converting from string.
        ' multiple address are separated with (,;), The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"
        mail.[To] = TextBoxTo.Text
        mail.Cc = TextBoxCc.Text
        mail.Subject = TextBoxSubject.Text
        mail.Charset = _charsets(ComboBoxEncoding.SelectedIndex, 1)

        Dim body As String = TextBoxBody.Text
        body = body.Replace("[$from]", mail.From.ToString())
        body = body.Replace("[$to]", mail.[To].ToString())
        body = body.Replace("[$subject]", mail.Subject)

        If CheckBoxHtml.Checked Then
            mail.HtmlBody = body
        Else
            mail.TextBody = body
        End If

        Dim count As Integer = _attachments.Count

        For i As Integer = 0 To count - 1
            mail.AddAttachment(TryCast(_attachments(i), String))
        Next

        _signAndEncryptEmail(mail)
        Return mail
    End Function

    Private Function _createSmtpServer() As SmtpServer
        Dim server As SmtpServer = New SmtpServer(TextBoxServer.Text)
        server.Protocol = CType(ComboBoxProtocols.SelectedIndex, ServerProtocol)

        ' If remote SMTP server supports TLS, use TLS, then use plain TCP connection.
        server.ConnectType = SmtpConnectType.ConnectTryTLS

        If server.Server.Length <> 0 Then
            Dim ports As Integer() = {25, 587, 465}
            server.Port = ports(ComboBoxPorts.SelectedIndex)

            If CheckBoxAuth.Checked Then
                server.User = TextBoxUser.Text
                server.Password = TextBoxPassword.Text
            End If

            If CheckBoxSsl.Checked Then
                server.ConnectType = SmtpConnectType.ConnectSSLAuto
            End If
        End If

        Return server
    End Function

#End Region

    Private Sub ButtonSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSend.Click
        If TextBoxFrom.Text.Length = 0 Then
            MessageBox.Show("Please input From!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>")
            Return
        End If

        If TextBoxTo.Text.Length = 0 AndAlso TextBoxCc.Text.Length = 0 Then
            MessageBox.Show("Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients")
            Return
        End If

        If CheckBoxAuth.Checked AndAlso (TextBoxUser.Text.Length = 0 OrElse TextBoxPassword.Text.Length = 0) Then
            MessageBox.Show("Please input user/password for authentication!")
            Return
        End If

        If TextBoxServer.Text.Length = 0 Then
            _directSendEmail()
            Return
        End If

        ButtonSend.Enabled = False
        ButtonCancel.Enabled = True
        _isCancelSending = False

        Try
            Dim smtp As SmtpClient = New SmtpClient()

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
        End Try

        ButtonSend.Enabled = True
        ButtonCancel.Enabled = False
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        ButtonCancel.Enabled = False
        _isCancelSending = True
    End Sub

    Private Sub ButtonAddAttachment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddAttachment.Click
        openFileDialogAttachment.Reset()
        openFileDialogAttachment.Multiselect = True
        openFileDialogAttachment.CheckFileExists = True
        openFileDialogAttachment.CheckPathExists = True

        If openFileDialogAttachment.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        Dim attachments As String() = openFileDialogAttachment.FileNames

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

#Region "Cross Thread Access Control"
    ' Because some the events are fired on another
    ' thread, to change the control value safety, we used the following functions to 
    ' update control value. more detail, please refer to Control.BeginInvoke method in MSDN
    Protected Delegate Sub SetStatusDelegate(ByVal v As String)
    Protected Delegate Sub SetProgressDelegate(ByVal sent As Integer, ByVal total As Integer)
    Private _eventTicks As Long = 0

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

#Region "Guess SMTP server for popular email provider"

    Private _autoChangeServer As Boolean = True
    Private _autoChangeUser As Boolean = True

    Private Sub TextBoxUser_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TextBoxUser.KeyUp
        _autoChangeUser = (TextBoxUser.Text.Length = 0)
    End Sub

    Private Sub TextBoxServer_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TextBoxServer.KeyUp
        _autoChangeServer = (TextBoxServer.Text.Length = 0)
    End Sub

    Private Sub TextBoxFrom_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxFrom.TextChanged
        Dim address As MailAddress = New MailAddress(TextBoxFrom.Text)

        If _autoChangeUser Then
            TextBoxUser.Text = address.Address
        End If

        If _autoChangeServer Then
            Dim domain As String = address.GetAddressDomain()

            If String.Compare(domain, "hotmail.com", True) = 0 Then
                TextBoxServer.Text = "smtp.live.com"
            ElseIf String.Compare(domain, "gmail.com", True) = 0 Then
                TextBoxServer.Text = "smtp.gmail.com"
            ElseIf String.Compare(domain, "yahoo.com", True) = 0 Then
                TextBoxServer.Text = "smtp.mail.yahoo.com"
            ElseIf String.Compare(domain, "aol.com", True) = 0 Then
                TextBoxServer.Text = "smtp.aol.com"
            End If

            ChangeSettingforWellKnownServer(TextBoxServer.Text)
        End If
    End Sub

    Private Sub ChangeSettingforWellKnownServer(ByVal server As String)
        If String.Compare(server, "smtp.gmail.com", True) = 0 OrElse String.Compare(server, "smtp.live.com", True) = 0 OrElse String.Compare(server, "smtp.mail.yahoo.com", True) = 0 OrElse String.Compare(server, "smtp.office365.com", True) = 0 OrElse String.Compare(server, "smtp.aol.com", True) = 0 Then
            ComboBoxPorts.SelectedIndex = 1
            CheckBoxSsl.Checked = True
            CheckBoxAuth.Checked = True
        End If
    End Sub

    Private Sub TextBoxServer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxServer.TextChanged
        ChangeSettingforWellKnownServer(TextBoxServer.Text)
    End Sub

    Private Sub _checkBoxAuthChanged()
        TextBoxUser.Enabled = CheckBoxAuth.Checked
        TextBoxPassword.Enabled = CheckBoxAuth.Checked
    End Sub

    Private Sub CheckBoxAuth_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAuth.CheckedChanged
        _checkBoxAuthChanged()
    End Sub

    Private Sub ComboBoxProtocols_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxProtocols.SelectedIndexChanged
        If ComboBoxProtocols.SelectedIndex = 1 Then
            CheckBoxSsl.Checked = True
            ComboBoxPorts.Enabled = False
            CheckBoxAuth.Checked = True
            _checkBoxAuthChanged()
        Else
            ComboBoxPorts.Enabled = True
        End If
    End Sub

#End Region

#Region "AutoSize Control"

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
        _textBoxBodyHeightOffset = Me.Height - TextBoxBody.Height
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
        TextBoxBody.Width = Me.Width - 50
        TextBoxBody.Height = Me.Height - _textBoxBodyHeightOffset
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

#End Region

End Class


Imports System.Text
Imports EASendMail

Public Class Form1
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        _intialize()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        _initControlOffset()
    End Sub

    Private _attachments As List(Of String) = New List(Of String)()
    Private _isCancelSending As Boolean = False
    Private _total As Integer = 0
    Private _succeeded As Integer = 0
    Private _failed As Integer = 0

#Region "Cross Thread Access List Item"
    ' Because some the events are fired on another
    ' thread, to change the control value safety, we used the following functions to 
    ' update control value. more detail, please refer to Control.BeginInvoke method in MSDN
    Private Delegate Sub SetRecipientStatusDelegate(ByVal recipientIndex As Integer, ByVal status As String)

    Private Sub _updateRecipientStatus(ByVal recipientIndex As Integer, ByVal status As String)
        ListViewTo.Items(recipientIndex).SubItems(2).Text = status
    End Sub

    Private Sub _crossThreadUpdateRecipientStatus(ByVal recipientIndex As Integer, ByVal status As String)
        If InvokeRequired Then
            Dim args As Object() = New Object(1) {}
            args(0) = recipientIndex
            args(1) = status
            Dim d As SetRecipientStatusDelegate = New SetRecipientStatusDelegate(AddressOf _updateRecipientStatus)
            BeginInvoke(d, args)
        Else
            _updateRecipientStatus(recipientIndex, status)
        End If
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

    Private Sub _initProtocols()
        ComboBoxProtocols.Items.Add("SMTP Protocol - Recommended")
        ComboBoxProtocols.Items.Add("Exchange Web Service - 2007-2019/Office365")
        ComboBoxProtocols.Items.Add("Exchange WebDav - 2000/2003")
        ComboBoxProtocols.SelectedIndex = 0
        ComboBoxPorts.Items.Add("25")
        ComboBoxPorts.Items.Add("587")
        ComboBoxPorts.Items.Add("465")
        ComboBoxPorts.SelectedIndex = 0
    End Sub

    Private Sub _intialize()
        Dim s As StringBuilder = New StringBuilder()
        s.Append("Hi [$name], " & vbCrLf & "This sample demonstrates how to send email to mutilple recipients." & vbCrLf & vbCrLf)
        s.Append("From: [$from]" & vbCrLf)
        s.Append("To: <[$address]>" & vbCrLf)
        s.Append("Subject: [$subject]" & vbCrLf & vbCrLf)
        s.Append("If no sever address was specified, the email will be delivered to the recipient's server directly." & vbCrLf)
        s.Append("However, if you don't have a static IP address, ")
        s.Append("many anti-spam filters will mark it as a junk-email." & vbCrLf & vbCrLf)
        s.Append("If ""Test Email Address"" was checked, then only the recipient address will be tested and no message will be sent." & vbCrLf)

        TextBoxBody.Text = s.ToString()

        _initCharsets()
        _initProtocols()
        _checkBoxAuthChanged()
    End Sub

#End Region

#Region "Verify and disable control"

    Private Function _verifyInput() As Boolean
        If TextBoxFrom.Text.Length = 0 Then
            MessageBox.Show("Please input From, the format can be test@domain.com or Tester<test@domain.com>")
            TextBoxFrom.Focus()
            Return False
        End If

        If ListViewTo.Items.Count = 0 Then
            MessageBox.Show("Please add a recipient at least!")
            ButtonAddRecipient.Focus()
            Return False
        End If

        If CheckBoxAuth.Checked AndAlso (TextBoxUser.Text.Length = 0 OrElse TextBoxPassword.Text.Length = 0) Then
            MessageBox.Show("Please input user/password for authentication!")
            TextBoxUser.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub _disableControlForSending(ByVal isDisabled As Boolean)
        ButtonSend.Enabled = Not isDisabled
        ButtonSimpleSend.Enabled = Not isDisabled
        ButtonAddAttachment.Enabled = Not isDisabled
        ButtonClearAttachments.Enabled = Not isDisabled
        ButtonAddRecipient.Enabled = Not isDisabled
        ButtonClearRecipients.Enabled = Not isDisabled
        CheckBoxTestRecipient.Enabled = Not isDisabled
        CheckBoxAuth.Enabled = Not isDisabled
        CheckBoxSsl.Enabled = Not isDisabled
        TextBoxFrom.Enabled = Not isDisabled
        TextBoxSubject.Enabled = Not isDisabled
        ComboBoxEncoding.Enabled = Not isDisabled
        ComboBoxPorts.Enabled = Not isDisabled
        ComboBoxProtocols.Enabled = Not isDisabled
        ButtonCancel.Enabled = isDisabled
    End Sub

#End Region

#Region "Statistic Counter"

    Private Sub _resetStatisticCounter()
        _total = ListViewTo.Items.Count
        _succeeded = 0
        _failed = 0

        For i As Integer = 0 To _total - 1
            _crossThreadUpdateRecipientStatus(i, "Ready")
        Next

        ListViewTo.TopItem = ListViewTo.Items(0)
    End Sub

    Private Sub _updateStatisticCounter()
        StatusBarSend.Text = String.Format("Total {0}, Finished {1}, Succeeded {2}, Failed {3}", _total, _succeeded + _failed, _succeeded, _failed)
        Application.DoEvents()
    End Sub

    Private Delegate Sub UpdateResultCounterDelegate(ByVal isSucceeded As Boolean)

    Private Sub _crossThreadUpdateStatisticCounter(ByVal isSucceeded As Boolean)
        If InvokeRequired Then
            Dim args As Object() = New Object(0) {}
            args(0) = isSucceeded
            Dim d As UpdateResultCounterDelegate = New UpdateResultCounterDelegate(AddressOf _updateResultCounter)
            BeginInvoke(d, args)
        Else
            _updateResultCounter(isSucceeded)
        End If
    End Sub

    Private Sub _updateResultCounter(ByVal isSucceeded As Boolean)
        If isSucceeded Then
            _succeeded += 1
        Else
            _failed += 1
        End If

        _updateStatisticCounter()
    End Sub

#End Region

#Region "Create SmtpMail And SmtpServer instance based On Settings Of Form Controls"

    ' You can even use different server and add different attachment based on recipient address.
    Private Function _createMail(ByVal server As SmtpServer, ByVal recipientName As String, ByVal recipientAddress As String) As SmtpMail
    
        ' For evaluation usage, please use "TryIt" as the license code, otherwise the 
        ' "invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        ' "trial version expired" exception will be thrown.

        ' For licensed uasage, please use your license code instead of "TryIt", then the object
        ' will never expire
        Dim mail As SmtpMail = New SmtpMail("TryIt")

        mail.From = TextBoxFrom.Text
        
        mail.[To].Add(New MailAddress(recipientName, recipientAddress))
        
        mail.Subject = TextBoxSubject.Text
        mail.Charset = _charsets(ComboBoxEncoding.SelectedIndex, 1)

        Dim body As String = TextBoxBody.Text
        body = body.Replace("[$subject]", mail.Subject)
        body = body.Replace("[$from]", mail.From.ToString())
        body = body.Replace("[$name]", recipientName)
        body = body.Replace("[$address]", recipientAddress)
        mail.TextBody = body

        Dim count As Integer = _attachments.Count
        For i As Integer = 0 To count - 1
            mail.AddAttachment(TryCast(_attachments(i), String))
        Next

        If server Is Nothing OrElse String.IsNullOrEmpty(server.Server) Then
            Dim cur As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
            Dim gmtDateTime As String = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur)
            gmtDateTime.Remove(gmtDateTime.Length - 3, 1)
            Dim receivedHeader As String = String.Format("from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;" & vbCrLf & vbTab & " {1}", server.HeloDomain, gmtDateTime)
            mail.Headers.Insert(0, New HeaderItem("Received", receivedHeader))
        End If

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
        If Not _verifyInput() Then
            Return
        End If

        _disableControlForSending(True)
        _isCancelSending = False
        
        _resetStatisticCounter()
        _updateStatisticCounter()

        Dim mailThreadPool As SendThreadPool = New SendThreadPool()

        mailThreadPool.UpdateRecipientStatus = AddressOf _crossThreadUpdateRecipientStatus
        mailThreadPool.UpdateResultCounter = AddressOf _crossThreadUpdateStatisticCounter
        mailThreadPool.Reset(CInt(NumericUpDownConnections.Value), 0)
        
        Dim recipientIndex As Integer = 0

        While recipientIndex < _total AndAlso Not _isCancelSending
            Dim item As ListViewItem = ListViewTo.Items(recipientIndex)
            Dim recipientName As String = item.Text
            Dim recipientAddress As String = item.SubItems(1).Text

            Dim server As SmtpServer = _createSmtpServer()
            Dim mail As SmtpMail = _createMail(server, recipientName, recipientAddress)

            While Not mailThreadPool.SubmitMessage(server, mail, CheckBoxTestRecipient.Checked, recipientIndex)
                Application.DoEvents()
            End While

            If _isCancelSending Then
                Exit While
            End If

            recipientIndex += 1
        End While

        If _isCancelSending Then
            mailThreadPool.CancelAll()

            While recipientIndex < _total
                _crossThreadUpdateRecipientStatus(recipientIndex, "Operation was cancelled")
                recipientIndex += 1
            End While
        End If

        While mailThreadPool.UnfinishedMessages <> 0
            Application.DoEvents()
        End While

        _disableControlForSending(False)
    End Sub

#Region "Add/Clear Attachments"

    Private Sub ButtonAddAttachment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddAttachment.Click
        attachmentDlg.Reset()
        attachmentDlg.Multiselect = True
        attachmentDlg.CheckFileExists = True
        attachmentDlg.CheckPathExists = True

        If attachmentDlg.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        Dim attachments As String() = attachmentDlg.FileNames

        For i As Integer = 0 To attachments.Length - 1
            Dim fileName As String = attachments(i)
            _attachments.Add(fileName)
            Dim pos As Integer = fileName.LastIndexOf("\")

            If pos <> -1 Then
                fileName = fileName.Substring(pos + 1)
            End If

            TextBoxAttachments.Text += fileName
            TextBoxAttachments.Text += ";"
        Next
    End Sub

    Private Sub ButtonClearAttachments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClearAttachments.Click
        _attachments.Clear()
        TextBoxAttachments.Text = ""
    End Sub

#End Region

    Private Sub ButtonAddRecipient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddRecipient.Click
        Using formRecipient As FormRecipient = New FormRecipient()

            If formRecipient.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If

            Dim recipientName As String = formRecipient.TextBoxName.Text.Trim()
            Dim recipientAddress As String = formRecipient.TextBoxAddress.Text.Trim()

            ' For i As Integer = 0 To 20 - 1 ' this line is for test
            Dim item As ListViewItem = ListViewTo.Items.Add(recipientName)
            item.SubItems.Add(recipientAddress)
            item.SubItems.Add("Ready")
            ' Next ' this line is for test
        End Using
    End Sub

    Private Sub ButtonClearRecipients_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClearRecipients.Click
        ListViewTo.Items.Clear()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        _isCancelSending = True
        ButtonCancel.Enabled = False
    End Sub

    Private Sub CheckBoxTestRecipient_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxTestRecipient.CheckedChanged
        TextBoxServer.Enabled = (Not CheckBoxTestRecipient.Checked)
        ComboBoxProtocols.SelectedIndex = 0
        ComboBoxProtocols.Enabled = (Not CheckBoxTestRecipient.Checked)
    End Sub

#Region "Send Mass E-mail with Simple Code(single thread)"

    Private Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = _isCancelSending

        If Not cancel Then
            Application.DoEvents()
        End If
    End Sub

    Private Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = CInt(smtp.Tag)
        _crossThreadUpdateRecipientStatus(recipientIndex, "Connected")
        cancel = _isCancelSending
    End Sub

    Private Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = CInt(smtp.Tag)
        _crossThreadUpdateRecipientStatus(recipientIndex, If((sent <> total), String.Format("Sending {0}/{1} ... ", sent, total), "Disconnecting ..."))
        cancel = _isCancelSending
    End Sub

    Private Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = CInt(smtp.Tag)
        _crossThreadUpdateRecipientStatus(recipientIndex, "Authorized")
        cancel = _isCancelSending
    End Sub

    Private Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = CInt(smtp.Tag)
        _crossThreadUpdateRecipientStatus(recipientIndex, "Securing ...")
        cancel = _isCancelSending
    End Sub

    Private Sub ButtonSimpleSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSimpleSend.Click
        If Not _verifyInput() Then
            Return
        End If

        MessageBox.Show("Simple Send will send email with single thread, the code is vey simple." & vbCrLf & "If you don't want the extreme performance, the code is recommended to beginer!")
        _disableControlForSending(True)
        _isCancelSending = False

        _resetStatisticCounter()
        _updateStatisticCounter()

        Dim recipientIndex As Integer = 0
        While recipientIndex < _total AndAlso Not _isCancelSending

            Try
                Dim item As ListViewItem = ListViewTo.Items(recipientIndex)
                Dim recipientName As String = item.Text
                Dim recipientAddress As String = item.SubItems(1).Text
                Dim server As SmtpServer = _createSmtpServer()
                Dim mail As SmtpMail = _createMail(server, recipientName, recipientAddress)
                Dim smtp As SmtpClient = New SmtpClient()
                smtp.Tag = recipientIndex

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

                _crossThreadUpdateRecipientStatus(recipientIndex, "Connecting...")

                If Not CheckBoxTestRecipient.Checked Then
                    smtp.SendMail(server, mail)
                    _crossThreadUpdateRecipientStatus(recipientIndex, "Completed")
                Else
                    smtp.TestRecipients(server, mail)
                    _crossThreadUpdateRecipientStatus(recipientIndex, "PASS")
                End If

                _succeeded += 1
            Catch exp As Exception
                _crossThreadUpdateRecipientStatus(recipientIndex, String.Format("Error: {0}", exp.Message))
                _failed += 1
            Finally
                _updateStatisticCounter()
            End Try

            recipientIndex += 1
        End While

        If _isCancelSending Then

            While recipientIndex < _total
                _crossThreadUpdateRecipientStatus(recipientIndex, "Operation was cancelled")
                recipientIndex += 1
            End While
        End If

        _disableControlForSending(False)
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
    Private _buttonSendTopOffset As Integer = 0
    Private _buttonSendLeftOffset As Integer = 0
    Private _buttonSimpleSendLeftOffset As Integer = 0
    Private _buttonCancelLeftOffset As Integer = 0

    Private Sub _initControlOffset()
        Me.MinimumSize = New System.Drawing.Size(Me.Width, Me.Height)
        _isFormLoaded = True
        _textBoxAttachmentsWidthOffset = Me.Width - TextBoxAttachments.Width
        _textBoxBodyHeightOffset = Me.Height - TextBoxBody.Height
        _buttonSendTopOffset = Me.Height - ButtonSend.Top
        _textBoxFromWidthOffset = Me.Width - TextBoxFrom.Width
        _buttonAddAttachmentLeftOffset = Me.Width - ButtonAddAttachment.Left
        _buttonClearAttachmentsLeftOffset = Me.Width - ButtonClearAttachments.Left
        _serverGroupBoxOffset = Me.Width - GroupBoxServer.Left
        _buttonSendLeftOffset = Me.Width - ButtonSend.Left
        _buttonSimpleSendLeftOffset = Me.Width - ButtonSimpleSend.Left
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
        ButtonSend.Top = Me.Height - _buttonSendTopOffset
        ButtonCancel.Top = ButtonSend.Top
        ButtonSimpleSend.Top = ButtonSend.Top
        GroupBoxServer.Left = Me.Width - _serverGroupBoxOffset
        ButtonSend.Left = Me.Width - _buttonSendLeftOffset
        ButtonSimpleSend.Left = Me.Width - _buttonSimpleSendLeftOffset
        ButtonCancel.Left = Me.Width - _buttonCancelLeftOffset
        TextBoxFrom.Width = Me.Width - _textBoxFromWidthOffset
        TextBoxSubject.Width = TextBoxFrom.Width
        ListViewTo.Width = TextBoxFrom.Width
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

End Class

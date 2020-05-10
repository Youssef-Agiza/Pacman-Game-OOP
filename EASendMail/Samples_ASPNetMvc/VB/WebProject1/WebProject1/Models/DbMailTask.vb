Imports System.ComponentModel.DataAnnotations
Imports EASendMail

Public Class DbMailTask
    <Key, ScaffoldColumn(False)>
    Public Property TaskId As Integer

    <MaxLength(250), Display(Name:="Task Name")>
    Public Property TaskName As String

    <Display(Name:="Server"), MaxLength(128)>
    Public Property Server As String

    <Required>
    Public Property Port As Integer = 25

    <Display(Name:="My server requires user authentication")>
    Public Property IsAuthenticationRequired As Boolean

    <RequiredIf("IsAuthenticationRequired", True, ErrorMessage:="User name is required"), MaxLength(128)>
    Public Property User As String

    <RequiredIf("IsAuthenticationRequired", True, ErrorMessage:="Password is required"), MaxLength(128)>
    Public Property Password As String

    <Display(Name:="TLS/SSL Connection")>
    Public Property IsSslConnection As Boolean

    Public Property Protocol As ServerProtocol = ServerProtocol.SMTP
    Public Property AuthType As SmtpAuthType = SmtpAuthType.AuthAuto

    <Required, Display(Name:="Sender")>
    Public Property Sender As String

    <Display(Name:="Reply-To")>
    Public Property ReplyTo As String

    <Required>
    Public Property Subject As String

    <Display(Name:="Text Body")>
    Public Property TextBody As String

    <Display(Name:="Html Body")>
    Public Property HtmlBody As String

    Public Property Status As DbMailTaskStatus

    <Display(Name:="Total Count")>
    Public Property TotalCount As Integer = 0
    Public Property Succeeded As Integer = 0
    Public Property Failed As Integer = 0

    <Display(Name:="Creation Time")>
    Public Property CreationTime As DateTime = DateTime.Now

    <Display(Name:="Update Time")>
    Public Property LastWriteTime As DateTime = DateTime.Now

    Public Function BuildServer() As SmtpServer
        Dim myServer = New SmtpServer(If(String.IsNullOrWhiteSpace(Server), String.Empty, Server.Trim()))
        myServer.Protocol = Protocol
        myServer.Port = Port
        myServer.ConnectType = If((IsSslConnection), SmtpConnectType.ConnectSSLAuto, SmtpConnectType.ConnectTryTLS)

        If IsAuthenticationRequired Then
            myServer.AuthType = AuthType
            myServer.User = (If(String.IsNullOrWhiteSpace(User), String.Empty, User.Trim()))
            myServer.Password = (If(String.IsNullOrWhiteSpace(Password), String.Empty, Password.Trim()))
        End If

        Return myServer
    End Function

    Public Function BuildMail() As SmtpMail
        Dim mail = New SmtpMail("TryIt")
        mail.From = (If(String.IsNullOrWhiteSpace(Sender), String.Empty, Sender.Trim()))
        mail.Subject = If(String.IsNullOrEmpty(Subject), String.Empty, Subject)

        If Not String.IsNullOrEmpty(ReplyTo) Then
            Dim myReplyTo = New MailAddress(ReplyTo)

            If String.Compare(myReplyTo.Address, mail.From.Address, True) <> 0 Then
                mail.ReplyTo = myReplyTo
            End If
        End If

        If Not String.IsNullOrEmpty(TextBody) Then
            mail.TextBody = TextBody
        End If

        If Not String.IsNullOrEmpty(HtmlBody) Then
            mail.HtmlBody = HtmlBody
        End If

        Return mail
    End Function

    Public Function Copy() As DbMailTask
        Dim mailTask = New DbMailTask()
        mailTask.AuthType = Me.AuthType
        mailTask.CreationTime = Me.CreationTime
        mailTask.HtmlBody = Me.HtmlBody
        mailTask.IsAuthenticationRequired = Me.IsAuthenticationRequired
        mailTask.IsSslConnection = Me.IsSslConnection
        mailTask.LastWriteTime = Me.LastWriteTime
        mailTask.Password = Me.Password
        mailTask.Port = Me.Port
        mailTask.Protocol = Me.Protocol
        mailTask.ReplyTo = Me.ReplyTo
        mailTask.Sender = Me.Sender
        mailTask.Server = Me.Server
        mailTask.Subject = Me.Subject
        mailTask.TaskId = Me.TaskId
        mailTask.TaskName = Me.TaskName
        mailTask.TextBody = Me.TextBody
        mailTask.User = Me.User
        Return mailTask
    End Function
End Class

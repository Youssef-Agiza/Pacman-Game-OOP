Imports System.Web.Mvc
Imports System.Text
Imports EASendMail

Namespace Controllers
    Public Class SimpleMailController
        Inherits Controller

        ' GET: SimpleMail
        Function Index() As ActionResult

            Dim myTask = New MailTask()

            myTask.Subject = "Test email from ASP.NET MVC"
            myTask.IsAuthenticationRequired = False

            Dim body = New StringBuilder()
            body.Append("This sample demonstrates how to send simple email from ASP.NET MVC." & vbCrLf & vbCrLf)
            body.Append("If no sever address was specified, the email will be delivered to the recipient's server directly." & vbCrLf)
            body.Append("However, it is not recommended, because most email providers would reject your message due to anti-spam policy." & vbCrLf)

            myTask.TextBody = body.ToString()

            ViewBag.Port = DropDownListData.PortList(myTask.Port)
            ViewBag.Protocol = DropDownListData.ProtocolList(myTask.Protocol)

            ViewBag.IsSyncSendSucceeded = False
            ViewBag.SyncSendStatus = String.Empty

            Return View(myTask)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Index(ByVal myTask As MailTask) As ActionResult
            ViewBag.Port = DropDownListData.PortList(myTask.Port)
            ViewBag.Protocol = DropDownListData.ProtocolList(myTask.Protocol)

            If ModelState.IsValid Then
                _syncSendMail(myTask)
            End If

            Return View(myTask)
        End Function

        Private Sub _syncSendMail(ByVal myTask As MailTask)
            Try
                Dim smtp = New SmtpClient()

                Dim server = myTask.BuildServer()
                Dim mail = myTask.BuildMail()

                smtp.SendMail(server, mail)

                ViewBag.IsSyncSendSucceeded = True
                ViewBag.SyncSendStatus = "Message has been submitted to server successfully."
            Catch ep As Exception
                ViewBag.IsSyncSendSucceeded = False
                ViewBag.SyncSendStatus = ep.Message
            End Try
        End Sub

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function AsyncSend(ByVal mailTask As MailTask) As ActionResult
            AsyncSendMailService.CreateAsyncTask(mailTask)
            Return Content(mailTask.TaskId)
        End Function

        Public Function QueryAsyncTask(ByVal id As String) As ActionResult
            Dim status As AsyncSendMailStatus = AsyncSendMailService.QueryStatus(id)

            If status Is Nothing Then
                status = New AsyncSendMailStatus()
                status.TaskId = id
                status.Completed = True
                status.HasError = True
                status.Status = "Invalid Task ID"
            End If

            Return Json(status, JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace
Imports System.Web.Mvc

Namespace Controllers
    Public Class MassController
        Inherits Controller

        ' GET: Mass
        Public Function Index() As ActionResult
            Dim myTask = New MailTask()
            myTask.Subject = "Test email from ASP.NET MVC"
            myTask.IsAuthenticationRequired = False

            Dim body = New StringBuilder()
            body.Append("This sample demonstrates how to send multiple emails from ASP.NET MVC with thread pool." & vbCrLf & vbCrLf)
            body.Append("From: [$sender]" & vbCrLf)
            body.Append("To: [$rcpt]" & vbCrLf)
            body.Append("Subject: [$subject]" & vbCrLf & vbCrLf)
            body.Append("Above sender, rcpt, subject values will be replaced by actual value based on each recipient." & vbCrLf & vbCrLf)
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
        Public Function AsyncSend(ByVal myTask As MailTask) As ActionResult
            MassSendMailService.CreateAsyncTask(myTask)
            Return Content(myTask.TaskId)
        End Function

        Public Function QueryAsyncTask(ByVal id As String) As ActionResult
            Dim status = MassSendMailService.QueryStatus(id)

            If status Is Nothing Then
                status = New MassSendMailStatus()
                status.TaskId = id
                status.Completed = True
                status.HasError = True
                status.Status = "Invalid Task ID"
            End If

            Return Json(status, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
Imports System.Net
Imports System.Web.Mvc
Imports WebProject1.Models

Namespace Controllers
    Public Class DbMailTasksController
        Inherits Controller

        Private db As WebProject1Context = New WebProject1Context()

        Public Function Index() As ActionResult
            Return TaskList()
        End Function

        Public Function TaskList() As ActionResult
            Return PartialView(db.DbMailTasks.ToList())
        End Function

        Public Function Details(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim myTask As DbMailTask = db.DbMailTasks.Find(id)

            If myTask Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(myTask)
        End Function

        Public Function Create() As ActionResult
            ViewBag.RecipientCount = db.DbRecipients.Count()

            Dim myTask = New DbMailTask()
            myTask.Subject = "Test email from ASP.NET MVC"
            myTask.IsAuthenticationRequired = False

            Dim body = New StringBuilder()
            body.Append("This sample demonstrates how to pickup recipient from database and send emails from ASP.NET MVC with thread pool." & vbCrLf)
            body.Append("Curren task id is: [$taskid]" & vbCrLf & vbCrLf)
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
        Public Function Create(ByVal myTask As DbMailTask) As ActionResult
            If ModelState.IsValid Then

                If String.IsNullOrWhiteSpace(myTask.TaskName) Then
                    myTask.TaskName = "Unnamed task"
                End If

                myTask.CreationTime = DateTime.Now
                myTask.LastWriteTime = DateTime.Now

                db.DbMailTasks.Add(myTask)
                db.SaveChanges()
                Return RedirectToAction("Index", "DbRecipients")
            End If

            Return View(myTask)
        End Function

        Public Function Delete(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim myTask As DbMailTask = db.DbMailTasks.Find(id)

            If myTask Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(myTask)
        End Function

        <HttpPost, ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Public Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim myTask As DbMailTask = db.DbMailTasks.Find(id)

            If myTask.Status = DbMailTaskStatus.Completed OrElse myTask.Status = DbMailTaskStatus.Terminated Then
                db.DbMailTasks.Remove(myTask)
                db.Database.ExecuteSqlCommand("DELETE FROM DbRecipientResults WHERE TaskId = {0}", id)
                db.SaveChanges()
            End If

            Return RedirectToAction("Index", "DbRecipients")
        End Function

        Public Function QueryTasks() As ActionResult
            Dim tasks = db.DbMailTasks.[Select](
                Function(p) New With {
                    p.TaskId, p.TaskName, p.TotalCount, p.Status, p.Failed, p.Succeeded, p.CreationTime
                }
                ).ToList()
            Return Json(tasks, JsonRequestBehavior.AllowGet)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                db.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub
    End Class

End Namespace
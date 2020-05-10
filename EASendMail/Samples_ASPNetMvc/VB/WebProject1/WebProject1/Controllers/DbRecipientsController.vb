Imports System.Data.Entity
Imports System.Net
Imports System.Web.Mvc
Imports WebProject1.Models

Namespace Controllers
    Public Class DbRecipientsController
        Inherits Controller

        Private db As WebProject1Context = New WebProject1Context()

        Public Function Index() As ActionResult
            Return View(db.DbRecipients.ToList())
        End Function

        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Create(
        <Bind(Include:="RecipientId,Name,Address")> ByVal recipient As DbRecipient) As ActionResult
            If ModelState.IsValid Then
                db.DbRecipients.Add(recipient)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If

            Return View(recipient)
        End Function

        Public Function Edit(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim recipient As DbRecipient = db.DbRecipients.Find(id)

            If recipient Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(recipient)
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Edit(
        <Bind(Include:="RecipientId,Name,Address")> ByVal recipient As DbRecipient) As ActionResult
            If ModelState.IsValid Then
                db.Entry(recipient).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If

            Return View(recipient)
        End Function

        Public Function Delete(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim recipient As DbRecipient = db.DbRecipients.Find(id)

            If recipient Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(recipient)
        End Function

        <HttpPost, ActionName("Delete")>
        <ValidateAntiForgeryToken>
        Public Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim recipient As DbRecipient = db.DbRecipients.Find(id)
            db.DbRecipients.Remove(recipient)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                db.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
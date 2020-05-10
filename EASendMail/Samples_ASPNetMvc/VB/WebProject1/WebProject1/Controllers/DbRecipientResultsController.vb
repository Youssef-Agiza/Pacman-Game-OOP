Imports System.Web.Mvc
Imports WebProject1.Models

Namespace Controllers
    Public Class DbRecipientResultsController
        Inherits Controller

        Private db As WebProject1Context = New WebProject1Context()

        ' GET: DbRecipientResults
        Public Function Index(ByVal id As Integer?) As ActionResult
            Dim results = If((id Is Nothing), db.DbRecipientResults.ToList(), db.DbRecipientResults.Where(Function(item) item.TaskId = id).ToList())
            Return View(results)
        End Function
    End Class
End Namespace
Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    Function Support() As ActionResult
        ViewBag.Message = "Techical Support"
        Return View()
    End Function
End Class

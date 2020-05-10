Public Class MassSendMailStatus
    Public Property TaskId As String = String.Empty
    Public Property Completed As Boolean = False
    Public Property Status As String = String.Empty
    Public Property HasError As Boolean = False
    Public Property Progress As Integer = 0
    Public Property TotalCount As Integer = 0
    Public Property Succeeded As Integer = 0
    Public Property Failed As Integer = 0
    Public Property ActiveRecipientStatus As RecipientStatus() = New RecipientStatus(-1) {}
End Class

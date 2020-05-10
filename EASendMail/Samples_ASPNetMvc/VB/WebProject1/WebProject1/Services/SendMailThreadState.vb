Imports EASendMail

Class SendMailThreadState
    Public Property Mail As SmtpMail = Nothing
    Public Property Server As SmtpServer = Nothing
    Public Property RecipientIndex As Integer = 0
    Public Property TaskId As String = String.Empty
    Public Property DbTaskId As Integer = 0
End Class

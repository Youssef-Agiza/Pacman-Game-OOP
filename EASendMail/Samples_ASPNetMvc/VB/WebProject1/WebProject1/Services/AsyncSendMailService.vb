Imports System.Threading.Tasks
Imports EASendMail

Public Class AsyncSendMailService
    Private Shared _taskStatus As Dictionary(Of String, AsyncSendMailStatus) = Nothing
    Private Shared _lock As Object = Nothing

    Public Shared Sub Init()
        _lock = New Object()
        _taskStatus = New Dictionary(Of String, AsyncSendMailStatus)()
    End Sub

    Public Shared Sub CreateAsyncTask(ByVal myTask As MailTask)
        SyncLock _lock
            Dim status As AsyncSendMailStatus = New AsyncSendMailStatus()
            status.TaskId = myTask.TaskId

            If Not _taskStatus.ContainsKey(myTask.TaskId) Then
                _taskStatus.Add(myTask.TaskId, status)
                Task.Run(
                    Sub()
                        _sendEmail(status, myTask)
                    End Sub
                    )
            End If
        End SyncLock
    End Sub

    Public Shared Sub PutErrorStatus(ByVal taskId As String, ByVal [error] As String)
        SyncLock _lock
            Dim status As AsyncSendMailStatus = New AsyncSendMailStatus()
            status.TaskId = taskId
            status.HasError = True
            status.Status = [error]
            status.Completed = True

            If Not _taskStatus.ContainsKey(taskId) Then
                _taskStatus.Add(taskId, status)
            End If
        End SyncLock
    End Sub

    Public Shared Function QueryStatus(ByVal taskId As String) As AsyncSendMailStatus
        Dim task As AsyncSendMailStatus = Nothing

        SyncLock _lock

            If _taskStatus.ContainsKey(taskId) Then
                task = _taskStatus(taskId)

                If task.Completed Then
                    _taskStatus.Remove(taskId)
                End If
            End If
        End SyncLock

        Return task
    End Function

    Private Shared Sub _sendEmail(ByVal status As AsyncSendMailStatus, ByVal myTask As MailTask)
        Try
            Dim server = myTask.BuildServer()
            Dim mail = myTask.BuildMail()

            Dim smtp = New SmtpClient()
            ' EASendMail event handler
            AddHandler smtp.OnAuthorized, AddressOf OnAuthorized
            AddHandler smtp.OnConnected, AddressOf OnConnected
            AddHandler smtp.OnSecuring, AddressOf OnSecuring
            AddHandler smtp.OnSendingDataStream, AddressOf OnSendingDataStream

            status.Status = "Connecting server ..."
            smtp.Tag = status

            smtp.SendMail(server, mail)

            status.Status = "Completed"
            status.HasError = False
            status.Progress = 100
        Catch ep As Exception
            status.Status = ep.Message
            status.HasError = True
            status.Progress = 100
        End Try

        status.Completed = True
    End Sub

#Region "EASendMail Event Handler"

    Public Shared Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        Dim status = CType((CType(sender, SmtpClient)).Tag, AsyncSendMailStatus)
        status.Status = "Securing ..."
    End Sub

    Public Shared Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        Dim status = CType((CType(sender, SmtpClient)).Tag, AsyncSendMailStatus)
        status.Status = "Authorized"
    End Sub

    Public Shared Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        Dim status = CType((CType(sender, SmtpClient)).Tag, AsyncSendMailStatus)
        status.Status = "Connected"
    End Sub

    Public Shared Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        Dim status = CType((CType(sender, SmtpClient)).Tag, AsyncSendMailStatus)

        If total = 0 Then
            Return
        End If

        Dim progress As Integer = (sent * 100) / total
        status.Progress = progress
        status.Status = String.Format("Sending {0}% ...", progress)
    End Sub

#End Region

End Class


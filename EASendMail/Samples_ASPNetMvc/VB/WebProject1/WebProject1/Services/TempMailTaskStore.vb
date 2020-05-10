Public Class TempMailTaskStore
    Private Shared _tasks As Dictionary(Of String, MailTask) = Nothing
    Private Shared _lock As Object = Nothing

    Public Shared Sub Init()
        _lock = New Object()
        _tasks = New Dictionary(Of String, MailTask)()
    End Sub

    Public Shared Sub PutTask(ByVal mailTask As MailTask)
        SyncLock _lock

            If Not _tasks.ContainsKey(mailTask.TaskId) Then
                _tasks.Add(mailTask.TaskId, mailTask)
            End If
        End SyncLock
    End Sub

    Public Shared Function GetTask(ByVal taskId As String) As MailTask
        Dim task As MailTask = Nothing

        If String.IsNullOrEmpty(taskId) Then
            Return task
        End If

        SyncLock _lock

            If _tasks.ContainsKey(taskId) Then
                task = _tasks(taskId)
                _tasks.Remove(taskId)
            End If
        End SyncLock

        Return task
    End Function
End Class

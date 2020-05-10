Imports System.Threading.Tasks

Public Class MassSendMailService
    Private Shared _threadPools As Dictionary(Of String, MassSendThreadPool) = Nothing
    Private Shared _lock As Object = Nothing

    Public Shared Sub Init()
        _lock = New Object()
        _threadPools = New Dictionary(Of String, MassSendThreadPool)()
    End Sub

    Public Shared Sub CreateAsyncTask(ByVal mailTask As MailTask)
        SyncLock _lock
            Dim status = New MassSendMailStatus()
            status.TaskId = mailTask.TaskId

            If Not _threadPools.ContainsKey(mailTask.TaskId) Then
                Dim threadPool As MassSendThreadPool = New MassSendThreadPool()
                _threadPools.Add(mailTask.TaskId, threadPool)
                Task.Run(Sub()
                             threadPool.Start(mailTask)
                         End Sub)
            End If
        End SyncLock
    End Sub

    Public Shared Function QueryStatus(ByVal taskId As String) As MassSendMailStatus
        Dim status As MassSendMailStatus = Nothing

        SyncLock _lock

            If _threadPools.ContainsKey(taskId) Then
                Dim threadPool = _threadPools(taskId)
                status = threadPool.Status()

                If status.Completed Then
                    _threadPools.Remove(taskId)
                End If
            End If
        End SyncLock

        Return status
    End Function
End Class
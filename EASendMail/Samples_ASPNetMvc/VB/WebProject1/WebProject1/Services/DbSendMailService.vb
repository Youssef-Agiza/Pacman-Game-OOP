Imports System.Data.Entity
Imports System.Threading
Imports System.Threading.Tasks
Imports WebProject1.Models

Public Class DbSendMailService
    Private Shared _threadPools As Dictionary(Of Integer, DbSendThreadPool) = Nothing
    Private Shared _lock As Object = Nothing

    Public Shared Sub Init()
        _lock = New Object()
        _threadPools = New Dictionary(Of Integer, DbSendThreadPool)()

        Dim taskThread As Thread = New Thread(New ThreadStart(AddressOf _queryTaskThreadProc))
        taskThread.Start()
    End Sub

    Private Shared Sub _queryTaskThreadProc()
        Using db = New WebProject1Context()
            Dim unfinishedTasks = (From s In db.DbMailTasks Select s).Where(Function(s) s.Status = DbMailTaskStatus.Running).ToArray()

            For i As Integer = 0 To unfinishedTasks.Length - 1
                Dim task = unfinishedTasks(i)
                task.Status = DbMailTaskStatus.Terminated
                db.Entry(task).State = EntityState.Modified
                db.SaveChanges()
            Next

            Dim miniSleep As Integer = 2

            While True
                Dim tasks = (From s In db.DbMailTasks Select s).Where(Function(s) s.Status = DbMailTaskStatus.Pending).ToArray()

                If tasks.Length = 0 Then
                    Thread.Sleep(miniSleep)

                    If miniSleep * miniSleep < 1024 Then
                        miniSleep = miniSleep * miniSleep
                    End If

                    Continue While
                End If

                miniSleep = 2

                For i As Integer = 0 To tasks.Length - 1
                    _processTask(db, tasks(i))
                Next
            End While
        End Using
    End Sub

    Private Shared Sub _updateResult(ByVal taskId As Integer, ByVal isSucceeded As Boolean, ByVal recipientAddress As String, ByVal status As String)
        Using db = New WebProject1Context()
            Dim result As DbRecipientResult = New DbRecipientResult()
            result.CreationTime = DateTime.Now
            result.IsSucceeded = isSucceeded
            result.Recipient = recipientAddress
            result.TaskId = taskId
            result.Status = status
            db.DbRecipientResults.Add(result)
            db.SaveChanges()

            Dim task = db.DbMailTasks.Find(taskId)
            Dim taskStatus = _queryTaskStatus(taskId)

            If task IsNot Nothing AndAlso taskStatus IsNot Nothing Then
                task.Failed = taskStatus.Failed
                task.Succeeded = taskStatus.Succeeded
                task.TotalCount = taskStatus.TotalCount

                If taskStatus.Completed Then
                    task.Status = DbMailTaskStatus.Completed
                End If

                db.Entry(task).State = EntityState.Modified
                db.SaveChanges()
            End If
        End Using
    End Sub

    Private Shared Sub _processTask(ByVal db As WebProject1Context, ByVal mailTask As DbMailTask)
        Dim threadPool = New DbSendThreadPool()
        threadPool.UpdateResult = AddressOf _updateResult
        _threadPools.Add(mailTask.TaskId, threadPool)
        Dim recipients = db.DbRecipients.ToArray()
        mailTask.TotalCount = recipients.Length
        mailTask.Status = DbMailTaskStatus.Running
        db.Entry(mailTask).State = EntityState.Modified
        db.SaveChanges()
        Task.Run(Sub()
                     threadPool.Start(mailTask, recipients)
                 End Sub)
    End Sub

    Public Shared Function _queryTaskStatus(ByVal taskId As Integer) As DbSendMailStatus
        Dim status As DbSendMailStatus = Nothing

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

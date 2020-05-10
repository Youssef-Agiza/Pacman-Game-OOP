Imports System.Threading
Imports EASendMail

Public Class DbSendThreadPool
    Private _trafficController As TrafficController = Nothing
    Private _activeRecipients As Dictionary(Of Integer, RecipientStatus) = New Dictionary(Of Integer, RecipientStatus)()
    Private _threadCounter As Integer = 0

    Private _status As DbSendMailStatus = New DbSendMailStatus()

    Public Delegate Sub UpdateResultDelegate(ByVal taskId As Integer, ByVal isSucceeded As Boolean, ByVal recipientAddress As String, ByVal status As String)
    Public UpdateResult As UpdateResultDelegate = Nothing

    Public Function Status() As DbSendMailStatus
        SyncLock Me

            If _status.TotalCount > 0 Then
                _status.Progress = ((_status.Succeeded + _status.Failed) * 100) / _status.TotalCount

                If _status.Completed Then
                    _status.Status = String.Format("Total {0}, Succeeded {1}, Failed {2}.", _status.TotalCount, _status.Succeeded, _status.Failed)
                Else
                    _status.Status = String.Format("Total {0}, Succeeded {1}, Failed {2}, Waiting {3} ...", _status.TotalCount, _status.Succeeded, _status.Failed, _status.TotalCount - _status.Failed - _status.Succeeded)
                End If
            Else
                _status.Status = "Please wait ..."
            End If

            _status.ActiveRecipientStatus = _activeRecipients.Values.ToArray()
            Return _status
        End SyncLock
    End Function

    Private _recipients As DbRecipient() = Nothing

    Public Sub Start(ByVal mailTaskTemplate As DbMailTask, ByVal recipients As DbRecipient())
        If _trafficController Is Nothing Then
            _trafficController = New TrafficController()
        End If

        _recipients = recipients
        _status.TotalCount = _recipients.Length
        _status.Succeeded = 0
        _status.Failed = 0

        For recipientIndex As Integer = 0 To _recipients.Length - 1
            Dim mailTask = mailTaskTemplate.Copy()

            While Not _submitMessage(recipientIndex, mailTask, _recipients(recipientIndex))
                Thread.Sleep(10)
            End While
        Next

        While _threadCounter > 0 OrElse Not _status.Completed
            Thread.Sleep(50)
        End While
    End Sub

    Private Function _submitMessage(ByVal recipientIndex As Integer, ByVal mailTask As DbMailTask, ByVal recipient As DbRecipient) As Boolean
        If Not _trafficController.PrepareIncreaseConnection() Then
            _trafficController.Rollback()
            Return False
        End If

        If Not _trafficController.PrepareIncreaseMessage() Then
            _trafficController.Rollback()
            Return False
        End If

        Dim state As SendMailThreadState = New SendMailThreadState()
        state.Server = mailTask.BuildServer()
        state.Mail = mailTask.BuildMail()
        state.DbTaskId = mailTask.TaskId
        Dim recipientName As String = If(String.IsNullOrWhiteSpace(recipient.Name), String.Empty, recipient.Name)
        Dim recipientAddress As String = If(String.IsNullOrWhiteSpace(recipient.Address), String.Empty, recipient.Address)
        state.Mail.[To].Add(New MailAddress(recipientName, recipientAddress))
        state.Mail.TextBody = state.Mail.TextBody.Replace("[$sender]", state.Mail.From.ToString())
        state.Mail.TextBody = state.Mail.TextBody.Replace("[$rcpt]", state.Mail.[To].ToString())
        state.Mail.TextBody = state.Mail.TextBody.Replace("[$subject]", state.Mail.Subject.ToString())
        state.Mail.TextBody = state.Mail.TextBody.Replace("[$taskid]", mailTask.TaskId.ToString())
        state.RecipientIndex = recipientIndex

        Try
            Interlocked.Increment(_threadCounter)
            ThreadPool.QueueUserWorkItem(AddressOf Me._sendThreadProc, state)
            _trafficController.Commit()
            Return True
        Catch
            Interlocked.Decrement(_threadCounter)
            _trafficController.Rollback()
            Return False
        End Try
    End Function

    Private Sub _sendThreadProc(ByVal state As Object)
        Dim threadState As SendMailThreadState = CType(state, SendMailThreadState)
        Dim recipientIndex As Integer = threadState.RecipientIndex
        Dim status As RecipientStatus = New RecipientStatus()
        status.RecipientIndex = recipientIndex
        status.Recipient = threadState.Mail.[To].ToString()

        Try
            Dim smtp As SmtpClient = New SmtpClient()
            smtp.Tag = status

            ' EASendMail event handler
            AddHandler smtp.OnAuthorized, AddressOf OnAuthorized
            AddHandler smtp.OnConnected, AddressOf OnConnected
            AddHandler smtp.OnSecuring, AddressOf OnSecuring
            AddHandler smtp.OnSendingDataStream, AddressOf OnSendingDataStream

            status.Status = "Connecting server ..."
            _updateRecipientStatus(status)
            smtp.SendMail(threadState.Server, threadState.Mail)
            status.Status = "Succeeded"
            status.Completed = True
            _updateRecipientStatus(status)
            _updateResultCounter(True)

            If UpdateResult IsNot Nothing Then
                Dim recipientAddress As String = (TryCast(threadState.Mail.[To](0), MailAddress)).Address
                UpdateResult(threadState.DbTaskId, True, recipientAddress, "Completed")
            End If

        Catch ep As Exception
            status.Status = ep.Message
            status.HasError = True
            status.Completed = True
            _updateRecipientStatus(status)
            _updateResultCounter(False)

            If UpdateResult IsNot Nothing Then
                Dim recipientAddress As String = (TryCast(threadState.Mail.[To](0), MailAddress)).Address
                UpdateResult(threadState.DbTaskId, False, recipientAddress, ep.Message)
            End If

        Finally
            Interlocked.Decrement(_threadCounter)
            _trafficController.DecreaseConnection()
        End Try
    End Sub

    Private Sub _updateRecipientStatus(ByVal status As RecipientStatus)
        SyncLock Me
            Dim threadId As Integer = Thread.CurrentThread.ManagedThreadId

            If Not _activeRecipients.ContainsKey(threadId) Then
                _activeRecipients.Add(threadId, status)
            End If

            If status.Completed Then
                _activeRecipients.Remove(threadId)
            End If
        End SyncLock
    End Sub

    Private Sub _updateResultCounter(ByVal isSucceeded As Boolean)
        SyncLock Me

            If isSucceeded Then
                _status.Succeeded += 1
            Else
                _status.Failed += 1
                _status.HasError = True
            End If

            If _status.TotalCount = _status.Succeeded + _status.Failed Then
                _status.Completed = True
            End If
        End SyncLock
    End Sub

#Region "EASendMail Event Handler"

    Private Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim status = CType(smtp.Tag, RecipientStatus)
        status.Status = "Connected"
    End Sub

    Private Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim status = CType(smtp.Tag, RecipientStatus)
        status.Status = If((sent <> total), String.Format("Sending {0}/{1} ... ", sent, total), "Disconnecting ...")
    End Sub

    Private Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim status = CType(smtp.Tag, RecipientStatus)
        status.Status = "Authorized"
    End Sub

    Private Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim status = CType(smtp.Tag, RecipientStatus)
        status.Status = "Securing ..."
    End Sub

#End Region

End Class

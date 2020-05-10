Imports System.Threading
Imports EASendMail

Class SendThreadPool
    Public Delegate Sub UpdateRecipientStatusDelegate(ByVal recipientIndx As Integer, ByVal status As String)
    Public UpdateRecipientStatus As UpdateRecipientStatusDelegate = Nothing

    Public Delegate Sub UpdateResultCounterDelegate(ByVal isSucceeded As Boolean)
    Public UpdateResultCounter As UpdateResultCounterDelegate = Nothing

    Private _trafficController As TrafficController = New TrafficController()

    Public Function SubmitMessage(ByVal server As SmtpServer,
                                  ByVal mail As SmtpMail,
                                  ByVal isTestRecipient As Boolean,
                                  ByVal recipientIndex As Integer) As Boolean
        If Not _trafficController.PrepareIncreaseConnection() Then
            _trafficController.Rollback()
            Return False
        End If

        If Not _trafficController.PrepareIncreaseMessage() Then
            _trafficController.Rollback()
            Return False
        End If

        Dim state As SendMailThreadState = New SendMailThreadState()
        state.server = server
        state.mail = mail
        state.recipientIndex = recipientIndex
        state.isTestRecipient = isTestRecipient

        Try
            Interlocked.Increment(_threadCounter)
            ThreadPool.QueueUserWorkItem(AddressOf Me.SendThreadProc, state)
            _trafficController.Commit()
            Return True
        Catch ep As Exception
            Interlocked.Decrement(_threadCounter)
            _trafficController.Rollback()

            If UpdateRecipientStatus IsNot Nothing Then
                UpdateRecipientStatus(recipientIndex, ep.Message)
            End If
            Return False
        End Try
    End Function

    Public Sub CancelAll()
        SyncLock Me
            _isCancelSending = True
        End SyncLock
    End Sub

    Public ReadOnly Property UnfinishedMessages As Integer
        Get
            Return _threadCounter
        End Get
    End Property

    Public Sub Reset(ByVal maximumConnections As Integer, ByVal maximumMessagesPerMinute As Integer)
        SyncLock Me
            _trafficController.MaximumConnections = maximumConnections
            _trafficController.MaximumMessagesPerMinute = maximumMessagesPerMinute
            _trafficController.Reset()
            _isCancelSending = False
        End SyncLock
    End Sub

    Private _isCancelSending As Boolean = False
    Private _threadCounter As Integer = 0

    Class SendMailThreadState
        Public mail As SmtpMail
        Public server As SmtpServer
        Public recipientIndex As Integer
        Public isTestRecipient As Boolean
    End Class

    Private Sub SendThreadProc(ByVal state As Object)
        Dim threadState As SendMailThreadState = CType(state, SendMailThreadState)
        Dim recipientIndex As Integer = threadState.recipientIndex

        Try
            Dim smtp As SmtpClient = New SmtpClient()
            smtp.Tag = recipientIndex

            ' Catching the following events is not necessary, 
            ' just make the application more user friendly.
            ' If you use the object in asp.net/windows service or non-gui application, 
            ' You need not to catch the following events.
            ' To learn more detail, please refer to the code in EASendMail EventHandler region
            AddHandler smtp.OnIdle, AddressOf OnIdle
            AddHandler smtp.OnAuthorized, AddressOf OnAuthorized
            AddHandler smtp.OnConnected, AddressOf OnConnected
            AddHandler smtp.OnSecuring, AddressOf OnSecuring
            AddHandler smtp.OnSendingDataStream, AddressOf OnSendingDataStream

            If UpdateRecipientStatus IsNot Nothing Then
                UpdateRecipientStatus(recipientIndex, "Connecting ...")
            End If

            If threadState.isTestRecipient Then
                smtp.TestRecipients(threadState.server, threadState.mail)
            Else
                smtp.SendMail(threadState.server, threadState.mail)
            End If

            If UpdateRecipientStatus IsNot Nothing Then
                UpdateRecipientStatus(recipientIndex, If(threadState.isTestRecipient, "Pass", "Succeeded"))
            End If

            If UpdateResultCounter IsNot Nothing Then
                UpdateResultCounter(True)
            End If

        Catch ep As Exception
            If UpdateRecipientStatus IsNot Nothing Then
                UpdateRecipientStatus(recipientIndex, ep.Message)
            End If

            If UpdateResultCounter IsNot Nothing Then
                UpdateResultCounter(False)
            End If
        Finally
            Interlocked.Decrement(_threadCounter)
            _trafficController.DecreaseConnection()
        End Try
    End Sub

    Private Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = _isCancelSending
    End Sub

    Private Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = smtp.Tag
        If UpdateRecipientStatus IsNot Nothing Then
            UpdateRecipientStatus(recipientIndex, "Connected")
        End If
        cancel = _isCancelSending
    End Sub

    Private Sub OnSendingDataStream(ByVal sender As Object, ByVal sent As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = smtp.Tag
        If UpdateRecipientStatus IsNot Nothing Then
            UpdateRecipientStatus(recipientIndex,
                                  If((sent <> total),
                                  String.Format("Sending {0}/{1} ... ", sent, total),
                                  "Disconnecting ...")
                                  )
        End If

        cancel = _isCancelSending
    End Sub

    Private Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = smtp.Tag
        If UpdateRecipientStatus IsNot Nothing Then
            UpdateRecipientStatus(recipientIndex, "Authorized")
        End If
        cancel = _isCancelSending
    End Sub

    Private Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        Dim smtp As SmtpClient = CType(sender, SmtpClient)
        Dim recipientIndex As Integer = smtp.Tag
        If UpdateRecipientStatus IsNot Nothing Then
            UpdateRecipientStatus(recipientIndex, "Securing ...")
        End If
        cancel = _isCancelSending
    End Sub
End Class

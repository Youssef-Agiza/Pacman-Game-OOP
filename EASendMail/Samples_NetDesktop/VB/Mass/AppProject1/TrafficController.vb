Imports System.Threading

Class TrafficController
    Private _currentThreadId As Integer = Thread.CurrentThread.ManagedThreadId
    Private _concurrentConnections As Integer = 0

    Public Sub DecreaseConnection()
        SyncLock Me
            _concurrentConnections -= 1
        End SyncLock
    End Sub

    Private _maximumConnections As Integer = 10

    Public Property MaximumConnections As Integer
        Get
            Return _maximumConnections
        End Get
        Set(ByVal value As Integer)
            _maximumConnections = value
        End Set
    End Property

    Private _maximumMessagesPerMinutes As Integer = 0 ' means unlimited

    Public Property MaximumMessagesPerMinute As Integer
        Get
            Return _maximumMessagesPerMinutes
        End Get
        Set(ByVal value As Integer)
            _maximumMessagesPerMinutes = value
        End Set
    End Property

    Private _isUncommittedConnection As Boolean = False
    Public Function PrepareIncreaseConnection() As Boolean
        If _currentThreadId <> Thread.CurrentThread.ManagedThreadId Then
            Throw New Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.")
        End If

        If _isUncommittedConnection Then
            Throw New Exception("Uncommitted connection is existed, please commit or rollback it at first.")
        End If

        If _maximumConnections = 0 Then
            Return True
        End If

        If _concurrentConnections >= _maximumConnections Then
            Return False
        End If

        _isUncommittedConnection = True
        Return True
    End Function

    Private _isUncommittedMessage As Boolean = False
    Public Function PrepareIncreaseMessage() As Boolean
        If _currentThreadId <> Thread.CurrentThread.ManagedThreadId Then
            Throw New Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.")
        End If

        If _isUncommittedMessage Then
            Throw New Exception("Uncommitted message is existed, please commit or rollback it at first.")
        End If

        If _maximumMessagesPerMinutes = 0 Then
            Return True
        End If

        If _messageDateTimes.Count >= _maximumMessagesPerMinutes Then
            _clearExpiredMessageCount()
        End If

        If _messageDateTimes.Count >= _maximumMessagesPerMinutes Then
            Return False
        End If

        _isUncommittedMessage = True
        Return True
    End Function

    Private Sub _clearExpiredMessageCount()
        Dim expiredTime As DateTime = DateTime.Now.AddMinutes(-60)

        While _messageDateTimes.Peek() > expiredTime
            _messageDateTimes.Dequeue()
        End While
    End Sub

    Public Sub Commit()
        If _currentThreadId <> Thread.CurrentThread.ManagedThreadId Then
            Throw New Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.")
        End If

        If _isUncommittedConnection Then
            _concurrentConnections += 1
            _isUncommittedConnection = False
        End If

        If _isUncommittedMessage Then
            _messageDateTimes.Enqueue(DateTime.Now)
            _isUncommittedMessage = False
        End If
    End Sub

    Public Sub Rollback()
        If _currentThreadId <> Thread.CurrentThread.ManagedThreadId Then
            Throw New Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.")
        End If

        _isUncommittedConnection = False
        _isUncommittedMessage = False
    End Sub

    Public Sub Reset()
        SyncLock Me
            _concurrentConnections = 0
            _messageDateTimes.Clear()
            _isUncommittedConnection = False
            _isUncommittedMessage = False
        End SyncLock
    End Sub

    Private _messageDateTimes As Queue(Of DateTime) = New Queue(Of DateTime)()
End Class

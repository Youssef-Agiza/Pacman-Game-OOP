using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AppProject1
{
    class TrafficController
    {
        int _currentThreadId = Thread.CurrentThread.ManagedThreadId;
        int _concurrentConnections = 0;
        public void DecreaseConnection()
        {
            lock (this)
            {
                _concurrentConnections--;
            }
        }

        int _maximumConnections = 10;
        public int MaximumConnections
        {
            get { return _maximumConnections; }
            set { _maximumConnections = value; }
        }

        int _maximumMessagesPerMinutes = 0; // means unlimited
        public int MaximumMessagesPerMinute
        {
            get { return _maximumMessagesPerMinutes; }
            set { _maximumMessagesPerMinutes = value; }
        }

        bool _isUncommittedConnection = false;
        public bool PrepareIncreaseConnection()
        {
            if (_currentThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                throw new Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.");
            }

            if (_isUncommittedConnection)
            {
                throw new Exception("Uncommitted connection is existed, please commit or rollback it at first.");
            }
            // unlimited
            if (_maximumConnections == 0)
            {
                return true;
            }

            if (_concurrentConnections >= _maximumConnections)
            {
                return false;
            }

            _isUncommittedConnection = true;
            return true;

        }
        bool _isUncommittedMessage = false;
        public bool PrepareIncreaseMessage()
        {
            if (_currentThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                throw new Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.");
            }

            if (_isUncommittedMessage)
            {
                throw new Exception("Uncommitted message is existed, please commit or rollback it at first.");
            }

            // unlimited and don't increase message counter
            if (_maximumMessagesPerMinutes == 0)
            {
                return true;
            }

            if (_messageDateTimes.Count >= _maximumMessagesPerMinutes)
            {
                // only clear expired message while out of limit.
                _clearExpiredMessageCount();
            }

            if (_messageDateTimes.Count >= _maximumMessagesPerMinutes)
            {
                return false;
            }

            _isUncommittedMessage = true;
            return true;
        }

        void _clearExpiredMessageCount()
        {
            DateTime expiredTime = DateTime.Now.AddMinutes(-60);
            while (_messageDateTimes.Peek() > expiredTime)
            {
                _messageDateTimes.Dequeue();
            }
        }

        public void Commit()
        {
            if (_currentThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                throw new Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.");
            }

            if (_isUncommittedConnection)
            {
                _concurrentConnections++;
                _isUncommittedConnection = false;
            }

            if (_isUncommittedMessage)
            {
                _messageDateTimes.Enqueue(DateTime.Now);
                _isUncommittedMessage = false;
            }
        }

        public void Rollback()
        {
            if (_currentThreadId != Thread.CurrentThread.ManagedThreadId)
            {
                throw new Exception("PrepareIncreaseConnection method is not thread safe, please invoke it in created thread.");
            }

            _isUncommittedConnection = false;
            _isUncommittedMessage = false;
        }

        public void Reset()
        {
            lock (this)
            {
                _concurrentConnections = 0;
                _messageDateTimes.Clear();
                _isUncommittedConnection = false;
                _isUncommittedMessage = false;
            }
        }

        Queue<DateTime> _messageDateTimes = new Queue<DateTime>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EASendMail;

namespace AppProject1
{
    class SendThreadPool
    {
        public delegate void UpdateRecipientStatusDelegate(int recipientIndx, string status);
        public UpdateRecipientStatusDelegate UpdateRecipientStatus = null;

        public delegate void UpdateResultCounterDelegate(bool isSucceeded);
        public UpdateResultCounterDelegate UpdateResultCounter = null;

        TrafficController _trafficController = new TrafficController();
        public bool SubmitMessage(SmtpServer server, SmtpMail mail, bool isTestRecipient, int recipientIndex)
        {
            if (!_trafficController.PrepareIncreaseConnection())
            {
                _trafficController.Rollback();
                return false;
            }

            if (!_trafficController.PrepareIncreaseMessage())
            {
                _trafficController.Rollback();
                return false;
            }

            SendMailThreadState state = new SendMailThreadState();
            state.server = server;
            state.mail = mail;
            state.recipientIndex = recipientIndex;
            state.isTestRecipient = isTestRecipient;

            try
            {
                Interlocked.Increment(ref _threadCounter);
                ThreadPool.QueueUserWorkItem(this.SendThreadProc, state);
                _trafficController.Commit();

                return true;
            }
            catch (Exception ep)
            {
                Interlocked.Decrement(ref _threadCounter);
                _trafficController.Rollback();

                if (UpdateRecipientStatus != null)
                {
                    UpdateRecipientStatus(recipientIndex, ep.Message);
                }

                return false;
            }
        }

        public void CancelAll()
        {
            lock (this)
            {
                _isCancelSending = true;
            }
        }

        public int UnfinishedMessages
        {
            get
            {
                return _threadCounter;
            }
        }

        public void Reset(int maximumConnections, int maximumMessagesPerMinute)
        {
            lock (this)
            {
                _trafficController.MaximumConnections = maximumConnections;
                _trafficController.MaximumMessagesPerMinute = maximumMessagesPerMinute;
                _trafficController.Reset();

                _isCancelSending = false;
            }
        }

        bool _isCancelSending = false;
        int _threadCounter = 0;

        class SendMailThreadState
        {
            public SmtpMail mail;
            public SmtpServer server;
            public int recipientIndex;
            public bool isTestRecipient;
        }

        void SendThreadProc(object state)
        {
            SendMailThreadState threadState = (SendMailThreadState)state;
            int recipientIndex = threadState.recipientIndex;

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Tag = recipientIndex;
                // Catching the following events is not necessary, 
                // just make the application more user friendly.
                // If you use the object in asp.net/windows service or non-gui application, 
                // You need not to catch the following events.
                // To learn more detail, please refer to the code in EASendMail EventHandler region
                smtp.OnIdle += new SmtpClient.OnIdleEventHandler(OnIdle);
                smtp.OnAuthorized += new SmtpClient.OnAuthorizedEventHandler(OnAuthorized);
                smtp.OnConnected += new SmtpClient.OnConnectedEventHandler(OnConnected);
                smtp.OnSecuring += new SmtpClient.OnSecuringEventHandler(OnSecuring);
                smtp.OnSendingDataStream += new SmtpClient.OnSendingDataStreamEventHandler(OnSendingDataStream);

                if (UpdateRecipientStatus != null)
                {
                    UpdateRecipientStatus(recipientIndex, "Connecting ...");
                }

                if (threadState.isTestRecipient)
                {
                    smtp.TestRecipients(threadState.server, threadState.mail);
                }
                else
                {
                    smtp.SendMail(threadState.server, threadState.mail);
                }

                if (UpdateRecipientStatus != null)
                {

                    UpdateRecipientStatus(recipientIndex,
                        threadState.isTestRecipient ?
                        "Pass":
                        "Succeeded");
                }

                if (UpdateResultCounter != null)
                {
                    UpdateResultCounter(true);
                }
            }
            catch (Exception ep)
            {
                if (UpdateRecipientStatus != null)
                {
                    UpdateRecipientStatus(recipientIndex, ep.Message);
                }

                if (UpdateResultCounter != null)
                {
                    UpdateResultCounter(false);
                }
            }
            finally
            {
                Interlocked.Decrement(ref _threadCounter);
                _trafficController.DecreaseConnection();
            }
        }

        #region	EASendMail EventHandler

        void OnIdle(object sender, ref bool cancel)
        {
            cancel = _isCancelSending;
        }

        void OnConnected(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            if (UpdateRecipientStatus != null)
            {
                UpdateRecipientStatus(recipientIndex, "Connected");
            }
            cancel = _isCancelSending;
        }

        void OnSendingDataStream(object sender, int sent, int total, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            if (UpdateRecipientStatus != null)
            {
                UpdateRecipientStatus(recipientIndex,
                    (sent != total) ?
                    string.Format("Sending {0}/{1} ... ", sent, total) :
                    "Disconnecting ..."
                    );
            }
            cancel = _isCancelSending;
        }

        void OnAuthorized(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            if (UpdateRecipientStatus != null)
            {
                UpdateRecipientStatus(recipientIndex, "Authorized");
            }
            cancel = _isCancelSending;
        }

        void OnSecuring(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            int recipientIndex = (int)smtp.Tag;
            if (UpdateRecipientStatus != null)
            {
                UpdateRecipientStatus(recipientIndex, "Securing ...");
            }
            cancel = _isCancelSending;
        }

        #endregion
    }
}

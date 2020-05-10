using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using EASendMail;
using WebProject1.Models;

namespace WebProject1.Services
{
    class MassSendThreadPool
    {
        TrafficController _trafficController = null;
        Dictionary<int, RecipientStatus> _activeRecipients = new Dictionary<int, RecipientStatus>();

        int _threadCounter = 0;
        MassSendMailStatus _status = new MassSendMailStatus();

        public MassSendMailStatus Status()
        {
            lock (this)
            {
                if (_status.TotalCount > 0)
                {
                    _status.Progress = ((_status.Succeeded + _status.Failed) * 100) / _status.TotalCount;

                    if (_status.Completed)
                    {
                        _status.Status = string.Format("Total {0}, Succeeded {1}, Failed {2}.",
                                    _status.TotalCount, _status.Succeeded, _status.Failed);
                    }
                    else
                    {
                        _status.Status = string.Format("Total {0}, Succeeded {1}, Failed {2}, Waiting {3} ...",
                                  _status.TotalCount,
                                  _status.Succeeded,
                                  _status.Failed,
                                  _status.TotalCount - _status.Failed - _status.Succeeded);
                    }
                }
                else
                {
                    _status.Status = "Please wait ...";
                }

                _status.ActiveRecipientStatus = _activeRecipients.Values.ToArray();
                return _status;
            }
        }

        AddressCollection _recipientList = null;
        public void Start(MailTask mailTaskTemplate)
        {
            if (_trafficController == null)
            {
                _trafficController = new TrafficController();
            }

            string recipients = mailTaskTemplate.RecipientTo;
            mailTaskTemplate.RecipientTo = string.Empty;

            _recipientList = new AddressCollection(recipients);

            _status.TotalCount = _recipientList.Count;
            _status.Succeeded = 0;
            _status.Failed = 0;

            for (int recipientIndex = 0; recipientIndex < _recipientList.Count; recipientIndex++)
            {
                var mailTask = mailTaskTemplate.Copy();
                mailTask.RecipientTo = (_recipientList[recipientIndex] as MailAddress).ToString();
                while (!_submitMessage(recipientIndex, mailTask))
                {
                    Thread.Sleep(10);
                }
            }

            while (_threadCounter > 0 || !_status.Completed)
            {
                Thread.Sleep(50);
            }
        }

        bool _submitMessage(int recipientIndex, MailTask mailTask)
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
            state.Server = mailTask.BuildServer();
            state.Mail = mailTask.BuildMail();

            state.Mail.TextBody = state.Mail.TextBody.Replace("[$sender]", state.Mail.From.ToString());
            state.Mail.TextBody = state.Mail.TextBody.Replace("[$rcpt]", state.Mail.To.ToString());
            state.Mail.TextBody = state.Mail.TextBody.Replace("[$subject]", state.Mail.Subject.ToString());

            // even you can add different attachment for different recipient here.
            state.RecipientIndex = recipientIndex;

            try
            {
                Interlocked.Increment(ref _threadCounter);
                ThreadPool.QueueUserWorkItem(this._sendThreadProc, state);
                _trafficController.Commit();

                return true;
            }
            catch
            {
                Interlocked.Decrement(ref _threadCounter);
                _trafficController.Rollback();
                return false;
            }
        }

        void _sendThreadProc(object state)
        {
            SendMailThreadState threadState = (SendMailThreadState)state;
            int recipientIndex = threadState.RecipientIndex;

            RecipientStatus status = new RecipientStatus();
            status.RecipientIndex = recipientIndex;
            status.Recipient = threadState.Mail.To.ToString();

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Tag = status;

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

                status.Status = "Connecting server ...";
                _updateRecipientStatus(status);

                smtp.SendMail(threadState.Server, threadState.Mail);

                status.Status = "Succeeded";
                status.Completed = true;

                _updateRecipientStatus(status);
                _updateResultCounter(true);
            }
            catch (Exception ep)
            {
                status.Status = ep.Message;
                status.HasError = true;
                status.Completed = true;

                _updateRecipientStatus(status);
                _updateResultCounter(false);
            }
            finally
            {
                Interlocked.Decrement(ref _threadCounter);
                _trafficController.DecreaseConnection();
            }
        }

        void _updateRecipientStatus(RecipientStatus status)
        {
            lock (this)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                if (!_activeRecipients.ContainsKey(threadId))
                {
                    _activeRecipients.Add(threadId, status);
                }

                if (status.Completed)
                {
                    _activeRecipients.Remove(threadId);
                }
            }
        }

        void _updateResultCounter(bool isSucceeded)
        {
            lock (this)
            {
                if (isSucceeded)
                {
                    _status.Succeeded++;
                }
                else
                {
                    _status.Failed++;
                    _status.HasError = true;
                }

                if (_status.TotalCount == _status.Succeeded + _status.Failed)
                {
                    _status.Completed = true;
                }
            }
        }

        #region	EASendMail EventHandler

        void OnIdle(object sender, ref bool cancel)
        {
        }

        void OnConnected(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            var status = (RecipientStatus)smtp.Tag;
            status.Status = "Connected";
        }

        void OnSendingDataStream(object sender, int sent, int total, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            var status = (RecipientStatus)smtp.Tag;
            status.Status = (sent != total) ?
                    string.Format("Sending {0}/{1} ... ", sent, total) :
                    "Disconnecting ...";
        }

        void OnAuthorized(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            var status = (RecipientStatus)smtp.Tag;
            status.Status = "Authorized";
        }

        void OnSecuring(object sender, ref bool cancel)
        {
            SmtpClient smtp = (SmtpClient)sender;
            var status = (RecipientStatus)smtp.Tag;
            status.Status = "Securing ...";
        }

        #endregion
    }

}
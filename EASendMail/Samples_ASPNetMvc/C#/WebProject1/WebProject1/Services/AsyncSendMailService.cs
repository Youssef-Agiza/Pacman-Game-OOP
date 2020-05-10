using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using WebProject1.Models;
using EASendMail;

namespace WebProject1.Services
{
    public class AsyncSendMailService
    {
        private static Dictionary<string, AsyncSendMailStatus> _taskStatus = null;
        private static object _lock = null;

        public static void Init()
        {
            _lock = new object();
            _taskStatus = new Dictionary<string, AsyncSendMailStatus>();
        }

        public static void CreateAsyncTask(MailTask mailTask)
        {
            lock (_lock)
            {
                AsyncSendMailStatus status = new AsyncSendMailStatus();
                status.TaskId = mailTask.TaskId;
                if (!_taskStatus.ContainsKey(mailTask.TaskId))
                {
                    _taskStatus.Add(mailTask.TaskId, status);

                    Task.Run(() =>
                    {
                        _sendEmail(status, mailTask);
                    });
                }
            }
        }

        public static void PutErrorStatus(string taskId, string error)
        {
            lock (_lock)
            {
                AsyncSendMailStatus status = new AsyncSendMailStatus();
                status.TaskId = taskId;
                status.HasError = true;
                status.Status = error;
                status.Completed = true;
                if (!_taskStatus.ContainsKey(taskId))
                {
                    _taskStatus.Add(taskId, status);
                }
            }
        }

        public static AsyncSendMailStatus QueryStatus(string taskId)
        {
            AsyncSendMailStatus task = null;
            lock (_lock)
            {
                if (_taskStatus.ContainsKey(taskId))
                {
                    task = _taskStatus[taskId];
                    if (task.Completed)
                    {
                        _taskStatus.Remove(taskId);
                    }
                }
            }
            return task;
        }

        private static void _sendEmail(AsyncSendMailStatus status, MailTask mailTask)
        {
            try
            {
                var server = mailTask.BuildServer();
                var mail = mailTask.BuildMail();
                var smtp = new SmtpClient();

                // Add event handlers to current SmtpClient instance.
                smtp.OnAuthorized += new SmtpClient.OnAuthorizedEventHandler(OnAuthorized);
                smtp.OnConnected += new SmtpClient.OnConnectedEventHandler(OnConnected);
                smtp.OnSecuring += new SmtpClient.OnSecuringEventHandler(OnSecuring);
                smtp.OnSendingDataStream +=
                    new SmtpClient.OnSendingDataStreamEventHandler(OnSendingDataStream);

                status.Status = "Connecting server ...";
                smtp.Tag = status;
                smtp.SendMail(server, mail);

                status.Status = "Completed";
                status.HasError = false;
                status.Progress = 100;
            }
            catch (Exception ep)
            {
                status.Status = ep.Message;
                status.HasError = true;
                status.Progress = 100;
            }

            status.Completed = true;
        }

        public static void OnSecuring(
            object sender,
            ref bool cancel
        )
        {
            var status = (AsyncSendMailStatus)((SmtpClient)sender).Tag;
            status.Status = "Securing ...";
        }

        public static void OnAuthorized(
            object sender,
            ref bool cancel
        )
        {
            var status = (AsyncSendMailStatus)((SmtpClient)sender).Tag;
            status.Status = "Authorized";
        }


        public static void OnConnected(
            object sender,
            ref bool cancel
        )
        {
            var status = (AsyncSendMailStatus)((SmtpClient)sender).Tag;
            status.Status = "Connected";
        }

        public static void OnSendingDataStream(
            object sender,
            int sent,
            int total,
            ref bool cancel
        )
        {
            var status = (AsyncSendMailStatus)((SmtpClient)sender).Tag;
            if (total == 0)
            {
                return;
            }

            int progress = (sent * 100) / total;
            status.Progress = progress;
            status.Status = string.Format("Sending {0}% ...", progress);
        }
    }
}
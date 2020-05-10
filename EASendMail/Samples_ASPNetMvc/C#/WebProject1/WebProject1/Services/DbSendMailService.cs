using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using WebProject1.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebProject1.Services
{
    public class DbSendMailService
    {
        private static Dictionary<int, DbSendThreadPool> _threadPools = null;
        private static object _lock = null;

        public static void Init()
        {
            _lock = new object();
            _threadPools = new Dictionary<int, DbSendThreadPool>();

            Thread taskThread = new Thread(new ThreadStart(_queryTaskThreadProc));
            taskThread.Start();
        }

        static void _queryTaskThreadProc()
        {
            using (var db = new WebProject1Context())
            {
                // change running tasks (unfinished) to terminated
                var unfinishedTasks = (from s in db.DbMailTasks
                                       select s)
                                 .Where(s => s.Status == DbMailTaskStatus.Running)
                                 .ToArray();

                for (int i = 0; i < unfinishedTasks.Length; i++)
                {
                    var task = unfinishedTasks[i];
                    task.Status = DbMailTaskStatus.Terminated;
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                }

                int miniSleep = 2;
                while (true)
                {
                    var tasks = (from s in db.DbMailTasks
                                 select s)
                                 .Where(s => s.Status == DbMailTaskStatus.Pending)
                                 .ToArray();

                    if (tasks.Length == 0)
                    {
                        Thread.Sleep(miniSleep);
                        if (miniSleep * miniSleep < 1024)
                        {
                            miniSleep *= miniSleep;
                        }
                        continue;
                    }

                    miniSleep = 2;

                    for (int i = 0; i < tasks.Length; i++)
                    {
                        _processTask(db, tasks[i]);
                    }
                }
            }
        }

        static void _updateResult(int taskId,
            bool isSucceeded,
            string recipientAddress,
            string status)
        {
            using (var db = new WebProject1Context())
            {
                DbRecipientResult result = new DbRecipientResult();

                result.CreationTime = DateTime.Now;
                result.IsSucceeded = isSucceeded;
                result.Recipient = recipientAddress;
                result.TaskId = taskId;
                result.Status = status;

                db.DbRecipientResults.Add(result);
                db.SaveChanges();

                var task = db.DbMailTasks.Find(taskId);
                var taskStatus = _queryTaskStatus(taskId);

                if (task != null && taskStatus != null)
                {
                    task.Failed = taskStatus.Failed;
                    task.Succeeded = taskStatus.Succeeded;
                    task.TotalCount = taskStatus.TotalCount;
                    if (taskStatus.Completed)
                    {
                        task.Status = DbMailTaskStatus.Completed;
                    }

                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        static void _processTask(WebProject1Context db, DbMailTask mailTask)
        {
            var threadPool = new DbSendThreadPool();
            threadPool.UpdateResult = _updateResult;
            _threadPools.Add(mailTask.TaskId, threadPool);

            var recipients = db.DbRecipients.ToArray();

            mailTask.TotalCount = recipients.Length;
            mailTask.Status = DbMailTaskStatus.Running;

            db.Entry(mailTask).State = EntityState.Modified;
            db.SaveChanges();

            Task.Run(() =>
            {
                threadPool.Start(mailTask, recipients);
            });
        }

        public static DbSendMailStatus _queryTaskStatus(int taskId)
        {
            DbSendMailStatus status = null;
            lock (_lock)
            {
                if (_threadPools.ContainsKey(taskId))
                {
                    var threadPool = _threadPools[taskId];
                    status = threadPool.Status();
                    if (status.Completed)
                    {
                        _threadPools.Remove(taskId);
                    }
                }
            }

            return status;
        }

    }
}
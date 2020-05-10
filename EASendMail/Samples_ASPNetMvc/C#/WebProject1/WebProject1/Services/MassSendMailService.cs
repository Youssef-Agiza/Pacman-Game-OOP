using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using WebProject1.Models;
using EASendMail;

namespace WebProject1.Services
{
 
    public class MassSendMailService
    {
        private static Dictionary<string, MassSendThreadPool> _threadPools = null;
        private static object _lock = null;

        public static void Init()
        {
            _lock = new object();
            _threadPools = new Dictionary<string, MassSendThreadPool>();
        }

        public static void CreateAsyncTask(MailTask mailTask)
        {
            lock (_lock)
            {
                var status = new MassSendMailStatus();
                status.TaskId = mailTask.TaskId;
                if (!_threadPools.ContainsKey(mailTask.TaskId))
                {
                    MassSendThreadPool threadPool = new MassSendThreadPool();
                    _threadPools.Add(mailTask.TaskId, threadPool);

                    Task.Run(() =>
                    {
                        threadPool.Start(mailTask);
                    });
                }
            }
        }

        public static MassSendMailStatus QueryStatus(string taskId)
        {
            MassSendMailStatus status = null;
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
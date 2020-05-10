using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject1.Models;

namespace WebProject1.Services
{
    public class TempMailTaskStore
    {
        private static Dictionary<string, MailTask> _tasks = null;
        private static object _lock = null;

        public static void Init()
        {
            _lock = new object();
            _tasks = new Dictionary<string, MailTask>();
        }

        public static void PutTask(MailTask mailTask)
        {
            lock (_lock)
            {
                if (!_tasks.ContainsKey(mailTask.TaskId))
                {
                    _tasks.Add(mailTask.TaskId, mailTask);
                }
            }
        }

        public static MailTask GetTask(string taskId)
        {
            MailTask task = null;
            if (string.IsNullOrEmpty(taskId))
            {
                return task;
            }

            lock (_lock)
            {
                if (_tasks.ContainsKey(taskId))
                {
                    task = _tasks[taskId];
                    _tasks.Remove(taskId);
                }
            }

            return task;
        }
    }
}
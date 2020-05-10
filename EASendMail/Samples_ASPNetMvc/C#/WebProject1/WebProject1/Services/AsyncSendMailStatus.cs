using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject1.Services
{
    public class AsyncSendMailStatus
    {
        public string TaskId { get; set; } = string.Empty;
        public bool Completed { get; set; } = false;
        public string Status { get; set; } =  string.Empty;
        public bool HasError { get; set; } = false;
        public int Progress { get; set; } = 0; // progress 0-100
    }
}
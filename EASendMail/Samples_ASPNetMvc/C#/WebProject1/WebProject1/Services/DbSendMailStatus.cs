using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject1.Services
{
    public class DbSendMailStatus
    {
        public int TaskId { get; set; }
        public bool Completed { get; set; } = false;
        public string Status { get; set; } = string.Empty;
        public bool HasError { get; set; } = false;
        public int Progress { get; set; } = 0; // progress 0-100

        public int TotalCount { get; set; } = 0;
        public int Succeeded { get; set; } = 0;
        public int Failed { get; set; } = 0;
        public RecipientStatus[] ActiveRecipientStatus { get; set; } = new RecipientStatus[0];
    }
}
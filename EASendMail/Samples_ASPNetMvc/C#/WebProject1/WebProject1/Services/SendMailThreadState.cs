using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EASendMail;

namespace WebProject1.Services
{
    class SendMailThreadState
    {
        public SmtpMail Mail { get; set; } = null;
        public SmtpServer Server { get; set; } = null;
        public int RecipientIndex { get; set; } = 0;
        public string TaskId { get; set; } = string.Empty;
        public int  DbTaskId { get; set; } = 0;

    }
}
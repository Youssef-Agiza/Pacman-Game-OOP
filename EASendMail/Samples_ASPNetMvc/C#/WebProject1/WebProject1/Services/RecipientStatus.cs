using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject1.Services
{
    public class RecipientStatus
    {
        public int RecipientIndex { get; set; } = 0;
        public string Recipient { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool Completed { get; set; } = false;
        public bool HasError { get; set; } = false;
      //  public int Progress { get; set; } = 0; // 0 - 100;
    }
}
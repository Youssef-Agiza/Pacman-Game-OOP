using System.ComponentModel.DataAnnotations;
using EASendMail;
using System;

namespace WebProject1.Models
{
    public class DbMailTask
    {
        [Key, ScaffoldColumn(false)]
        public int TaskId { get; set; }

        [MaxLength(250), Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Server"), MaxLength(128)]
        public string Server { get; set; }

        [Required]
        public int Port { get; set; } = 25;

        [Display(Name = "My server requires user authentication")]
        public bool IsAuthenticationRequired { get; set; }

        [RequiredIf("IsAuthenticationRequired", true, ErrorMessage = "User name is required"), MaxLength(128)]
        public string User { get; set; }

        [RequiredIf("IsAuthenticationRequired", true, ErrorMessage = "Password is required"), MaxLength(128)]
        public string Password { get; set; }

        [Display(Name = "TLS/SSL Connection")]
        public bool IsSslConnection { get; set; }

        public ServerProtocol Protocol { get; set; } = ServerProtocol.SMTP;
        public SmtpAuthType AuthType { get; set; } = SmtpAuthType.AuthAuto;

        [Required, Display(Name = "Sender")]
        public string Sender { get; set; }

        [Display(Name = "Reply-To")]
        public string ReplyTo { get; set; }

        [Required]
        public string Subject { get; set; }

        [Display(Name = "Text Body")]
        public string TextBody { get; set; }

        [Display(Name = "Html Body")]
        public string HtmlBody { get; set; }

        public DbMailTaskStatus Status { get; set; }

        [Display(Name = "Total Count")]
        public int TotalCount { get; set; } = 0;

        public int Succeeded { get; set; } = 0;
        public int Failed { get; set; } = 0;

        [Display(Name = "Creation Time")]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [Display(Name = "Update Time")]
        public DateTime LastWriteTime { get; set; } = DateTime.Now;

        public SmtpServer BuildServer()
        {
            var server = new SmtpServer(string.IsNullOrWhiteSpace(Server) ? string.Empty : Server.Trim());
            server.Protocol = Protocol;

            server.Port = Port;
            server.ConnectType = (IsSslConnection) ?
                SmtpConnectType.ConnectSSLAuto : SmtpConnectType.ConnectTryTLS;

            if (IsAuthenticationRequired)
            {
                server.AuthType = AuthType;
                server.User = (string.IsNullOrWhiteSpace(User) ? string.Empty : User.Trim());
                server.Password = (string.IsNullOrWhiteSpace(Password) ? string.Empty : Password.Trim());
            }

            return server;
        }

        public SmtpMail BuildMail()
        {
            var mail = new SmtpMail("TryIt");
            mail.From = (string.IsNullOrWhiteSpace(Sender) ? string.Empty : Sender.Trim());

            mail.Subject = string.IsNullOrEmpty(Subject) ? string.Empty : Subject;

            if (!string.IsNullOrEmpty(ReplyTo))
            {
                var replyTo = new MailAddress(ReplyTo);
                if (string.Compare(replyTo.Address, mail.From.Address, true) != 0)
                {
                    mail.ReplyTo = replyTo;
                }
            }

            if (!string.IsNullOrEmpty(TextBody))
            {
                mail.TextBody = TextBody;
            }

            if (!string.IsNullOrEmpty(HtmlBody))
            {
                mail.HtmlBody = HtmlBody;
            }

            return mail;
        }

        public DbMailTask Copy()
        {
            var mailTask = new DbMailTask();
            mailTask.AuthType = this.AuthType;
            mailTask.CreationTime = this.CreationTime;
            mailTask.HtmlBody = this.HtmlBody;
            mailTask.IsAuthenticationRequired = this.IsAuthenticationRequired;
            mailTask.IsSslConnection = this.IsSslConnection;
            mailTask.LastWriteTime = this.LastWriteTime;
            mailTask.Password = this.Password;
            mailTask.Port = this.Port;
            mailTask.Protocol = this.Protocol;
            mailTask.ReplyTo = this.ReplyTo;
            mailTask.Sender = this.Sender;
            mailTask.Server = this.Server;
            mailTask.Subject = this.Subject;
            mailTask.TaskId = this.TaskId;
            mailTask.TaskName = this.TaskName;
            mailTask.TextBody = this.TextBody;
            mailTask.User = this.User;

            return mailTask;
        }
    }
}
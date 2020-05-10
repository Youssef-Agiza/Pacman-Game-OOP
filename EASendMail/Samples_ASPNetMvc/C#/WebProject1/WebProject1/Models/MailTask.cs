using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EASendMail;

namespace WebProject1.Models
{
    public class MailTask
    {
        [Required, ScaffoldColumn(false)]
        public string TaskId { get; set; } = System.Guid.NewGuid().ToString();

        [Display(Name = "Task Name"), MaxLength(128)]
        public string TaskName { get; set; } = "Unnamed Task";

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

        public int OauthProvider { get; set; } = 0;

        [Required, Display(Name = "Sender")]
        public string Sender { get; set; }

        [Display(Name = "Reply-To")]
        public string ReplyTo { get; set; }

        [Required, Display(Name = "To")]
        public string RecipientTo { get; set; }

        [Display(Name = "Cc")]
        public string RecipientCc { get; set; }

        [Display(Name = "Bcc")]
        public string RecipientBcc { get; set; }

        [Required]
        public string Subject { get; set; }

        [Display(Name = "Text Body")]
        public string TextBody { get; set; }

        [Display(Name = "Html Body")]
        public string HtmlBody { get; set; }

        public bool IsAsyncTask { get; set; } = false;

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
            mail.To = (string.IsNullOrWhiteSpace(RecipientTo) ? string.Empty : RecipientTo.Trim());
            mail.Cc = (string.IsNullOrWhiteSpace(RecipientCc) ? string.Empty : RecipientCc.Trim());
            mail.Bcc = (string.IsNullOrWhiteSpace(RecipientBcc) ? string.Empty : RecipientBcc.Trim());

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

        public MailTask Copy()
        {
            var mailTask = new MailTask();
            mailTask.AuthType = this.AuthType;
            mailTask.HtmlBody = this.HtmlBody;
            mailTask.IsAsyncTask = this.IsAsyncTask;
            mailTask.IsAuthenticationRequired = this.IsAuthenticationRequired;
            mailTask.IsSslConnection = this.IsSslConnection;
            mailTask.Password = this.Password;
            mailTask.Port = this.Port;
            mailTask.Protocol = this.Protocol;
            mailTask.RecipientBcc = this.RecipientBcc;
            mailTask.RecipientCc = this.RecipientCc;
            mailTask.RecipientTo = this.RecipientTo;
            mailTask.ReplyTo = this.ReplyTo;
            mailTask.Sender = this.Sender;
            mailTask.Server = this.Server;
            mailTask.Subject = this.Subject;
            mailTask.TaskId = this.TaskId;
            mailTask.TextBody = this.TextBody;
            mailTask.User = this.User;

            return mailTask;
        }
    }
}
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MailKitSender
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> BccAddresses { get; set; }
        public List<EmailAddress> CcAddresses { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }

        public string TemplateEmail()
        {
            return $"";
        }
    }
}
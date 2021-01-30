using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailKitSender.Services
{
    public interface IEmailService
    {
        void SendMail(EmailMessage message);

        Task SendMailAsync(EmailMessage message);

        Task<List<EmailMessage>> ReadMail(int maxCount = 10);
    }
}
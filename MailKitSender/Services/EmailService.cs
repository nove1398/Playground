using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailKitSender.Services
{
    public class EmailService : IEmailService, IDisposable
    {
        private readonly EmailOptions _emailOptions;
        private CancellationTokenSource cancellationToken;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
            cancellationToken = new CancellationTokenSource();
        }

        public async Task<List<EmailMessage>> ReadMail(int maxCount = 10)
        {
            using var emailClient = new Pop3Client();
            await emailClient.ConnectAsync(_emailOptions.PopServer, _emailOptions.PopPort, true, cancellationToken.Token);

            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

            await emailClient.AuthenticateAsync(_emailOptions.PopUsername, _emailOptions.PopPassword, cancellationToken.Token);

            List<EmailMessage> emails = new List<EmailMessage>();
            for (int i = 0; i < emailClient.Count && i < maxCount; i++)
            {
                var message = emailClient.GetMessage(i, cancellationToken.Token);
                var emailMessage = new EmailMessage
                {
                    Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                    Subject = message.Subject
                };
                emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, Name = x.Name }));
                emailMessage.FromAddress = message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Email = x.Address, Name = x.Name }).FirstOrDefault();
                emails.Add(emailMessage);
            }

            return emails;
        }

        public void SendMail(EmailMessage message)
        {
            var emailMessage = CreateEmail(message);
            Send(emailMessage);
        }

        private void Send(MimeMessage emailMessage)
        {
            using var smtpClient = new SmtpClient();
            try
            {
                smtpClient.Connect(_emailOptions.SmtpServer, _emailOptions.SmtpPort, true, cancellationToken.Token);
                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                smtpClient.Authenticate(_emailOptions.SmtpUsername, _emailOptions.SmtpPassword, cancellationToken.Token);
                smtpClient.Send(emailMessage, cancellationToken.Token);
            }
            catch
            {
                throw;
            }
            finally
            {
                smtpClient.Disconnect(true);
                smtpClient.Dispose();
            }
        }

        private MimeMessage CreateEmail(EmailMessage message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(message.FromAddress.Name, message.FromAddress.Email));
            email.To.AddRange(message.ToAddresses.Select(m => new MailboxAddress(m.Name, m.Email)));
            email.Cc.AddRange(message.CcAddresses.Select(m => new MailboxAddress(m.Name, m.Email)));
            email.Bcc.AddRange(message.BccAddresses.Select(m => new MailboxAddress(m.Name, m.Email)));
            email.Subject = message.Subject;

            var body = new BodyBuilder
            {
                HtmlBody = message.Content
            };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var file in message.Attachments)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                    body.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }

            email.Body = body.ToMessageBody();
            return email;
        }

        public async Task SendMailAsync(EmailMessage message)
        {
            var emailMessage = CreateEmail(message);

            await SendAsync(emailMessage);
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using var smtpClient = new SmtpClient();
            try
            {
                await smtpClient.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.SmtpPort, true, cancellationToken.Token);
                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await smtpClient.AuthenticateAsync(_emailOptions.SmtpUsername, _emailOptions.SmtpPassword, cancellationToken.Token);
                await smtpClient.SendAsync(emailMessage, cancellationToken.Token);
            }
            catch
            {
                throw;
            }
            finally
            {
                await smtpClient.DisconnectAsync(true, cancellationToken.Token);
                smtpClient.Dispose();
            }
        }

        public void Dispose()
        {
            cancellationToken.Cancel();
        }
    }
}
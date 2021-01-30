using MailKitSender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailKitSender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;

        public EmailController(ILogger<EmailController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var attachments = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection(); // pass this into the new message
            var message = new EmailMessage
            {
                Attachments = attachments,
                Content = "Test email message",
                Subject = "Test subject",
                ToAddresses = new List<EmailAddress> { new EmailAddress { Email = "test@gmail.com", Name = "Test name" } },
                FromAddress = new EmailAddress { Email = "from address", Name = "FromUser@gmail.com" }
            };
            await _emailService.SendMailAsync(message);
            return Ok();
        }
    }
}
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var apiKey = _configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("martysiaxdxx@gmail.com", "Auth");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(message);

        }
    }
}

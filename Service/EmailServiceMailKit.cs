using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Service
{
    public class EmailServiceMailKit: IEmailSender
    {
        private readonly SmtpService _smtp;

        public EmailServiceMailKit(IOptions<SmtpService> smpt)
        {
            _smtp = smpt.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // create message
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse(_smtp.SenderEmail));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };
            
            // send email
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_smtp.HostAddress, _smtp.HostPort, true);
                await smtp.AuthenticateAsync (_smtp.HostUsername, _smtp.HostPassword);
                await smtp.SendAsync(emailToSend);
                smtp.Disconnect(true);
            }
        }
    }
}

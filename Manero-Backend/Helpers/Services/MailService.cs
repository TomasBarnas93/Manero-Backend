using MailKit.Net.Smtp;
using MailKit.Security;
using Manero_Backend.Models.Interfaces.Services;
using MimeKit.Text;
using MimeKit;

namespace Manero_Backend.Helpers.Services
{
    public class MailService : IEMailService
    {
        public async Task SendPasswordResetAsync(string code, long expire, string emailAddress)
        {
            string passwordResetUrl = "https://localhost:7164/v1/api/auth/resetcode/" + code;


            using (SmtpClient client = new SmtpClient())
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("manerogrupp3@outlook.com"));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = "Here is your password reset link.";
                email.Body = new TextPart(TextFormat.Html) { 
                    Text = "<h2>Here is your password reset link</h2><br><a href=\""+ passwordResetUrl +"\">Reset Password</a><br><p>This code expires on "+ DateTimeOffset.FromUnixTimeSeconds(expire) + ".</p><br><p>If you haven't requested this password reset then please contact Manero support !</p>"
                };
                /*
                email.Body = new TextPart(TextFormat.Plain) {
                    Text =
                    "Here is your password reset link: " + passwordResetUrl
                    +
                    "\r\n" +
                    "This code expires on " + DateTimeOffset.FromUnixTimeSeconds(expire)
                };*/

                // send email
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("manerogrupp3@outlook.com", "klmd21d-2MdE29");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

        }
    }
}

using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Epal.Infrastructure.EmailServices;

public class EmailSender (ILogger<EmailSender> _logger): IEmailSender
{
    private const string Host = "smtp.timeweb.ru";
    private const int SmtpPort = 465;
    private const string Username = "confirm@otakubase.ru";
    private const string Password = "K<<Sv/Py4L7u>!";

    public async Task SendEmailAsync(string recipientEmail, string Title, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Never Play Alone", Username));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = Title;

            // Add the email body
            // TODO HTML
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(Host, SmtpPort, SecureSocketOptions.SslOnConnect);
            await smtpClient.AuthenticateAsync(Username, Password);

            // Send the email
            await smtpClient.SendAsync(message);
            Console.WriteLine("Email sent successfully!");
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email sending failed: {ex.Message}");
            _logger.LogError(recipientEmail, ex, ex.Message);
        }
    }
}
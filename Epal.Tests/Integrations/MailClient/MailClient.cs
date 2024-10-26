using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Epal.Tests.Integrations.MailClient;

public class MailClient
{
    private const string Host = "smtp.timeweb.ru";
    private const int SmtpPort = 465;
    private const string Username = "confirm@otakubase.ru";
    private const string Password = "K<<Sv/Py4L7u>!";

    public static void ConnectToSmtp()
    {
        try
        {
            Console.WriteLine("SMTP connection trying..");
            using var smtpClient = new SmtpClient();
            smtpClient.Connect(Host, SmtpPort, SecureSocketOptions.SslOnConnect); // Use StartTls for security
            Console.WriteLine("SMTP connected, authenticating..");
            smtpClient.Authenticate(Username, Password);
            Console.WriteLine("SMTP connection successful!");
            smtpClient.Disconnect(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SMTP connection failed: {ex.Message}");
        }
    }

    public static void SendEmail(string recipient, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", Username));
            message.To.Add(new MailboxAddress("Recipient Name", recipient));
            message.Subject = subject;

            // Add the email body
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var smtpClient = new SmtpClient();
            smtpClient.Connect(Host, SmtpPort, SecureSocketOptions.SslOnConnect);
            smtpClient.Authenticate(Username, Password);
            
            // Send the email
            smtpClient.Send(message);
            Console.WriteLine("Email sent successfully!");
            smtpClient.Disconnect(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email sending failed: {ex.Message}");
        }
    }
}
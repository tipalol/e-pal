namespace Epal.Tests.Integrations.MailClient;

public class MainClientTests
{
    private readonly Infrastructure.EmailServices.EmailSender Client;

 
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ConnectAllProtocolsAndSendMail_OK()
    {
        // Send a test email
        Client.SendEmailAsync("danul383rus@gmail.com", "Confirmation Email", "This is a test email.");
    }
}
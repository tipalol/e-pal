namespace Epal.Tests.Integrations.MailClient;

public class MainClientTests
{
    private readonly MailClient Client;
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ConnectAllProtocolsAndSendMail_OK()
    {
        // Send a test email
        MailClient.SendEmail("danul383rus@gmail.com", "Confirmation Email", "This is a test email.");
    }
}
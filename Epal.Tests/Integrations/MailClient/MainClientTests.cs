using Epal.Application.Services;

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
        var pService = new PasswordService();
        var passwordHash = pService.HashPassword("123");
        
        Assert.AreEqual(passwordHash, "asfdsadfasfgasfdgsaredfgsdfgsdfgsdfgsdfg");
    }
}
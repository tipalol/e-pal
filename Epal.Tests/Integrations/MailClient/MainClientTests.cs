using Epal.Application.Services;
using Epal.Infrastructure.Clients;

namespace Epal.Tests.Integrations.MailClient;

public class MainClientTests
{
    private readonly EmailSender Client;

 
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Connect_OK()
    {
        PasswordService ps = new PasswordService();
        var c = ps.HashPassword("123");
        Assert.That("321", Is.EqualTo(c));

    }
}
using Microsoft.AspNetCore.SignalR;

namespace Epal.Api.Hubs;

public class EpalHub : Hub
{
    public async Task Ping()
    {
        await Clients.Caller.SendAsync("Pong", true);
        Console.WriteLine("PONG");
    }
}

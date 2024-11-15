using Epal.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Epal.Api.Hubs;

public class EpalHub(IUserService userService) : Hub
{
    public override async Task OnConnectedAsync()
    {
        
        // Логика установки статуса online
        var _profileId = Context.GetHttpContext().Items["UserIdentifier"] as string;
        if (!Guid.TryParse(_profileId, out Guid profileId))
            throw new Exception("exxxxxxxxxxxxxxx");
        // Обновите статус пользователя в базе данных на online
        await userService.SetUserOnline(profileId);
        await Clients.All.SendAsync("UserStatusChanged", profileId, "online");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var _profileId = Context.GetHttpContext().Items["UserIdentifier"] as string;
        if (!Guid.TryParse(_profileId, out Guid profileId))
            throw new Exception("exxxxxxxxxxxxxxx");
        await userService.SetUserOffline(profileId);

        await Clients.All.SendAsync("UserStatusChanged", profileId, "offline");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task Ping()
    {
        await Clients.Caller.SendAsync("Pong", true);
        Console.WriteLine("PONG");
    }
}

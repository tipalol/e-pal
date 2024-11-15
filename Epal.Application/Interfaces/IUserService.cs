using Epal.Application.Common.Models;

namespace Epal.Application.Interfaces;

public interface IUserService
{
    public AuthenticatedUser? AuthenticatedUser { get; }
    Task SetUserOnline(Guid profileId);
    Task SetUserOffline(Guid profileId);
    Task<DateTime?> GetLastActivityTime(Guid profileId);// на будущие задачи
}

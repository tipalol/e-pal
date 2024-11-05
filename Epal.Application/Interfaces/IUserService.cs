using Epal.Application.Common.Models;

namespace Epal.Application.Interfaces;

public interface IUserService
{
    public AuthenticatedUser AuthenticatedUser { get; }
}

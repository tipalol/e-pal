using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Epal.Application.Interfaces;

public interface IUserService
{
    public ClaimsPrincipal? CurrentUser { get;}
    public IEnumerable<Claim>? GetUserClaims();
}
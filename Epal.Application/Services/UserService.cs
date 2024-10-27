using System.Security.Claims;
using Epal.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Epal.Application.Services;

public class UserService(IHttpContextAccessor contextAccessor) : IUserService
{
    
    public ClaimsPrincipal? CurrentUser => contextAccessor.HttpContext?.User;
    
    public IEnumerable<Claim>? GetUserClaims()
        => CurrentUser?.Claims.AsEnumerable();
    
}
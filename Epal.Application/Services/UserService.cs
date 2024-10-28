using System.Security.Claims;
using Epal.Application.Common.Models;
using Epal.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Epal.Application.Services;

public class UserService(IHttpContextAccessor contextAccessor) : IUserService
{
    public AuthenticatedUser AuthenticatedUser => _authenticatedUser ??= Authenticate();
    
    private AuthenticatedUser? _authenticatedUser;

    private AuthenticatedUser Authenticate()
    {
        var claims = CurrentUser?.Claims.ToArray();

        if (claims == null)
            throw new Exception("Ошибка аутентификации пользователя");

        var id = claims.SingleOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
        var username = claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var email = claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            throw new Exception("Ошибка аутентификации пользователя");
        
        var authenticatedUser = new AuthenticatedUser(Guid.Parse(id), username, email);
        
        return authenticatedUser;
    }
    
    private ClaimsPrincipal? CurrentUser => contextAccessor.HttpContext?.User;
}
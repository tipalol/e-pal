using System.Security.Claims;
using Epal.Application.Common.Models;
using Epal.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Epal.Application.Services;

public class UserService(IHttpContextAccessor contextAccessor, IEpalDbContext context) : IUserService
{
    public AuthenticatedUser? AuthenticatedUser => _authenticatedUser ??= Authenticate();
    
    private AuthenticatedUser? _authenticatedUser;

    private AuthenticatedUser Authenticate()
    {
        var claims = CurrentUser?.Claims.ToArray();

        if (claims == null)
            return null;

        var id = claims.SingleOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
        var username = claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var email = claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            return null;
        
        var authenticatedUser = new AuthenticatedUser(Guid.Parse(id), username, email);
        _authenticatedUser = authenticatedUser;
        return authenticatedUser;
    }
    
    private ClaimsPrincipal? CurrentUser => contextAccessor.HttpContext?.User;
    public async Task SetUserOnline(Guid profileId)
    {
        var profile = await context.Profiles.FindAsync(profileId);
        if (profile != null)
        {
            profile.IsOnline = true;
            profile.LastActivityTime = DateTime.UtcNow;
            CancellationToken cancellationToken = default;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SetUserOffline(Guid profileId)
    {
        var profile = await context.Profiles.FindAsync(profileId);
        if (profile != null)
        {
            profile.IsOnline = false;
            profile.LastActivityTime = DateTime.UtcNow;
            CancellationToken cancellationToken = default;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<DateTime?> GetLastActivityTime(Guid profileId)
    {
        throw new NotImplementedException();
    }
}
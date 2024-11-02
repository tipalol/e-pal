using Epal.Application.Features.Authorize.Token;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Authorize.Refresh;

public record RefreshTokenRequest : IRequest<string>;

internal sealed class Handler(IEpalDbContext context, IUserService userService, ISender sender) : IRequestHandler<RefreshTokenRequest, string>
{
    public async Task<string> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var userId = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (profile is null)
            throw new ArgumentException("There is no such a user");

        var token = await sender.Send(new TokenRequest(profile.Id, profile.Email, profile.Username), cancellationToken);

        return token;
    }
}

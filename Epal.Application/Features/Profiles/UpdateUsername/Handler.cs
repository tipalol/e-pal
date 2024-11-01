using Epal.Application.Common;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Profiles.UpdateUsername;

public record UpdateUsernameRequest(string Username) : IRequest<Result>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<UpdateUsernameRequest, Result>
{
    public async Task<Result> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;

        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile is null)
            return Result.Fail("Profile not found");

        profile.Username = request.Username;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

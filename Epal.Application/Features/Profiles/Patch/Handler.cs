using Epal.Application.Common;
using Epal.Application.Features.Profiles.Get.MyProfile;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Patch;

using MediatR;
using Microsoft.EntityFrameworkCore;


public record PatchMyProfileType() : IRequest<Result>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<PatchMyProfileType, Result>
{
    public async Task<Result> Handle(PatchMyProfileType request, CancellationToken cancellationToken)
    {
        var id = userService.AuthenticatedUser?.Id;
        var profile = await context.Profiles
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (profile is null)
            return Result.Fail("Profile not found");
        profile.ProfileType = ProfileType.Epal;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}
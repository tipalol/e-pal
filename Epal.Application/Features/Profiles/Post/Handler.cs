using Epal.Application.Common;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Profiles.Post;

public record UpdateUsernameRequest(Guid UserId, ProfileModel ProfileModel) : IRequest<Result<ProfileResponse>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<UpdateUsernameRequest, Result<ProfileResponse>>
{
    public async Task<Result<ProfileResponse>> Handle(UpdateUsernameRequest updateProfileRequest, CancellationToken cancellationToken)
    {
        var profile = await context.Users.SingleOrDefaultAsync(x => x.Id == updateProfileRequest.UserId, cancellationToken);
        if (profile is null) return Result<ProfileResponse>.Fail("Profile not found");
        bool newUsernameExisted =
            await context.Users.AnyAsync(x => x.Username == updateProfileRequest.ProfileModel.Username, cancellationToken);
        if (newUsernameExisted)
            return Result<ProfileResponse>.Fail("Username is not available");
        profile.Username = updateProfileRequest.ProfileModel.Username;
        await context.SaveChangesAsync(cancellationToken);
        return Result<ProfileResponse>.Ok(ProfileResponse.FromProfile(profile));
    }
}
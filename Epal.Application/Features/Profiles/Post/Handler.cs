using Epal.Application.Common;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Profiles.Post;

public record UpdateProfileRequest(ProfileModel ProfileModel) : IRequest<Result<ProfileResponse>>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<UpdateProfileRequest, Result<ProfileResponse>>
{
    public async Task<Result<ProfileResponse>> Handle(UpdateProfileRequest updateProfileRequest, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Users.SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);
        
        if (profile is null) 
            return Result<ProfileResponse>.Fail("Profile not found");
        
        var newUsernameExisted = await context.Users
            .AnyAsync(x => x.Username == updateProfileRequest.ProfileModel.Username, cancellationToken);
        
        if (newUsernameExisted)
            return Result<ProfileResponse>.Fail("Username is not available");
        
        profile.Username = updateProfileRequest.ProfileModel.Username;
        
        await context.SaveChangesAsync(cancellationToken);
        
        return Result<ProfileResponse>.Ok(ProfileResponse.FromProfile(profile));
    }
}
using Epal.Application.Common;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
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
        
        // TODO
        if (newUsernameExisted && userService.AuthenticatedUser.Username != updateProfileRequest.ProfileModel.Username)
            return Result<ProfileResponse>.Fail("Username is not available");
        
        profile.Username = updateProfileRequest.ProfileModel.Username;
        // TODO
        if (string.IsNullOrEmpty(profile.Avatar) || profile.Avatar == StaticValues.DefaultAvatarByGender(profile.Gender)) //Если аватар не меняли
        {
            profile.Avatar = StaticValues.DefaultAvatarByGender(updateProfileRequest.ProfileModel.Gender ?? Gender.Unselected);
        }
        profile.Gender = updateProfileRequest.ProfileModel.Gender ?? Gender.Unselected;
        // TODO
        profile.Languages = updateProfileRequest.ProfileModel.Languages;
        await context.SaveChangesAsync(cancellationToken);
        
        return Result<ProfileResponse>.Ok(ProfileResponse.FromProfile(profile));
    }
}
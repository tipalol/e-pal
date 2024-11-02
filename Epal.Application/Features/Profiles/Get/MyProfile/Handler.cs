using Epal.Application.Common;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Profiles.Get.MyProfile;

public record MyProfileRequest() : IRequest<Result<ProfileResponse>>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<MyProfileRequest, Result<ProfileResponse>>
{
    public async Task<Result<ProfileResponse>> Handle(MyProfileRequest request, CancellationToken cancellationToken)
    {
        var id = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .Where(x => x.Id == id)
            .Select(x => ProfileResponse.FromProfile(x))
            .SingleOrDefaultAsync(cancellationToken);
        
        return profile is null ? 
            Result<ProfileResponse>.Fail("Profile not found") : 
            Result<ProfileResponse>.Ok(profile);
    }
}
﻿using Epal.Application.Common;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Profiles.Get;
public record ProfileRequest(string Username) : IRequest<Result<ProfileResponse>>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<ProfileRequest, Result<ProfileResponse>>
{
    public async Task<Result<ProfileResponse>> Handle(ProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = userService.AuthenticatedUser?.Id;
        
        var profile = await context.Profiles
            .Where(x => x.Username == request.Username)
            .Select(x => ProfileResponse.FromProfile(x, userId))
            .SingleOrDefaultAsync(cancellationToken);
        
        return profile is null ? 
            Result<ProfileResponse>.Fail("Profile not found") : 
            Result<ProfileResponse>.Ok(profile);
    }
}
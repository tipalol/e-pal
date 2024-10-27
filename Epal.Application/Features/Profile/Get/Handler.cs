using Epal.Application.Features.Profile.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Epal.Application.Features.Profile.Models.ProfileResponse;

namespace Epal.Application.Features.Profile.Get;
public record ProfileRequest(string ProfileId) : IRequest<ProfileResponse>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<ProfileRequest, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(ProfileRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Users.SingleOrDefaultAsync(x=> x.ProfileId== request.ProfileId, cancellationToken);
        if (profile is null)
            throw new ArgumentException("User not found");
        var profileResponse = Create(profile).WithResponseStatus(ResponseStatus.OK).Build();
        return profileResponse;
    }
}


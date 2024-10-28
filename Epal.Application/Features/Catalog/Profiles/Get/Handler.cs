using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Features.Catalog.ServiceTypes.Get;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Profiles.Get;

public record NewProfilesRequest(int Take = 6): IRequest<IEnumerable<ProfileView>>;
public class Handler(IEpalDbContext context) : IRequestHandler<NewProfilesRequest, IEnumerable<ProfileView>>
{
    public async Task<IEnumerable<ProfileView>> Handle(NewProfilesRequest request, CancellationToken cancellationToken)
    {
        var profiles = await context.Users
            .Where(x => x.ProfileType == ProfileType.Epal)
            .OrderByDescending(x => x.EpalStatusAcquiring)
            .Take(request.Take)
            .Where(x => x.EpalStatusAcquiring != null)
            .Select(x=> new ProfileView(x.Id, x.Username, x.Avatar))
            .ToListAsync(cancellationToken);
        return profiles;
    }
}

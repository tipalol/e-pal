using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Features.Catalog.ServiceTypes.Get;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Profiles.Get;

public record GetProfilesByServiceTypsRequest(Guid Id, int Take = 6): IRequest<IEnumerable<ProfileView>>;
public class Handler(IEpalDbContext context) : IRequestHandler<GetProfilesByServiceTypsRequest, IEnumerable<ProfileView>>
{
    public async Task<IEnumerable<ProfileView>> Handle(GetProfilesByServiceTypsRequest request, CancellationToken cancellationToken)
    {
        var serviceProfileIds  = await context.Services
            .Where(x => x.ServiceTypeId == request.Id)
            .Select(x => x.ProfileId)
            .ToListAsync(cancellationToken);
        var profiles = await context.Users
            .Where(x => x.ProfileType == ProfileType.Epal)
            .Where(x=> serviceProfileIds.Contains(x.Id))
            .OrderByDescending(x => x.EpalStatusAcquiring)
            .Take(request.Take)
            .Where(x => x.EpalStatusAcquiring != null)
            .Select(x=> new ProfileView(x.Id, x.Username, x.Avatar))
            .ToListAsync(cancellationToken);
        return profiles;
    }
}

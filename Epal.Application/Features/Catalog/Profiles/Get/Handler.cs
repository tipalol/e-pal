using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Features.Catalog.ServiceTypes.Get;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Profiles.Get;

public record EpalsByServiceTypeCatalogRequest(Guid ServiceTypeId, int Take = 6): IRequest<IEnumerable<ProfileView>>;
public class Handler(IEpalDbContext context) : IRequestHandler<EpalsByServiceTypeCatalogRequest, IEnumerable<ProfileView>>
{
    public async Task<IEnumerable<ProfileView>> Handle(EpalsByServiceTypeCatalogRequest request, CancellationToken cancellationToken)
    {
        var profiles = await context.Users
            .Where(x => x.ProfileType == ProfileType.Epal)
            .Where(x => x.Services.Any(x => x.ServiceTypeId == request.ServiceTypeId))
            .OrderByDescending(x => x.EpalStatusAcquiring)
            .Take(request.Take)
            .Select(x => new ProfileView(x.Id, x.Username!, x.Avatar))
            .ToArrayAsync(cancellationToken);
            
        return profiles;
    }
}

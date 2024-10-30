using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Features.Catalog.ServiceTypes.Get;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Profiles.Get;

public record EpalsByServiceTypeCatalogRequest(Guid? ServiceTypeId, SortingType Sort, int Take = 20): IRequest<IEnumerable<ProfileView>>;
public class Handler(IEpalDbContext context) : IRequestHandler<EpalsByServiceTypeCatalogRequest, IEnumerable<ProfileView>>
{
    public async Task<IEnumerable<ProfileView>> Handle(EpalsByServiceTypeCatalogRequest request, CancellationToken cancellationToken)
    {
        var query = context.Users
            .Where(x => x.ProfileType == ProfileType.Epal);

        if (request.ServiceTypeId.HasValue)
            query = query.Where(x => x.Services.Any(x => x.ServiceTypeId == request.ServiceTypeId));

        query = ApplySorting(query, request.Sort);
        
        var profiles = await query
            .Take(request.Take)
            .Select(x => new ProfileView(x.Id, x.Username!, x.Bio, x.Avatar))
            .ToArrayAsync(cancellationToken);
        
        return profiles;
    }

    private static IQueryable<Profile> ApplySorting(IQueryable<Profile> query, SortingType? sortingType)
    {
        return sortingType switch
        {
            null or SortingType.None => query,
            SortingType.Newbies => query.OrderByDescending(x => x.EpalStatusAcquiring),
            _ => query
        };
    }
}

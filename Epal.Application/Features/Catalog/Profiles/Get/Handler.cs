using Epal.Application.Common;
using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Profiles.Get;

public record EpalsByServiceTypeCatalogRequest(Guid? ServiceTypeId, SortingType Sort, int Take = 20, int Skip = 0): IRequest<PaginatedResult<ProfileView>>;
public class Handler(IEpalDbContext context) : IRequestHandler<EpalsByServiceTypeCatalogRequest, PaginatedResult<ProfileView>>
{
    public async Task<PaginatedResult<ProfileView>> Handle(EpalsByServiceTypeCatalogRequest request, CancellationToken cancellationToken)
    {
        var query = context.Users
            .Where(x => x.ProfileType == ProfileType.Epal);

        if (request.ServiceTypeId.HasValue)
            query = query.Where(x => x.Services.Any(x => x.CategoryId == request.ServiceTypeId));

        query = ApplySorting(query, request.Sort);

        var total = await query.CountAsync(cancellationToken: cancellationToken);
        
        var profiles = await query
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(x => new ProfileView(x.Id, x.Username!, x.Bio, x.Avatar))
            .ToArrayAsync(cancellationToken);
        
        return PaginatedResult<ProfileView>.Create(profiles, request.Take, request.Skip, total);
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

using Epal.Application.Common;
using Epal.Application.Features.Catalog.Profiles.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Profiles.Get;

public record EpalsByCategoryCatalogRequest(Guid? CategoryId, SortingType Sort, int Take = 20, int Skip = 0): IRequest<PaginatedResult<ProfileView>>;
public class Handler(IEpalDbContext context) : IRequestHandler<EpalsByCategoryCatalogRequest, PaginatedResult<ProfileView>>
{
    public async Task<PaginatedResult<ProfileView>> Handle(EpalsByCategoryCatalogRequest request, CancellationToken cancellationToken)
    {
        var query = context.Profiles
            .Where(x => x.ProfileType == ProfileType.Epal);

        if (request.CategoryId.HasValue)
            query = query.Where(x => x.Services.Any(x => x.CategoryId == request.CategoryId));

        query = ApplySorting(query, request.Sort);

        var total = await query.CountAsync(cancellationToken: cancellationToken);

        var profiles = await query
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(x => new ProfileView(x.Id, x.Username!, x.Bio, x.Avatar, x.IsOnline))
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

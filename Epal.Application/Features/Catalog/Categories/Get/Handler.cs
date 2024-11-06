using Epal.Application.Common;
using Epal.Application.Features.Catalog.Categories.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Categories.Get;

public record CategoriesCatalogRequest(int Take, int Skip) : IRequest<PaginatedResult<CategoryCatalogView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<CategoriesCatalogRequest, PaginatedResult<CategoryCatalogView>>
{
    public async Task<PaginatedResult<CategoryCatalogView>> Handle(CategoriesCatalogRequest request, CancellationToken cancellationToken)
    {
        var query = context.Categories;

        var total = await query.CountAsync(cancellationToken); 
        IEnumerable<CategoryCatalogView> serviceTypes;

        if (request.Take == -1)
        {
            serviceTypes = await query
                .OrderByDescending(x => x.Services.Count())
                .Skip(request.Skip)
                .Select(x => new CategoryCatalogView(x.Id, x.Name, x.Avatar))
                .ToArrayAsync(cancellationToken);
        }
        else
        {
            serviceTypes = await query
                .OrderByDescending(x => x.Services.Count())
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(x => new CategoryCatalogView(x.Id, x.Name, x.Avatar))
                .ToArrayAsync(cancellationToken);
        }

        return PaginatedResult<CategoryCatalogView>.Create(serviceTypes, request.Take, request.Skip, total);
    }
}

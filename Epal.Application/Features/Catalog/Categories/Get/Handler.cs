using Epal.Application.Common;
using Epal.Application.Features.Catalog.Categories.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.Categories.Get;

public record CategoriesCatalogRequest(int Take = 6, int Skip = 0) : IRequest<PaginatedResult<CategoryCatalogView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<CategoriesCatalogRequest, PaginatedResult<CategoryCatalogView>>
{
    public async Task<PaginatedResult<CategoryCatalogView>> Handle(CategoriesCatalogRequest request, CancellationToken cancellationToken)
    {
        var query = context.Categories;

        var total = await query.CountAsync(cancellationToken);

        var serviceTypes = await query
            .OrderByDescending(x => x.Services.Count())
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(x => new CategoryCatalogView(x.Id, x.Name, x.Avatar))
            .ToArrayAsync(cancellationToken);

        return PaginatedResult<CategoryCatalogView>.Create(serviceTypes, request.Take, request.Skip, total);
    }
}

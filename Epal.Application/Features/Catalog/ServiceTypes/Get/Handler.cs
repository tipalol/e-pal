using Epal.Application.Common;
using Epal.Application.Features.Catalog.ServiceTypes.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.ServiceTypes.Get;

public record ServiceTypesCatalogRequest(int Take = 6, int Skip = 0) : IRequest<PaginatedResult<ServiceTypeCatalogView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<ServiceTypesCatalogRequest, PaginatedResult<ServiceTypeCatalogView>>
{
    public async Task<PaginatedResult<ServiceTypeCatalogView>> Handle(ServiceTypesCatalogRequest request, CancellationToken cancellationToken)
    {
        var query = context.ServiceTypes;

        var total = await query.CountAsync(cancellationToken);
        
        var serviceTypes = await query
            .OrderByDescending(x => x.Services.Count())
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(x => new ServiceTypeCatalogView(x.Id, x.Name, x.Avatar))
            .ToArrayAsync(cancellationToken);
        
        return PaginatedResult<ServiceTypeCatalogView>.Create(serviceTypes, request.Take, request.Skip, total);
    }
}

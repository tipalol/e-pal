using Epal.Application.Features.Catalog.ServiceTypes.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.ServiceTypes.Get;

public record ServiceTypesCatalogRequest(int Take = 5) : IRequest<IEnumerable<ServiceTypeCatalogView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<ServiceTypesCatalogRequest, IEnumerable<ServiceTypeCatalogView>>
{
    public async Task<IEnumerable<ServiceTypeCatalogView>> Handle(ServiceTypesCatalogRequest request, CancellationToken cancellationToken)
    {
        return await context.ServiceTypes.
            OrderByDescending(x => x.Services.Count())
            .Take(request.Take)
            .Select(x => new ServiceTypeCatalogView(x.Id, x.Name, x.Avatar))
            .ToListAsync(cancellationToken);
    }
}

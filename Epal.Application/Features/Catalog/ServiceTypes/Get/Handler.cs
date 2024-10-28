using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Catalog.ServiceTypes.Get;

public record ServiceTypesCatalogRequest(int TakeCount = 5) : IRequest<IEnumerable<ServiceType>>;

public class Handler(IEpalDbContext context) : IRequestHandler<ServiceTypesCatalogRequest, IEnumerable<ServiceType>>
{
    public async Task<IEnumerable<ServiceType>> Handle(ServiceTypesCatalogRequest request, CancellationToken cancellationToken)
    {
        return await context.ServiceTypes.
            OrderByDescending(x => x.Services.Count()).
            Take(request.TakeCount).
            ToListAsync(cancellationToken);
    }
}

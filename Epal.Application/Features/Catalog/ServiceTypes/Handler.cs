using MediatR;

namespace Epal.Application.Features.Catalog.ServiceTypes;

// TODO
public record ServiceTypesCatalogRequest() : IRequest;

public class Handler : IRequestHandler<ServiceTypesCatalogRequest>
{
    public Task Handle(ServiceTypesCatalogRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using Epal.Application.Common;
using Epal.Application.Features.Services.GetTypes.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.ById;

public record GetServiceTypeById(Guid ServiceTypeId) : IRequest<Result<ServiceTypeListView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<GetServiceTypeById, Result<ServiceTypeListView>>
{
    public async Task<Result<ServiceTypeListView>> Handle(GetServiceTypeById request, CancellationToken cancellationToken)
    {
        var serviceType = await context.ServiceTypes
            .SingleOrDefaultAsync(x => x.Id == request.ServiceTypeId, cancellationToken);

        if (serviceType == null)
            return Result<ServiceTypeListView>.Fail("Service type not found");
        
        return Result<ServiceTypeListView>.Ok(new ServiceTypeListView(serviceType.Id, serviceType.Name, serviceType.Avatar));
    }
}
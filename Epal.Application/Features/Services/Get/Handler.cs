using Epal.Application.Common;
using Epal.Application.Features.Services.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.Get;

public record GetServicesRequest(Guid ProfileId, Guid ServiceTypeId) : IRequest<Result<IEnumerable<ServiceListView>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetServicesRequest, Result<IEnumerable<ServiceListView>>>
{
    public async Task<Result<IEnumerable<ServiceListView>>> Handle(GetServicesRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Users
            .Include(x => x.Services)
            .SingleOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);

        if (profile == null)
            return Result<IEnumerable<ServiceListView>>.Fail("User not found");

        var serviceType = await context.ServiceTypes
            .SingleOrDefaultAsync(x => x.Id == request.ServiceTypeId, cancellationToken);

        if (serviceType == null)
            return Result<IEnumerable<ServiceListView>>.Fail("Service type not found");

        var services = profile.Services
            .Where(x => x.ServiceTypeId == serviceType.Id)
            .Select(x => new ServiceListView(x.Id, x.Name, x.Avatar, x.Price, serviceType.Id))
            .ToArray();

        return Result<IEnumerable<ServiceListView>>.Ok(services);
    }
}

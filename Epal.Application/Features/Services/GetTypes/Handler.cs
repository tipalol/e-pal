using Epal.Application.Common;
using Epal.Application.Features.Services.GetTypes.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.GetTypes;

public record GetServiceTypesRequest(Guid ProfileId) : IRequest<Result<IEnumerable<ServiceTypeListView>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetServiceTypesRequest, Result<IEnumerable<ServiceTypeListView>>>
{
    public async Task<Result<IEnumerable<ServiceTypeListView>>> Handle(GetServiceTypesRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Users
            .Include(x => x.Services)
            .ThenInclude(x => x.ServiceType)
            .SingleOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);

        if (profile == null)
            return Result<IEnumerable<ServiceTypeListView>>.Fail("User not found");

        var serviceTypes = profile.Services
            .Select(x => new ServiceTypeListView(x.ServiceType.Id, x.ServiceType.Name, x.ServiceType.Avatar))
            .DistinctBy(x => x.Id)
            .ToArray();

        return Result<IEnumerable<ServiceTypeListView>>.Ok(serviceTypes);
    }
}

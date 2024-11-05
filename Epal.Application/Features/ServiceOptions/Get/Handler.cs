using Epal.Application.Common;
using Epal.Application.Features.ServiceOptions.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.ServiceOptions.Get;

public record GetServiceOptionsRequest(Guid ProfileId, Guid ServiceId) : IRequest<Result<IEnumerable<ServiceOptionListView>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetServiceOptionsRequest, Result<IEnumerable<ServiceOptionListView>>>
{
    public async Task<Result<IEnumerable<ServiceOptionListView>>> Handle(GetServiceOptionsRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);

        if (profile == null)
            return Result<IEnumerable<ServiceOptionListView>>.Fail("Profile not found");

        var service = await context.Services
            .Include(x => x.ServiceOptions)
            .SingleOrDefaultAsync(x => x.Id == request.ServiceId, cancellationToken);

        if (service == null)
            return Result<IEnumerable<ServiceOptionListView>>.Fail("Category not found");

        var services = service.ServiceOptions
            .Select(x => new ServiceOptionListView(x.Id, x.Name, x.Description, x.Price, service.Id))
            .ToArray();

        return Result<IEnumerable<ServiceOptionListView>>.Ok(services);
    }
}

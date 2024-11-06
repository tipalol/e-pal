using Epal.Application.Common;
using Epal.Application.Features.Services.Get.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.Get;

public record GetServicesRequest(Guid ProfileId) : IRequest<Result<IEnumerable<ServiceListView>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetServicesRequest, Result<IEnumerable<ServiceListView>>>
{
    public async Task<Result<IEnumerable<ServiceListView>>> Handle(GetServicesRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles
            .Include(x => x.Services)
            .ThenInclude(x => x.ServiceOptions)
            .SingleOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);

        if (profile == null)
            return Result<IEnumerable<ServiceListView>>.Fail("Profile not found");

        var services = profile.Services
            .Where(x => x.ServiceOptions.Count != 0)
            .Select(x => new ServiceListView(x.Id, x.Name, x.ServiceOptions.Min(x => x.Price), x.Icon, x.Description,
                x.CategoryId))
            .Concat(
                profile.Services.Where(x => x.ServiceOptions.Count == 0)
                    .Select(x => new ServiceListView(x.Id, x.Name, 0, x.Icon, x.Description, x.CategoryId))
            ).ToArray();
        return Result<IEnumerable<ServiceListView>>.Ok(services);
    }
}

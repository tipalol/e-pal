using Epal.Application.Common;
using Epal.Application.Features.Activity.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Activity.GetAll;

public record GetAllActiviesRequest() : IRequest<Result<IEnumerable<ActivityModel>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetAllActiviesRequest, Result<IEnumerable<ActivityModel>>>
{
    public async Task<Result<IEnumerable<ActivityModel>>> Handle(GetAllActiviesRequest request, CancellationToken cancellationToken)
    {

        var activities = await context.Services
            .Select(x => new ActivityModel(x))
            .ToListAsync(cancellationToken);
        return activities.Count == 0 ?  Result<IEnumerable<ActivityModel>>.Fail("Profile not found") : Result<IEnumerable<ActivityModel>>.Ok(activities);
    }
}

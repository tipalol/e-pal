using Epal.Application.Common;
using Epal.Application.Features.Activity.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Activity.Get.MyActivies;


public record MyActivityRequest() : IRequest<Result<IEnumerable<ActivityModel>>>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<MyActivityRequest, Result<IEnumerable<ActivityModel>>>
{
    public async Task<Result<IEnumerable<ActivityModel>>> Handle(MyActivityRequest request, CancellationToken cancellationToken)
    {
        var Id = userService.AuthenticatedUser.Id;
        // ToDO напиши запрос мне лень думать, тут надо на ActivityInProfle смотреть
        var activities = await context.Activities
            .Where(x => x.Id == Id)
            .Select(x => new ActivityModel(x))
            .ToListAsync(cancellationToken);
        return activities.Count == 0 ?  Result<IEnumerable<ActivityModel>>.Fail("Profile not found") : Result<IEnumerable<ActivityModel>>.Ok(activities);
    }
}
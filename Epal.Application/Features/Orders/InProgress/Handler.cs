using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.InProgress;

public record SetOrderStatusInProgress(Guid OrderId) : IRequest<Result>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<SetOrderStatusInProgress, Result>
{
    public async Task<Result> Handle(SetOrderStatusInProgress request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile is null)
            throw new ArgumentException("User not found");

        var order = await context.Orders
            .SingleOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order is null)
            return Result.Fail("Order not found");

        if (order.SellerId != profileId)
            return Result.Fail("You can't move status of this order");

        order.Status = OrderStatus.InProgress;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

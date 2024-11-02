using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.Dispute;

public record DisputeOrderRequest(Guid OrderId, string Reason) : IRequest<Result>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<DisputeOrderRequest, Result>
{
    public async Task<Result> Handle(DisputeOrderRequest request, CancellationToken cancellationToken)
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

        if (order.BuyerId != profileId)
            return Result.Fail("You can't dispute this order");

        order.Status = OrderStatus.Disputed;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.Approve;

public record ApproveOrderRequest(Guid OrderId) : IRequest<Result>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<ApproveOrderRequest, Result>
{
    public async Task<Result> Handle(ApproveOrderRequest request, CancellationToken cancellationToken)
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
            return Result.Fail("You can't approve this order");

        order.Status = OrderStatus.Approved;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

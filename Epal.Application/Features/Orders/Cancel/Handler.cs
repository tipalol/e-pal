using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.Cancel;

public record CancelOrderRequest(Guid OrderId) : IRequest<Result>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<CancelOrderRequest, Result>
{
    public async Task<Result> Handle(CancelOrderRequest request, CancellationToken cancellationToken)
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

        order.Status = OrderStatus.Canceled;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

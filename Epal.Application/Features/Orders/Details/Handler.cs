using Epal.Application.Features.Orders.Details.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.Details;

public record GetOrderDetailsRequest(Guid OrderId) : IRequest<OrderDetails>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<GetOrderDetailsRequest, OrderDetails>
{
    public async Task<OrderDetails> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile is null)
            throw new ArgumentException("Profile not found");

        var order = await context.Orders
            .Where(x => x.Id == request.OrderId)
            .Where(x => x.BuyerId == profileId || x.SellerId == profileId)
            .Select(x => new OrderDetails(
                x.Id, x.Status, x.Created.ToString("dd.MM.yyyy"),
                new OrderDetailsSellerView(x.Seller.Id, x.Seller.Username!, ""),
                new OrderDetailsServiceView(x.ServiceOption.Id, x.ServiceOption.Name))
            )
            .SingleOrDefaultAsync(cancellationToken);

        if (order is null)
            throw new ArgumentException("order is not found");

        return order;
    }
}
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.Add;

public record AddOrderRequest(Guid ServiceOptionId) : IRequest;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<AddOrderRequest>
{
    public async Task Handle(AddOrderRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile is null)
            throw new ArgumentException("User not found");

        var serviceInfo = await context.ServiceOptions
            .Where(x => x.Id == request.ServiceOptionId)
            .Select(x => new { SellerId = x.Service.ProfileId, Total = x.Price })
            .SingleOrDefaultAsync(cancellationToken);

        if (serviceInfo is null)
            throw new ArgumentException("Service option not found");

        var order = new Order
        {
            Status = OrderStatus.Created,
            SellerId = serviceInfo.SellerId,
            BuyerId = profileId,
            Total = serviceInfo.Total,
            ServiceOptionId = request.ServiceOptionId
        };

        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
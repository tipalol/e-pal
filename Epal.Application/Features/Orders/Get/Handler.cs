using Epal.Application.Features.Orders.Get.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Orders.Get;

public record GetOrdersRequest(OrderStatus? StatusFilter, OrderType OrderType) : IRequest<IEnumerable<OrderDto>>;

public class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<GetOrdersRequest, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Users
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile is null)
            throw new ArgumentException("Profile not found");

        var query = context.Orders
            .AsNoTracking();

        query = request.OrderType == OrderType.Buy
            ? query.Where(x => x.BuyerId == profileId)
            : query.Where(x => x.SellerId == profileId);

        if (request.StatusFilter.HasValue)
            query = query.Where(x => x.Status == request.StatusFilter);

        var orders = await query
            .OrderByDescending(x => x.Created)
            .Select(x => new OrderDto(x.Id, x.Status, x.Service.Name))
            .ToArrayAsync(cancellationToken);

        return orders;
    }
}
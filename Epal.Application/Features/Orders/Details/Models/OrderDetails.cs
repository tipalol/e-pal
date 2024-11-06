using Epal.Domain.Enums;

namespace Epal.Application.Features.Orders.Details.Models;

public record OrderDetails(Guid Id, OrderStatus Status, string Created, OrderDetailsSellerView DetailsSeller, OrderDetailsServiceView DetailsService);

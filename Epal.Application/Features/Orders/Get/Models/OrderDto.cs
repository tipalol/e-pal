using Epal.Domain.Enums;

namespace Epal.Application.Features.Orders.Get.Models;

public record OrderDto(Guid Id, OrderStatus Status, string Date, int Quantity, double Total, OrderServiceView Service, OrderSellerView Seller);

public record OrderSellerView(Guid Id, string Name, string Avatar);

public record OrderServiceView(Guid Id, string Name, string Icon);
using Epal.Domain.Enums;

namespace Epal.Application.Features.Orders.Get.Models;

public record OrderDto(Guid Id, OrderStatus Status, string Service);
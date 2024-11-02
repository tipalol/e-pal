using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;

namespace Epal.Domain.Entities;

public class Order : DateTrackedEntity
{
    public double Total { get; set; }
    public OrderStatus Status { get; set; }

    public Guid ServiceOptionId { get; set; }
    public ServiceOption ServiceOption { get; set; }

    public Guid BuyerId { get; set; }
    public Profile Buyer { get; set; }

    public Guid SellerId { get; set; }
    public Profile Seller { get; set; }
}

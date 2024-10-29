using Epal.Api.Controllers.Base;
using Epal.Application.Features.Orders.Add;
using Epal.Application.Features.Orders.Details;
using Epal.Application.Features.Orders.Get;
using Epal.Application.Features.Orders.Get.Models;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

[Authorize]
public class OrdersController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetAll(OrderStatus statusFilter, OrderType typeFilter)
        => await Sender.Send(new GetOrdersRequest(statusFilter, typeFilter));
    
    [HttpGet("{orderId:guid}")]
    public async Task Get([FromRoute(Name = "orderId")] Guid orderId)
        => await Sender.Send(new GetOrderDetailsRequest(orderId));
    
    [HttpPost]
    public async Task Add(AddOrderRequest request)
        => await Sender.Send(request);
}
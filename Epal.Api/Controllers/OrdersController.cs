using Epal.Api.Controllers.Base;
using Epal.Application.Features.Orders.Add;
using Epal.Application.Features.Orders.Approve;
using Epal.Application.Features.Orders.Cancel;
using Epal.Application.Features.Orders.Complete;
using Epal.Application.Features.Orders.Details;
using Epal.Application.Features.Orders.Dispute;
using Epal.Application.Features.Orders.Get;
using Epal.Application.Features.Orders.Get.Models;
using Epal.Application.Features.Orders.InProgress;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

[Authorize]
public class OrdersController(ISender sender) : RestController(sender)
{
    /// <summary>
    /// Получить все заказы для текущего пользователя
    /// </summary>
    /// <param name="statusFilter">Фильтр по статусам заказа</param>
    /// <param name="typeFilter">Фильтр по типу заказа: приходящий/исходящий</param>
    /// <returns>Список заказов</returns>
    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetAll(OrderStatus statusFilter, OrderType typeFilter)
        => await Sender.Send(new GetOrdersRequest(statusFilter, typeFilter));

    /// <summary>
    /// Получить заказ по айди
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    [HttpGet("{orderId:guid}")]
    public async Task Get([FromRoute(Name = "orderId")] Guid orderId)
        => await Sender.Send(new GetOrderDetailsRequest(orderId));

    [HttpPost]
    public async Task Add(AddOrderRequest request)
        => await Sender.Send(request);

    [HttpPost("approve")]
    public async Task Approve(Guid orderId)
        => await Sender.Send(new ApproveOrderRequest(orderId));

    [HttpPost("inProgress")]
    public async Task InProgress(Guid orderId)
        => await Sender.Send(new SetOrderStatusInProgress(orderId));

    [HttpPost("complete")]
    public async Task Complete(Guid orderId)
        => await Sender.Send(new CompleteOrderRequest(orderId));

    [HttpPost("cancel")]
    public async Task Cancel(Guid orderId)
        => await Sender.Send(new CancelOrderRequest(orderId));

    [HttpPost("dispute")]
    public async Task Dispute(Guid orderId, string reason)
        => await Sender.Send(new DisputeOrderRequest(orderId, reason));
}

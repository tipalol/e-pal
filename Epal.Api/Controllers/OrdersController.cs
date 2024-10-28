using Epal.Api.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

[Authorize]
public class OrdersController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public Task GetAll()
        => throw new NotImplementedException();
    
    [HttpGet("placed")]
    public Task GetPlaced()
        => throw new NotImplementedException();
    
    [HttpGet("pending")]
    public Task GetPending()
        => throw new NotImplementedException();
    
    [HttpGet("finished")]
    public Task GetFinished()
        => throw new NotImplementedException();
    
    [HttpGet("{orderId}")]
    public Task Get(Guid orderId)
        => throw new NotImplementedException();
    
    [HttpPost]
    public Task Add()
        => throw new NotImplementedException();
}
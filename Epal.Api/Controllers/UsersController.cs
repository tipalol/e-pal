using Epal.Api.Controllers.Base;
using Epal.Application.Features.Users.Get;
using Epal.Application.Features.Users.GetAll;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class UsersController(ISender sender) : RestController(sender)
{
    [HttpGet("{id:guid}")]
    public async Task<User> GetUser([FromRoute(Name = "id")] Guid id)
        => await Sender.Send(new GetUserRequest(id));
    
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers()
        => await Sender.Send(new GetUsersRequest());
}
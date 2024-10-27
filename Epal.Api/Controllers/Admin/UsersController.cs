using Epal.Api.Controllers.Base;
using Epal.Application.Features.Admin.Users.Add;
using Epal.Application.Features.Admin.Users.Get;
using Epal.Application.Features.Admin.Users.GetAll;
using Epal.Application.Features.Admin.Users.Remove;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Admin;

public class UsersController(ISender sender) : RestController(sender)
{
    /// <summary>
    /// Получение пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers()
        => await Sender.Send(new GetUsersRequest());
    
    [HttpGet("{id:guid}")]
    public async Task<User> GetUser([FromRoute(Name = "id")] Guid id)
        => await Sender.Send(new GetUserRequest(id));
    
    [HttpPost]
    public async Task<User> AddUser(CreateUserRequest request)
        => await Sender.Send(request);
    
    [HttpDelete("{id:guid}")]
    public async Task DeleteUser([FromRoute(Name = "id")] Guid id)
        => await Sender.Send(new RemoveUserRequest(id));
}
using Epal.Api.Controllers.Base;
using Epal.Application.Features.Admin.Users.Add;
using Epal.Application.Features.Admin.Users.Get;
using Epal.Application.Features.Admin.Users.GetAll;
using Epal.Application.Features.Admin.Users.Remove;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Admin;

/// <summary>
/// Админ контроллер для управления пользователями
/// </summary>
//[Authorize]
public class UsersController(ISender sender) : RestController(sender)
{
    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<Profile>> GetUsers()
        => await Sender.Send(new GetUsersRequest());

    /// <summary>
    /// Получение пользователя по айди
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<Profile> GetUser([FromRoute(Name = "id")] Guid id)
        => await Sender.Send(new GetUserRequest(id));

    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    [HttpPost]
    public async Task<Profile> AddUser(CreateUserRequest request)
        => await Sender.Send(request);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task DeleteUser([FromRoute(Name = "id")] Guid id)
        => await Sender.Send(new RemoveUserRequest(id));
}

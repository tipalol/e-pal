using Epal.Api.Controllers.Base;
using Epal.Application.Features.CheckExsistingUser;
using Epal.Application.Features.CheckExsistingUser.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.UserStatus;

public class UserStatusController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public async Task<StatusResponse> Get(string email)
        => await Sender.Send(new CheckUserRequest(email));
}
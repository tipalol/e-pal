using Epal.Api.Controllers.Base;
using Epal.Application.Features.CheckExsistingUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.UserStatus;

public class UserStatusController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public async Task<Domain.Enums.UserStatus> Register(CheckUserRequest request)
        => await Sender.Send(request);
}
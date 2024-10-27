using Epal.Api.Controllers.Base;
using Epal.Application.Features.Status;
using Epal.Application.Features.Status.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class UserStatusController(ISender sender) : RestController(sender)
{
    [HttpGet]
    public async Task<StatusResponse> Get(string email)
        => await Sender.Send(new StatusUserRequest(email));
}
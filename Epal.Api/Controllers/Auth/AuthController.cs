using Epal.Api.Controllers.Base;
using Epal.Application.Features.Authorize;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Auth;

public class AuthController(ISender sender) : RestController(sender)
{
    [HttpPost]
    public async Task<string> Authorize(AuthorizeRequest request)
        => await Sender.Send(request);
}
using Epal.Api.Controllers.Base;
using Epal.Application.Features.Authorize;
using Epal.Application.Features.Authorize.Refresh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class AuthController(ISender sender) : RestController(sender)
{
    [HttpPost]
    public async Task<string> Authorize(AuthorizeRequest request)
        => await Sender.Send(request);
    
    [HttpPost("refresh")]
    public async Task<string> RefreshToken()
        => await Sender.Send(new RefreshTokenRequest());
}
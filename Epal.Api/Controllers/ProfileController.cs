using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Profiles.Get;
using Epal.Application.Features.Profiles.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ProfileController(ISender sender) : RestController(sender)
{
    [HttpGet("username")]
    public async Task<Result<ProfileResponse>> GetByUsername(string username)
        => await Sender.Send(new ProfileRequest(username));
}
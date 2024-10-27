using System.Security.Claims;
using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Profiles.Get;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Features.Profiles.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ProfileController(ISender sender) : RestController(sender)
{
    [HttpGet("username")]
    public async Task<Result<ProfileResponse>> GetByUsername(string username)
        => await Sender.Send(new ProfileRequest(username));

    [HttpPost, Authorize]
    public async Task<Result<ProfileResponse>> UpdateProfile([FromBody]ProfileModel model)
    {
        return await Sender.Send((new UpdateUsernameRequest(model)));
    }
}
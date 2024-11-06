using Epal.Api.Controllers.Base;
using Epal.Application.Common;
using Epal.Application.Features.Profiles.Get;
using Epal.Application.Features.Profiles.Get.MyProfile;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Features.Profiles.Patch;
using Epal.Application.Features.Profiles.Post;
using Epal.Application.Features.Profiles.UpdateUsername;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class ProfileController(ISender sender) : RestController(sender)
{
    [HttpGet, Authorize]
    public async Task<Result<ProfileResponse>> GetMyProfile()
        => await Sender.Send(new MyProfileRequest());

    [HttpGet("{username}")]
    public async Task<Result<ProfileResponse>> GetByUsername([FromRoute(Name = "username")] string username)
        => await Sender.Send(new ProfileRequest(username));

    [HttpPost, Authorize]
    public async Task<Result<ProfileResponse>> UpdateProfile(ProfileModel profileModel)
        => await Sender.Send(new UpdateProfileRequest(profileModel));

    [HttpPost("becomeEpal"), Authorize]
    public async Task<Result> BecomeEpal()
        => await Sender.Send(new BecomeEpalRequest());

    [HttpPost("username"), Authorize]
    public async Task<Result> UpdateUsername(string username)
        => await Sender.Send(new UpdateUsernameRequest(username));
}

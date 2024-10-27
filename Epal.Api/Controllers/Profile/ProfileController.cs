using Epal.Api.Controllers.Base;
using Epal.Application.Features.Profile.Get;
using Epal.Application.Features.Registration;
using Epal.Application.Features.Registration.EmailConfirmation;
using Epal.Application.Features.Registration.ResendEmailConfirmation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Profile;

public class ProfileController(ISender sender) : RestController(sender)
{
    [HttpGet("profileId")]
    public async Task ProfileResponse(string profileId)
        => await Sender.Send(new ProfileRequest(profileId));
}
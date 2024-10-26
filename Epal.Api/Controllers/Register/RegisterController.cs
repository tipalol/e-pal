using Epal.Api.Controllers.Base;
using Epal.Application.Features.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers.Register;

public class RegisterController(ISender sender) : RestController(sender)
{
    [HttpPost]
    public async Task Register(RegistrationRequest request)
        => await Sender.Send(request);

    [HttpPost("confirm")]
    public async Task<bool> ConfirmEmail(int verificationCode)
        => await Sender.Send(verificationCode);
}
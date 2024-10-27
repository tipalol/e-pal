﻿using Epal.Api.Controllers.Base;
using Epal.Application.Features.Registration;
using Epal.Application.Features.Registration.EmailConfirmation;
using Epal.Application.Features.Registration.ResendEmailConfirmation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epal.Api.Controllers;

public class RegisterController(ISender sender) : RestController(sender)
{
    [HttpPost]
    public async Task Register(RegistrationRequest request)
        => await Sender.Send(request);
    
    [HttpPost("resend")]
    public async Task ResendEmail(ResendEmailConfirmationRequest verificationRequest)
        => await Sender.Send(verificationRequest);

    [HttpPost("confirm")]
    public async Task<bool> ConfirmEmail(EmailConfirmRequest verificationRequest)
        => await Sender.Send(verificationRequest);
}
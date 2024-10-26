﻿using Epal.Application.Features.Registration;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Epal.Application.Features.EMailConfirmation;

public record EmailConfirmRequest(string Email, int VerificationCode) : IRequest<bool>;

internal sealed class Handler(IVerificationService verificationService, IEpalDbContext context) : IRequestHandler<EmailConfirmRequest, bool>
{
    public async Task<bool> Handle(EmailConfirmRequest request, CancellationToken cancellationToken)
    {
        if (verificationService.Verify(request.Email, request.VerificationCode))
        {
            var user = await context.Users.FirstAsync(x => x.Email == request.Email, cancellationToken);
            user.Status = UserStatus.Confirmed;
            return true;
        }
        return false;
    }

 
}
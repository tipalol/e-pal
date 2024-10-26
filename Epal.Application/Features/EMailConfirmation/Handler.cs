using Epal.Application.Features.Registration;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Epal.Application.Features.EMailConfirmation;

public record EMailConfirmRequest(string Email, int VerificationCode) : IRequest<bool>;

internal sealed class Handler(IVerificationService verificationService) : IRequestHandler<EMailConfirmRequest, bool>
{
    public Task<bool> Handle(EMailConfirmRequest request, CancellationToken cancellationToken)
    {
        return verificationService.Verify(request.Email, request.VerificationCode) ? Task.FromResult(true) : Task.FromResult(false);
    }

 
}
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Registration.EmailConfirmation;

public record EmailConfirmRequest(string Email, int VerificationCode) : IRequest<bool>;

internal sealed class Handler(IVerificationService verificationService, IEpalDbContext context) : IRequestHandler<EmailConfirmRequest, bool>
{
    public async Task<bool> Handle(EmailConfirmRequest request, CancellationToken cancellationToken)
    {
        if (verificationService.Verify(request.Email, request.VerificationCode))
        {
            var user = await context.Profiles.FirstAsync(x => x.Email == request.Email, cancellationToken);
            user.Status = UserStatus.Confirmed;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }

 
}
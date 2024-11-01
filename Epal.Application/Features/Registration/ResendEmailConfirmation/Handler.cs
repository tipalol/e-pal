using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Registration.ResendEmailConfirmation;

public record ResendEmailConfirmationRequest(string Email) : IRequest;

public class Handler(IEpalDbContext context, IVerificationService verificationService) : IRequestHandler<ResendEmailConfirmationRequest>
{
    public async Task Handle(ResendEmailConfirmationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await context.Profiles
            .AnyAsync(x => x.Email == request.Email, cancellationToken);
        
        if (!userExists)
        {
            throw new ArgumentException("Пользователь с таким Email не существует");
        }

        await verificationService.SendVerificationCodeAsync(request.Email); 
    }
}
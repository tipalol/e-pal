using Epal.Application.Interfaces;
using MediatR;

namespace Epal.Application.Features.Profile.Post;

public record __Name(string Email, int VerificationCode) : IRequest<bool>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<__Name, bool>
{
    public Task<bool> Handle(__Name request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true); 
    }
}
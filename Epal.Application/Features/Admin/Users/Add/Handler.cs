using System.ComponentModel;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Admin.Users.Add;

public record CreateUserRequest(string Username, string Email, [PasswordPropertyText] string Password) : IRequest<Domain.Entities.Profile>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService) : IRequestHandler<CreateUserRequest, Domain.Entities.Profile>
{
    public async Task<Domain.Entities.Profile> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new Domain.Entities.Profile(request.Email, passwordHash);

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return user;
    }
}
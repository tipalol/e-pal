using System.ComponentModel;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Admin.Users.Add;

public record CreateUserRequest(string Username, string Email, [PasswordPropertyText] string Password) : IRequest<User>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService) : IRequestHandler<CreateUserRequest, User>
{
    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new User(request.Email, passwordHash);

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return user;
    }
}
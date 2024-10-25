using System.ComponentModel;
using Epal.Application.Interfaces;
using Epal.Application.Services;
using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Users.Add;

public record CreateUserRequest(string Username, string Email, [PasswordPropertyText] string password) : IRequest<User>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<CreateUserRequest, User>
{
    public async Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        string passwordHash = PasswordService.HashPassword(request.password);
        var user = new User(request.Username, request.Email, passwordHash);
        try
        {
            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
using System.ComponentModel;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;

namespace Epal.Application.Features.Admin.Users.Add;

public record CreateUserRequest(string Username, string Email, [PasswordPropertyText] string Password, ProfileType Type) : IRequest<Profile>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService) : IRequestHandler<CreateUserRequest, Profile>
{
    public async Task<Profile> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new Profile
        {
            Email = request.Email, 
            Username = request.Username,
            PasswordHash = passwordHash, 
            ProfileType = request.Type
        };

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return user;
    }
}
using Epal.Application.Features.Authorize.Token;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Registration;

public record RegistrationRequest(string Email, string Password) : IRequest<string>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService, IVerificationService verificationService, ISender sender) : IRequestHandler<RegistrationRequest, string>
{
    public async Task<string> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await context.Profiles
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (userExists)
        {
            throw new ArgumentException("Пользователь с таким Email уже существует");
        }

        verificationService.SendVerificationCodeAsync(request.Email);

        var passwordHash = passwordService.HashPassword(request.Password);
        var user = Profile.Create(request.Email, passwordHash);

        var username = Math.Abs(user.Id.GetHashCode() % 10000000).ToString();
        if (await context.Profiles.AnyAsync(x => x.Username == username, cancellationToken))
        {
            username = Math.Abs(user.Id.GetHashCode() % 10123321).ToString();
        }
        user.Username = username;

        await context.Profiles.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var token = await sender.Send(new TokenRequest(user.Id, user.Email, user.Username), cancellationToken);

        return token;
    }
}

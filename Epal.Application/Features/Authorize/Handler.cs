using System.Security;
using Epal.Application.Features.Authorize.Token;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;

namespace Epal.Application.Features.Authorize;
public record AuthorizeRequest(string Login, string Password) : IRequest<string>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService, ISender sender) : IRequestHandler<AuthorizeRequest, string>
{
    public async Task<string> Handle(AuthorizeRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        Profile? user;
        if (request.Login is null)
            throw new ArgumentException("Empty argument exception");
        if (request.Login.Contains('@'))
            user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Login, cancellationToken);
        else
            user = await context.Users.FirstOrDefaultAsync(x => x.Username == request.Login, cancellationToken);

        if (user is null)
            throw new ArgumentException("Пользователь не найден");
        if (user.PasswordHash != passwordHash)
            throw new PasswordException("Неправильный пароль");
        if (user.Status == UserStatus.Banned)
            throw new FieldAccessException("ВЫ з0банены ^_^/ пакеда");

        var token = await sender.Send(new TokenRequest(user.Id, user.Email, user.Username), cancellationToken);

        return token;
    }
}

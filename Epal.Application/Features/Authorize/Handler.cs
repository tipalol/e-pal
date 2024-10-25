using Epal.Application.Features.Token;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Authorize;
public record AuthorizeRequest(string Login, string Password) : IRequest<string>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService, ISender sender) : IRequestHandler<AuthorizeRequest, string>
{
    public async Task<string> Handle(AuthorizeRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        User? user;
        
        if (request.Login.Contains('@'))
            user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Login, cancellationToken);
        else
            user = await context.Users.FirstOrDefaultAsync(x => x.Username == request.Login, cancellationToken);
        
        if (user is null)
            throw new ArgumentException("Пользователь не найден");

        if (user.PasswordHash != passwordHash)
            throw new ArgumentException("Неправильный пароль");


        var token = await sender.Send(new TokenRequest(user.Id, user.Email, user.Username), cancellationToken);
        
        return token;
    }
}
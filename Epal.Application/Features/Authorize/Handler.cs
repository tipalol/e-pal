using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Epal.Application.Features.Users.Add;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Epal.Application.Features.Authorize;
public record CreateAuthorizeRequset(string Login, string Password) : IRequest<string>;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService) : IRequestHandler<CreateAuthorizeRequset, string>
{
    public async Task<string> Handle(CreateAuthorizeRequset request, CancellationToken cancellationToken)
    {
        var passwordHash = passwordService.HashPassword(request.Password);
        User? user;
        if (request.Login.Contains("@"))
            user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Login,
                cancellationToken: cancellationToken);
        else
            user = await context.Users.FirstOrDefaultAsync(x => x.Username == request.Login,
                cancellationToken: cancellationToken);
        if (user is null)
            throw new ArgumentException("Пользователь не найден");

        if (user.PasswordHash != passwordHash)
            throw new ArgumentException("Неправильный пароль");
        const string secretKey = "Ez4+=UE!~Jf}j<&ZAghEk:9n{ZP4TCTg";
        var token = CreateTokenString(user.Username, secretKey);
        return token;
    }


    private string CreateTokenString(string Username, string secretkey)
    {
        

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey ?? throw new ArgumentNullException(nameof(secretkey))));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(10 + 2),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
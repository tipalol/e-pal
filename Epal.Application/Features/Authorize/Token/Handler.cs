using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Epal.Application.Features.Authorize.Token;

public record TokenRequest(Guid Id, string Email, string? Username) : IRequest<string>;

public class Handler : IRequestHandler<TokenRequest, string>
{
    private const string SecretKey = "Ez4+=UE!~Jf}j<&ZAghEk:9n{ZP4TCTg";

    public Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
        => Task.FromResult(CreateTokenString(request.Id, request.Username, request.Email));
    
    private static string CreateTokenString(Guid id, string? username, string email)
    {
        List<Claim> claims = [
            new (ClaimTypes.Sid, id.ToString()),
            new (ClaimTypes.Name, username ?? string.Empty),
            new (ClaimTypes.Email, email),
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
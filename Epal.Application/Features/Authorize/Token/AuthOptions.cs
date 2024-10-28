using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Epal.Application.Features.Authorize.Token;

public class AuthOptions
{
    public const string Issuer = "Epal.Backend";
    public const string Audience = "Epal.Client";
    private const string Key = "Ez4+=UE!~Jf}j<&ZAghEk:9n{ZP4TCTg";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}
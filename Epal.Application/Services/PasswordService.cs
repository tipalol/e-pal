using System.Security.Cryptography;
using Epal.Application.Interfaces;

namespace Epal.Application.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);
        
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
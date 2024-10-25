using System.Security.Cryptography;

namespace Epal.Application.Services;

public static class PasswordService
{
    public static string HashPassword(string password)
    {
        // Используем алгоритм SHA-256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
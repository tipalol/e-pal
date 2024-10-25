namespace Epal.Application.Interfaces;

public interface IPasswordService
{
    public string HashPassword(string password);
}
using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

public class User : DateTrackedEntity
{
    public User()
    {
    }
    
    public User(string userName, string email, string passwordHash)
    {
        Username = userName;
        Email = email;
        PasswordHash = passwordHash;
        Balance = 0;
        EmailConfirmed = false;
        IsBanned = false;
    }
    
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool IsBanned { get; set; }
    public decimal Balance { get; set; }
}
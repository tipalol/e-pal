using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;

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
        Status = UserStatus.Created;
    }
    
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public decimal Balance { get; set; }
    public UserStatus Status { get; set; }
}
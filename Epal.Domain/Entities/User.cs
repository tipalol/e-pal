using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

public class User : DateTrackedEntity
{
    public User(){}
    public User(string userName, string email, string passwordHash)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Balance = 0;
        EmailConfirmed = false;
        isBanned = false;
    }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public bool isBanned { get; set; }//Banned
    //public int AccessFiledCount {get;set;}
    public decimal Balance { get; set; }
}
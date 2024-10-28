using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;


namespace Epal.Domain.Entities;

//TODO добавить сюда релейшн на предоаставляемые услуги
public class Profile : DateTrackedEntity
{
    public Profile()
    {
    }
    public Profile(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
        Status = UserStatus.Created;
    }
    public string Email { get; set; }
    public string? Username { get; set; }
    public string PasswordHash { get; set; }
    public decimal Balance { get; set; }
    public UserStatus Status { get; set; }
    public ProfileType ProfileType { get; set; }
}
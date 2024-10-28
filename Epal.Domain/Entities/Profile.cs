using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;

namespace Epal.Domain.Entities;

public class Profile : DateTrackedEntity
{
    public string Email { get; set; }
    public string Avatar { get; set; }
    public string Languages { get; set; }
    public string? Username { get; set; }
    public string PasswordHash { get; set; }
    public decimal Balance { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Created;
    public ProfileType ProfileType { get; set; } = ProfileType.User;

    public ICollection<Service> Services { get; set; } = [];
    public DateTime? EpalStatusAcquiring { get; set; }

    public static Profile Create(string email, string passwordHash)
    {
        return new Profile
        {
            Email = email,
            PasswordHash = passwordHash
        };
    }
}

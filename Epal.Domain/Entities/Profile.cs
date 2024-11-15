using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;

namespace Epal.Domain.Entities;

public class Profile : DateTrackedEntity
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Gender Gender { get; set; }
    public string Avatar { get; set; }
    public string? Bio { get; set; }
    public string Languages { get; set; } = "Russian";
    
    public decimal Balance { get; set; }
    public bool IsOnline { get; set; }
    public DateTime? LastActivityTime { get; set; }
    
    public UserStatus Status { get; set; } = UserStatus.Created;
    public ProfileType ProfileType { get; set; } = ProfileType.User;
    public DateTime? EpalStatusAcquiring { get; set; }

    public ICollection<Service> Services { get; set; } = [];
    public ICollection<Order> BoughtOrders { get; set; } = new List<Order>();
    public ICollection<Order> SoldOrders { get; set; } = new List<Order>();

    public static Profile Create(string email, string passwordHash)
    {
        return new Profile
        {
            Email = email,
            PasswordHash = passwordHash,
            ProfileType = ProfileType.User,
            Gender = Gender.Unselected,
            Languages = " ",
            Status = UserStatus.Created,
            Avatar = "https://global-oss.epal.gg/data/album/729833/1724368151270586.jpeg?x-oss-process=image/resize,m_fill,w_256,h_256"
        };
    }
}
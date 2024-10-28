using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;

namespace Epal.Domain.Entities;

public class Profile : DateTrackedEntity
{
    public Gender Gender { get; set; } = Gender.Unselected;
    public string Email { get; set; }
    public string Avatar { get; set; }
    public string Languages { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public decimal Balance { get; set; }
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
            Avatar = "https://th.bing.com/th/id/R.6eec4aaf95a7775913960d599b47eec8?rik=pqGASt3uwgtBnw&pid=ImgRaw&r=0?x-oss-process=image/resize,m_fill,w_256,h_256",
            Status = UserStatus.Created,
        };
    }
}
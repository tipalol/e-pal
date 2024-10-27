using System.ComponentModel.DataAnnotations.Schema;
using Epal.Domain.Entities.Base;
using Epal.Domain.Enums;

namespace Epal.Domain.Entities;

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
        Follower = new List<Profile>();
    }
    public string Email { get; set; }
    public string? ProfileId { get; set; }
    [NotMapped]
    public string? Username
    {
        get => ProfileId;
        set => ProfileId = value;
    }
    public string PasswordHash { get; set; }
    public decimal Balance { get; set; }
    public UserStatus Status { get; set; }
    public ICollection<Profile> Follower { get; set; }
}
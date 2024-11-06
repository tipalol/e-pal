using Epal.Domain.Entities;
using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Models;

 public class ProfileResponse
{
    public required Guid Id { get; set; }
    public string? Username { get; set; }
    public required UserStatus Status { get; set; }
    public Gender Gender { get; set; }
    public string Avatar { get; set; }
    public string Languages { get; set; } = "РУССКИЙ";
    public bool IsMyProfile { get; set; }
    public double Balance { get; set; }
    public ProfileType ProfileType { get; set; }

    public static ProfileResponse FromProfile(Profile profile, Guid? askingId)
    {
        return new ProfileResponse
        {
            Id = profile.Id,
            Username = profile.Username,
            Status = profile.Status,
            ProfileType = profile.ProfileType,
            Avatar = profile.Avatar,
            Languages = profile.Languages,
            Gender = profile.Gender,
            Balance = (double)profile.Balance,
            IsMyProfile = askingId != null && profile.Id == askingId
        };
    }
}

using Epal.Domain.Entities;
using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Models;

 public class ProfileResponse
{
    public required Guid Id { get; set; }
    public string? Username { get; set; }
    public required UserStatus Status { get; set; }
    public Gender Gender { get; set; }
    public string Avatar { get; set; } = "https://global-oss.epal.gg/data/album/729833/1724368151270586.jpeg?x-oss-process=image/resize,m_fill,w_256,h_256";
    public string Languages { get; set; } = "РУССКИЙ";

    public static ProfileResponse FromProfile(Profile profile)
    {
        return new ProfileResponse
        {
            Id = profile.Id,
            Username = profile.Username,
            Status = profile.Status,
            Avatar = profile.Avatar,
            Languages = profile.Languages,
            Gender = profile.Gender
        };
    }

   
}

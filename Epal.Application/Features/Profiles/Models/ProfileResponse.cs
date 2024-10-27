using Epal.Domain.Entities;
using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Models;

 public class ProfileResponse
{
    public required Guid Id { get; set; }
    public string? Username { get; set; }
    public required UserStatus UserStatus { get; set; }

    public static ProfileResponse FromProfile(Profile profile)
    {
        return new ProfileResponse
        {
            Id = profile.Id,
            Username = profile.Username,
            UserStatus = profile.Status
        };
    }
}

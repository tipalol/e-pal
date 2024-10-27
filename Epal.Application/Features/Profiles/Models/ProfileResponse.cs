﻿using Epal.Domain.Entities;
using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Models;

 public class ProfileResponse
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public string? Username { get; set; }
    public required decimal Balance { get; set; }
    public required UserStatus UserStatus { get; set; }
    public int FollowersCount { get; set; }

    public static ProfileResponse FromProfile(Profile profile)
    {
        return new ProfileResponse
        {
            FollowersCount = profile.Followers.Count(),
            Id = profile.Id,
            Email = profile.Email,
            Balance = 0,
            UserStatus = profile.Status
        };
    }
}
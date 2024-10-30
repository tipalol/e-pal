using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Models;

public record ProfileModel(string Username, Gender? Gender = Gender.Unselected, string Languages = "");
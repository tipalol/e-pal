using Epal.Domain.Enums;

namespace Epal.Application.Features.CheckExsistingUser.Models;

public record StatusResponse(bool Exists, UserStatus? Status = null);
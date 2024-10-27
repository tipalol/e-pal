using Epal.Domain.Enums;

namespace Epal.Application.Features.Status.Models;

public record StatusResponse(bool Exists, UserStatus? Status = null);
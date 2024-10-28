namespace Epal.Application.Features.Services.Add.Models;

public record ServiceDto(string Name, string Description, string Avatar, double Price, Guid ServiceTypeId);

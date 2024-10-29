namespace Epal.Application.Features.Services.AddOrUpdate.Models;

public record ServiceDto(Guid? Id, string Name, string Description, string Avatar, double Price, Guid ServiceTypeId);

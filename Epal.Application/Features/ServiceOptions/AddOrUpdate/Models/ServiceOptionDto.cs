namespace Epal.Application.Features.ServiceOptions.AddOrUpdate.Models;

public record ServiceOptionDto(Guid? Id, string Name, string Description, double Price, Guid ServiceId);

namespace Epal.Application.Features.Services.AddOrUpdate.Models;

public class ServiceDto()
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
    public double Price { get; set; }
    public Guid ServiceTypeId { get; set; }
}

using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

public class ServiceOption : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    
    public Guid ServiceId { get; set; }
    public Service Service { get; set; }
}
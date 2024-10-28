using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

public class Service : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
    public string Icon { get; set; }
    public double Price { get; set; }

    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
    public Guid ServiceTypeId { get; set; }
    public ServiceType ServiceType { get; set; }
}

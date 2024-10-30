using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

public class Category : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
    public ICollection<Service> Services { get; set; } = [];
}

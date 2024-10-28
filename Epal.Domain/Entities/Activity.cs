using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

// TODO SERVICE
public class Activity : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
    public string Icon { get; set; }
}
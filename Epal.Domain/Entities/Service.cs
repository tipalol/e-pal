using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities;

public class Service : Entity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    public string? Icon { get; set; }
    public string? Tags { get; set; }

    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<ServiceOption> ServiceOptions { get; set; } = [];
}

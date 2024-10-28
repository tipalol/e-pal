using Epal.Domain.Entities.Base;

namespace Epal.Domain.Entities.ManyToMany;

public class ProfileServices : Entity
{
    public Profile Profile { get; set; }
    public Guid ProfileId { get; set; }

    public Service Service { get; set; }
    public Guid ServiceId { get; set; }

    public double Price { get; set; }
    public bool IsActive { get; set; }
}

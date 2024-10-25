namespace Epal.Domain.Entities.Base;

public abstract class DateTrackedEntity : Entity
{
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? Updated { get; set; }
}
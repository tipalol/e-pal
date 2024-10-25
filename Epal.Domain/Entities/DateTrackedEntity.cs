namespace Epal.Domain.Entities;

public class DateTrackedEntity : Entity
{
    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? Updated { get; set; }

    public DateTrackedEntity() : base()
    {
        Created = DateTimeOffset.UtcNow;
    }
}
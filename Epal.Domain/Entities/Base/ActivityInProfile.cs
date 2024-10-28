using System.ComponentModel.DataAnnotations.Schema;

namespace Epal.Domain.Entities.Base;
//составной ключ
public class ActivityInProfile
{
    public Profile Profile { get; set; }
    public Guid ProfileId { get; set; }
    public Activity Activity { get; set; }
    public Guid ActivityId { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
}
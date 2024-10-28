namespace Epal.Application.Features.Activity.Models;

public class ActivityModel
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }
    public string Icon { get; set; }
    
    public ActivityModel()
    {
    }
    public ActivityModel(Domain.Entities.Activity activity)
    {
        Id = activity.Id;
        Name = activity.Name;
        Description = activity.Description;
        Avatar = activity.Avatar;
        Icon = activity.Icon;
    }
}
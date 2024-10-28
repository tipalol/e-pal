namespace Epal.Application.Features.Services.Models;

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
    public ActivityModel(Domain.Entities.Service service)
    {
        Id = service.Id;
        Name = service.Name;
        Description = service.Description;
        Avatar = service.Avatar;
        Icon = service.Icon;
    }
}

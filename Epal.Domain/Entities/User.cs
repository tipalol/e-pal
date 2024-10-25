namespace Epal.Domain.Entities;

public class User : DateTrackedEntity
{
    public string Email { get; set; }

    public string Password { get; set; }
}
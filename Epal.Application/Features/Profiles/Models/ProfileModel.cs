using Epal.Domain.Enums;

namespace Epal.Application.Features.Profiles.Models;

public class ProfileModel
{
    
    public string Username { get; set; }
    public Gender? Gender { get; set; }
    public ProfileModel()
    {
    }
    public ProfileModel(string username, Gender? gender)
    {
        Username = username;
        Gender = gender;
    }
    
}
using Epal.Domain.Entities;
using Epal.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Interfaces;

public interface IEpalDbContext
{
    public DbSet<Profile> Users { get; }
    public DbSet<Activity> Activities { get; }
    public DbSet<ActivityInProfile> ActivitiesInProfiles { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken token);
}
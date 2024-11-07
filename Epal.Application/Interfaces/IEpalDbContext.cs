using Epal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Interfaces;

public interface IEpalDbContext
{
    public DbSet<Profile> Profiles { get; }
    public DbSet<Service> Services { get; }
    public DbSet<ServiceOption> ServiceOptions { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Order> Orders { get; }
    public DbSet<ServiceExtraInfo> ServiceExtraInfos { get; }


    public Task<int> SaveChangesAsync(CancellationToken token);
}

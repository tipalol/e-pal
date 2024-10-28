using Epal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Interfaces;

public interface IEpalDbContext
{
    public DbSet<Profile> Users { get; }
    public DbSet<Service> Services { get; }
    public DbSet<ServiceType> ServiceTypes { get; }
    public DbSet<Order> Orders { get; }

    public Task<int> SaveChangesAsync(CancellationToken token);
}

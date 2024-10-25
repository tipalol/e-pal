using Epal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Interfaces;

public interface IEpalDbContext
{
    public DbSet<User> Users { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken token);
}
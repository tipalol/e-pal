using Epal.Domain.Entities;
using Epal.Domain.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epal.Infrastructure.Database.Configurations;

public class ProfileServicesConfiguration: IEntityTypeConfiguration<ProfileServices>
{
    public void Configure(EntityTypeBuilder<ProfileServices> builder)
    {
        builder
            .HasOne(e => e.Service)
            .WithMany()
            .HasForeignKey(e => e.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

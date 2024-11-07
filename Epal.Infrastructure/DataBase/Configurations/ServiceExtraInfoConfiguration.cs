using Epal.Domain.Entities;
using Epal.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epal.Infrastructure.Database.Configurations;

public class ServiceExtraInfoConfiguration : IEntityTypeConfiguration<ServiceExtraInfo>
{
    public void Configure(EntityTypeBuilder<ServiceExtraInfo> builder)
    {
        builder
            .HasOne(sei => sei.Service)
            .WithOne(s => s.ServiceExtraInfo)
            .HasForeignKey<ServiceExtraInfo>(sei => sei.ServiceId)
            .OnDelete(DeleteBehavior.Cascade); // Удаление каскадно

        builder
            .Property(sei => sei.Servers)
            .HasConversion(
                v => string.Join("/", v),
                v => v.Split('/', StringSplitOptions.RemoveEmptyEntries)
            );

        builder
            .Property(sei => sei.Styles)
            .HasConversion(
                v => string.Join("/", v),
                v => v.Split('/', StringSplitOptions.RemoveEmptyEntries)
            );

        builder
            .Property(sei => sei.Platforms)
            .HasConversion(
                v => string.Join("/", v),
                v => v.Split('/', StringSplitOptions.RemoveEmptyEntries)
            );

        builder
            .Property(sei => sei.Positions)
            .HasConversion(
                v => string.Join("/", v),
                v => v.Split('/', StringSplitOptions.RemoveEmptyEntries)
            );
    }
}
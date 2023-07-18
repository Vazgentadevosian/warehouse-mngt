using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WRMNGT.Data.Entities;

namespace WRMNGT.Data.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).HasMaxLength(255).IsRequired();
            builder.Property(l => l.Address).HasMaxLength(255).IsRequired();
            builder.Property(l => l.Capacity).IsRequired();
            builder.Property(l => l.OpenTime).IsRequired();
            builder.Property(l => l.CloseTime).IsRequired();
        }
    }
}

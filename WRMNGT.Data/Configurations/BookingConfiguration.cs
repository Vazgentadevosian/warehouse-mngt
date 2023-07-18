using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WRMNGT.Data.Entities;

namespace WRMNGT.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.Time).IsRequired();
            builder.Property(b => b.Goods).HasMaxLength(255);
            builder.Property(b => b.Carrier).HasMaxLength(255);
            builder.Property(b => b.State).IsRequired();
            builder.Property(b => b.LocationId).IsRequired();

            builder.HasOne(b => b.Location)
                .WithMany(l => l.Bookings)
                .HasForeignKey(b => b.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

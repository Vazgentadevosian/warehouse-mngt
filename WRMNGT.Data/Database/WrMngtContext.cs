using Microsoft.EntityFrameworkCore;
using System.Reflection;

using WRMNGT.Data.Entities;

namespace WRMNGT.Data.Database;

public class WrMngtContext: DbContext
{
    public WrMngtContext(DbContextOptions<WrMngtContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public override int SaveChanges()
    {
        var selectedEntityList = ChangeTracker.Entries()
            .Where(x => (x.Entity is IEntityCreatedAt) &&
                        (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();

        selectedEntityList.ForEach(entity =>
        {
            if (entity.State == EntityState.Added)
            {
                if (entity.Entity is IEntityCreatedAt)
                    ((IEntityCreatedAt)entity.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }
        });

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
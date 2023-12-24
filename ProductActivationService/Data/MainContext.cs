using Microsoft.EntityFrameworkCore;
using ProductActivationService.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProductActivationService.Data
{
    /// <summary>
    /// MainContext
    /// </summary>
    public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
    {
        public DbSet<CustomerEntity> Customer { get; set; } = null!;

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}

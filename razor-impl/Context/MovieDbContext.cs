using Microsoft.EntityFrameworkCore;

namespace razor_impl.Context;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
    {
    }

    public DbSet<razor_impl.Models.Movie>? Movie { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var key = entry.Metadata.FindPrimaryKey();

            if (key != null)
            {
                var keyValues = key.Properties.Select(p => entry.Property(p.Name).CurrentValue).ToArray();

                var keyValueString = string.Join(", ", keyValues);
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}, Key: {keyValueString}");
            }

            if (entry.State == EntityState.Added)
            {

            }
            else if (entry.State == EntityState.Modified)
            {

            }
            else if (entry.State == EntityState.Deleted)
            {

            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

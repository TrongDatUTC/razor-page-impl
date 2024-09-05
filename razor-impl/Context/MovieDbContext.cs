using Microsoft.EntityFrameworkCore;

namespace razor_impl.Context;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
    {
    }

    public DbSet<razor_impl.Models.Movie>? Movie { get; set; }
}

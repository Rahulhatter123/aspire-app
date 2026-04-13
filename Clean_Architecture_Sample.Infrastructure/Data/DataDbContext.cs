using Clean_Architecture_Sample.Domain;
using Microsoft.EntityFrameworkCore;

namespace Clean_Architecture_Sample.Infrastructure.Data;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ImageFile).HasMaxLength(500);
        });
    }
}
using day09.Models;
using Microsoft.EntityFrameworkCore;

namespace day09.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Name: không trùng
        b.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        b.Entity<Product>().HasIndex(x => x.Name).IsUnique();
        b.Entity<Banner>().HasIndex(x => x.Name).IsUnique();
        b.Entity<Blog>().HasIndex(x => x.Name).IsUnique();

        // Product.CategoryId: khóa ngoại -> Category
        b.Entity<Product>()
            .HasOne(p => p.Category).WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class AppDbContext : DbContext
{
    private readonly string _connectionString;

    public AppDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public AppDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("polygon");
        modelBuilder.Entity<Wishlist>().HasKey(l => new {l.Login, l.Ticker});
    }
}
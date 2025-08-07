using InventoryTransfersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTransfersAPI.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<InventoryTransfer> InventoryTransfers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<InventoryTransfer>(e =>
      {
        e.Property(p => p.Status).HasDefaultValue("Pending");
        e.HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);
      });
      base.OnModelCreating(builder);
    }
  }
}
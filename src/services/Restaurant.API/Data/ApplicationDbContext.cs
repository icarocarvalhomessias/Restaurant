using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Models;

namespace Restaurant.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.ClientName).IsRequired().HasMaxLength(100);
                entity.Property(o => o.ClientAddress).IsRequired().HasMaxLength(200);
                entity.Property(o => o.ClientPhone).IsRequired().HasMaxLength(15);
                entity.Property(o => o.OrderStatus).IsRequired();
                entity.Property(o => o.RegisterDate).IsRequired();
                entity.Property(o => o.TotalValue).IsRequired().HasColumnType("decimal(18,2)");
            });

            // Configuring Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.Active).IsRequired();
                entity.Property(p => p.RegisterDate).IsRequired();
                entity.Property(p => p.Image).HasMaxLength(200);
                entity.Property(p => p.Stock).IsRequired();
            });

            // Configuring many-to-many relationship between Order and Product
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct",
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j => j.HasOne<Order>().WithMany().HasForeignKey("OrderId")
                );
        }
    }
}

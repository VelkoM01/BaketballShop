using BasketballShopSharedLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballShopServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<UserAccount> UserAccounts { get; set; } = default!;
        public DbSet<SystemRole> SystemRoles { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;
        public DbSet<TokenInfo> TokenInfo { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)"); 
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CheckoutDbContext : DbContext
    {
        public CheckoutDbContext(DbContextOptions<CheckoutDbContext> options) : base(options)
        { }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<BasketHistoryEntity> BasketHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>()
            .HasOne<BasketHistoryEntity>(s => s.BasketHistory)
            .WithOne(ad => ad.Product)
            .HasForeignKey<BasketHistoryEntity>(ad => ad.ProductId);

            modelBuilder.Entity<CustomerEntity>()
            .HasOne<BasketHistoryEntity>(s => s.BasketHistory)
            .WithOne(ad => ad.Customer)
            .HasForeignKey<BasketHistoryEntity>(ad => ad.CustomerId);

            new CustomerMap(modelBuilder.Entity<CustomerEntity>());
            new ProductMap(modelBuilder.Entity<ProductEntity>());
            new BasketHistoryMap(modelBuilder.Entity<BasketHistoryEntity>());
        }
    }
}

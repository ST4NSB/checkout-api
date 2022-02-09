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

            new CustomerMap(modelBuilder.Entity<CustomerEntity>());
            new ProductMap(modelBuilder.Entity<ProductEntity>());
            new BasketHistoryMap(modelBuilder.Entity<BasketHistoryEntity>());
        }
    }
}

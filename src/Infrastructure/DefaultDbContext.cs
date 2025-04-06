using Domain.Entity;
using Infrastructure.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class DefaultDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DefaultDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Dealer> Dealer { get; set; }
        public DbSet<DealerContact> DealerContact { get; set; }
        public DbSet<DealerDeliveryAddress> DealerDeliveryAddress { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ClientOrder> ClientOrder { get; set; }
        public DbSet<ClientOrderProduct> ClientOrderProduct { get; set; }
        public DbSet<SupplyOrder> SupplyOrder { get; set; }
        public DbSet<SupplyOrderClientOrder> SupplyOrderClientOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DealerMap());
            modelBuilder.ApplyConfiguration(new DealerContactMap());
            modelBuilder.ApplyConfiguration(new DealerPhoneMap());
            modelBuilder.ApplyConfiguration(new DealerDeliveryAddressMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductCategoryMap());
            modelBuilder.ApplyConfiguration(new ClientOrderMap());
            modelBuilder.ApplyConfiguration(new ClientOrderProductMap());
            modelBuilder.ApplyConfiguration(new SupplyOrderMap());
            modelBuilder.ApplyConfiguration(new SupplyOrderClientOrderMap());
        }
    }
}

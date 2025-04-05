using Domain.Entity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dealer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CNPJ).IsRequired().HasMaxLength(14);
                entity.Property(e => e.RazaoSocial).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NomeFantasia).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.HasMany(e => e.TeleContacstDealer).WithOne().HasForeignKey(dc => dc.DealerId);
                entity.HasMany(e => e.DealerDeliveryAddress).WithOne().HasForeignKey(dda => dda.DealerId);
            });

            modelBuilder.Entity<DealerContact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(15);
            });

            modelBuilder.Entity<DealerDeliveryAddress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.SKU).IsRequired().HasMaxLength(50);
                entity.Property(e => e.StockQuantity).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt);
                entity.HasOne<ProductCategory>().WithMany().HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ClientOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.ClientCNPJ).IsRequired().HasMaxLength(14);
                entity.Property(e => e.StatusId).IsRequired();
                entity.HasMany(e => e.CLientOrderProducts).WithOne().HasForeignKey(cop => cop.ClientOrderId);
            });

            modelBuilder.Entity<ClientOrderProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            });
        }
    }
}

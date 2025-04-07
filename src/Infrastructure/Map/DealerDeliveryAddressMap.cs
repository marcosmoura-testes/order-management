using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class DealerDeliveryAddressMap : IEntityTypeConfiguration<DealerDeliveryAddress>
    {
        public void Configure(EntityTypeBuilder<DealerDeliveryAddress> builder)
        {
            builder.ToTable("DealerDeliveryAddress");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(d => d.Address).IsRequired().HasMaxLength(150);
            builder.Property(d => d.DealerId);
            builder.HasOne<Dealer>().WithMany(dealer => dealer.DealerDeliveryAddress).HasForeignKey(d => d.DealerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
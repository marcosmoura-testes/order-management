using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    internal class ClientOrderMap : IEntityTypeConfiguration<ClientOrder>
    {
        public void Configure(EntityTypeBuilder<ClientOrder> builder)
        {
            builder.ToTable("ClientOrders");

            builder.HasKey(co => co.Id);

            builder.Property(co => co.CreatedAt)
                   .IsRequired();

            builder.Property(co => co.ClientCNPJ)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(co => co.DealerId)
                   .IsRequired();

            builder.Property(co => co.StatusId)
                   .IsRequired();

            builder.Property(co => co.TotalAmount);

            builder.HasMany(co => co.CLientOrderProducts)
                   .WithOne()
                   .HasForeignKey(p => p.ClientOrderId);

        }
    }
}

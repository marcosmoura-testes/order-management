using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class ClientOrderProductMap : IEntityTypeConfiguration<ClientOrderProduct>
    {
        public void Configure(EntityTypeBuilder<ClientOrderProduct> builder)
        {
            builder.ToTable("ClientOrderProduct");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.ClientOrderId)
                .IsRequired();

            builder.Property(c => c.ProductId)
                .IsRequired();

            builder.Property(c => c.Quantity)
                .IsRequired();

            builder.Property(c => c.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}

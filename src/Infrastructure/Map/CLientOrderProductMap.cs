using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class CLientOrderProductMap : IEntityTypeConfiguration<ClientOrderProduct>
    {
        public void Configure(EntityTypeBuilder<ClientOrderProduct> builder)
        {
            builder.ToTable("ClientOrderProducts");

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

            builder.HasOne<ClientOrder>()
                .WithMany()
                .HasForeignKey(c => c.ClientOrderId);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(c => c.ProductId);
        }
    }
}

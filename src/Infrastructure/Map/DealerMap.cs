using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class DealerMap : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.ToTable("Dealer");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(d => d.RazaoSocial)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.NomeFantasia)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(d => d.TeleContacstDealer)
                .WithOne()
                .HasForeignKey(dc => dc.DealerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.DealerDeliveryAddress)
                .WithOne()
                .HasForeignKey(dda => dda.DealerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    internal class DealerPhoneMap : IEntityTypeConfiguration<DealerPhone>
    {
        public void Configure(EntityTypeBuilder<DealerPhone> builder)
        {
            builder.ToTable("DealerPhone");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(d => d.DealerId);

            builder.HasOne<Dealer>()
                .WithMany(d => d.PhonesDealer)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
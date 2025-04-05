﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class DealerContactMap : IEntityTypeConfiguration<DealerContact>
    {
        public void Configure(EntityTypeBuilder<DealerContact> builder)
        {
            builder.ToTable("DealerContact");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(d => d.DealerId);

            builder.HasOne<Dealer>()
                .WithMany(d => d.TeleContacstDealer)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
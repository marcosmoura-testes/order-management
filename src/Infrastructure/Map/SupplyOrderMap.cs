using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class SupplyOrderMap : IEntityTypeConfiguration<SupplyOrder>
    {
        public void Configure(EntityTypeBuilder<SupplyOrder> builder)
        {
            builder.ToTable("SupplyOrder");

            builder.HasKey(so => so.Id);

            builder.Property(so => so.Id)
                .ValueGeneratedOnAdd();

            builder.Property(so => so.CreatedAt)
                .IsRequired();

            builder.Property(so => so.DealerId)
                .IsRequired();

            builder.Property(co => co.TotalAmount);

            builder.HasMany(co => co.SupplyOrderClientOrders)
                   .WithOne()
                   .HasForeignKey(p => p.SupplyOrderId);
        }
    }
}

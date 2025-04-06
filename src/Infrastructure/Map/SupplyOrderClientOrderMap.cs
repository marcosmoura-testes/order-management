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
    public class SupplyOrderClientOrderMap : IEntityTypeConfiguration<SupplyOrderClientOrder>
    {
        public void Configure(EntityTypeBuilder<SupplyOrderClientOrder> builder)
        {
            builder.ToTable("SupplyOrderClientOrders");

            builder.HasKey(soco => soco.Id);

            builder.Property(soco => soco.SupplyOrderId)
                   .IsRequired();

            builder.Property(soco => soco.ClientOrderId)
                   .IsRequired();

        }
    }
}

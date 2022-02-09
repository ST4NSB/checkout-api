using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess
{
    public class BasketHistoryMap
    {
        public BasketHistoryMap(EntityTypeBuilder<BasketHistoryEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.BasketHistoryId);
            entityTypeBuilder.ToTable("baskethistory");

            entityTypeBuilder.Property(x => x.BasketHistoryId).HasColumnName("BasketHistoryId");
            entityTypeBuilder.Property(x => x.CustomerId).HasColumnName("CustomerId");
            entityTypeBuilder.Property(x => x.ProductId).HasColumnName("ProductId");
            entityTypeBuilder.Property(x => x.Quantity).HasColumnName("Quantity");
            entityTypeBuilder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").HasDefaultValue(DateTime.Now);
            entityTypeBuilder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").HasDefaultValue(DateTime.Now);
        }
    }
}
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

            entityTypeBuilder.Property(x => x.BasketHistoryId).HasColumnName("baskethistoryid");
            entityTypeBuilder.Property(x => x.CustomerId).HasColumnName("customerid");
            entityTypeBuilder.Property(x => x.ProductId).HasColumnName("productid");
            entityTypeBuilder.Property(x => x.Quantity).HasColumnName("quantity");
            entityTypeBuilder.Property(x => x.CreatedDate).HasColumnName("createddate").HasDefaultValue(DateTime.Now);
            entityTypeBuilder.Property(x => x.UpdatedDate).HasColumnName("updateddate").HasDefaultValue(DateTime.Now);
        }
    }
}
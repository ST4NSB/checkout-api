using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<ProductEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.ProductId);
            entityTypeBuilder.ToTable("products");

            entityTypeBuilder.Property(x => x.ProductId).HasColumnName("ProductId");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("Name");
            entityTypeBuilder.Property(x => x.Price).HasColumnName("Price");
            entityTypeBuilder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").HasDefaultValue(DateTime.Now);
        }
    }
}
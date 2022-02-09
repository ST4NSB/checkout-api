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

            entityTypeBuilder.Property(x => x.ProductId).HasColumnName("productid");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("name");
            entityTypeBuilder.Property(x => x.Price).HasColumnName("price");
            entityTypeBuilder.Property(x => x.CreatedDate).HasColumnName("createddate").HasDefaultValue(DateTime.Now);
        }
    }
}
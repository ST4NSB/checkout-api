using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess
{
    public class CustomerMap
    {
        public CustomerMap(EntityTypeBuilder<CustomerEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.CustomerId);
            entityTypeBuilder.ToTable("customers");

            entityTypeBuilder.Property(x => x.CustomerId).HasColumnName("customerid");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("name");
            entityTypeBuilder.Property(x => x.PaysVat).HasColumnName("paysvat");
            entityTypeBuilder.Property(x => x.ClosedBasket).HasColumnName("closedbasket").HasDefaultValue(false);
            entityTypeBuilder.Property(x => x.PaidBasket).HasColumnName("paidbasket").HasDefaultValue(false);
            entityTypeBuilder.Property(x => x.CreatedDate).HasColumnName("createddate").HasDefaultValue(DateTime.Now);
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class ProductMap
    {
        private EntityTypeBuilder<ProductEntity> entityTypeBuilder;

        public ProductMap(EntityTypeBuilder<ProductEntity> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}
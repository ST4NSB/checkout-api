using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class CustomerMap
    {
        private EntityTypeBuilder<CustomerEntity> entityTypeBuilder;

        public CustomerMap(EntityTypeBuilder<CustomerEntity> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}
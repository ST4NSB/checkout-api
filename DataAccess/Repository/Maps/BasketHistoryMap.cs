using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class BasketHistoryMap
    {
        private EntityTypeBuilder<BasketHistoryEntity> entityTypeBuilder;

        public BasketHistoryMap(EntityTypeBuilder<BasketHistoryEntity> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DomainCqrs.MongoDB
{
    [ConnectionStringName(DomainCqrsDbProperties.ConnectionStringName)]
    public class DomainCqrsMongoDbContext : AbpMongoDbContext, IDomainCqrsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDomainCqrs();
        }
    }
}
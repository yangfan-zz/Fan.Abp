using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DomainCqrs.MongoDB
{
    [ConnectionStringName(DomainCqrsDbProperties.ConnectionStringName)]
    public interface IDomainCqrsMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}

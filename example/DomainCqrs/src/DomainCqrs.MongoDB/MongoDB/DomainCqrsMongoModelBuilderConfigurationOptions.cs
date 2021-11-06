using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace DomainCqrs.MongoDB
{
    public class DomainCqrsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public DomainCqrsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}
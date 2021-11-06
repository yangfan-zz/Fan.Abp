using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace DomainCqrs.MongoDB
{
    public static class DomainCqrsMongoDbContextExtensions
    {
        public static void ConfigureDomainCqrs(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DomainCqrsMongoModelBuilderConfigurationOptions(
                DomainCqrsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}
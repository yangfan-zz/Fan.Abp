using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DomainCqrs.EntityFrameworkCore
{
    public class DomainCqrsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public DomainCqrsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
using Fan.Abp.Uow.FreeSql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Fan.Abp.FreeSql
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class FreeSqlModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
        }
    }
}

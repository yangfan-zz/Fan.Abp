using System;
using System.Threading.Tasks;
using Fan.Abp.FreeSql;
using FreeSql;
using FreeSqlDemo.FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FreeSqlDemo;

[DependsOn(
    typeof(FreeSqlModule),
    typeof(AbpAutofacModule)
)]
public class FreeSqlDemoModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        IFreeSql fsql = new FreeSqlBuilder()
            .UseConnectionString(DataType.MySql, @"data source=localhost;port=3306;user id=root;password=123456.;initial catalog=freesql;charset=utf8")
            .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
            .UseAutoSyncStructure(true)
            .Build();

        context.Services.AddSingleton(fsql);
        context.Services.AddFreeDbContext<DemoSqlDbContext>(options =>
        {
            options.UseFreeSql(fsql);
        });
    }

    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetRequiredService<ILogger<FreeSqlDemoModule>>();
        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        logger.LogInformation($"MySettingName => {configuration["MySettingName"]}");

        var hostEnvironment = context.ServiceProvider.GetRequiredService<IHostEnvironment>();
        logger.LogInformation($"EnvironmentName => {hostEnvironment.EnvironmentName}");

        return Task.CompletedTask;
    }
}

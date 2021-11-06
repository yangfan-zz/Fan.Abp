using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DomainCqrs.EntityFrameworkCore
{
    public class DomainCqrsHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DomainCqrsHttpApiHostMigrationsDbContext>
    {
        public DomainCqrsHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DomainCqrsHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DomainCqrs"));

            return new DomainCqrsHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}

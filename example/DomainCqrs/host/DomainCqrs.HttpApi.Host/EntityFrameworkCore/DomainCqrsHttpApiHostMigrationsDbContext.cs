using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DomainCqrs.EntityFrameworkCore
{
    public class DomainCqrsHttpApiHostMigrationsDbContext : AbpDbContext<DomainCqrsHttpApiHostMigrationsDbContext>
    {
        public DomainCqrsHttpApiHostMigrationsDbContext(DbContextOptions<DomainCqrsHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureDomainCqrs();
        }
    }
}

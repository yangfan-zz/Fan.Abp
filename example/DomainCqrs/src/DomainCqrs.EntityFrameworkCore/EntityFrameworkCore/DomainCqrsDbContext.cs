using DomainCqrs.Customers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DomainCqrs.EntityFrameworkCore
{
    [ConnectionStringName(DomainCqrsDbProperties.ConnectionStringName)]
    public class DomainCqrsDbContext : AbpDbContext<DomainCqrsDbContext>, IDomainCqrsDbContext
    {
        public DbSet<Customer> Customers { get; set; }


        public DomainCqrsDbContext(DbContextOptions<DomainCqrsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDomainCqrs();
        }
    }
}
using System;
using DomainCqrs.Customers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DomainCqrs.EntityFrameworkCore
{
    public static class DomainCqrsDbContextModelCreatingExtensions
    {
        public static void ConfigureDomainCqrs(
            this ModelBuilder builder,
            Action<DomainCqrsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DomainCqrsModelBuilderConfigurationOptions(
                DomainCqrsDbProperties.DbTablePrefix,
                DomainCqrsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

       

            builder.Entity<Customer>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Customers", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Name).IsRequired().HasMaxLength(64);
                b.Property(q => q.Age);


                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            
        }
    }
}
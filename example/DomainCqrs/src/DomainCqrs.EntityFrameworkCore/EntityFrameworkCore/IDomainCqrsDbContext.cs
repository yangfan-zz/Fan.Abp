using DomainCqrs.Customers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DomainCqrs.EntityFrameworkCore
{
    [ConnectionStringName(DomainCqrsDbProperties.ConnectionStringName)]
    public interface IDomainCqrsDbContext : IEfCoreDbContext
    {
     
        DbSet<Customer> Customers { get; }
         
    }
}
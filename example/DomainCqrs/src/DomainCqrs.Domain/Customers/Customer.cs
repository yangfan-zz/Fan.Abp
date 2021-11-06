using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace DomainCqrs.Customers
{
    public class Customer : FullAuditedAggregateRoot<Guid>
    {
        public Customer()
        {

        }

        public Customer(Guid id, string name, int age):base(id)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; internal set; }

        public int Age { get; internal set; }
    }
}

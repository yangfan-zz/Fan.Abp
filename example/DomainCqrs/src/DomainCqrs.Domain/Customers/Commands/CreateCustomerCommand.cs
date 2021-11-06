using System;
using System.ComponentModel.DataAnnotations;
using Fan.Abp.Cqrs.Commands;

namespace DomainCqrs.Customers.Commands
{
    public class CreateCustomerCommand : CreateCommand<Guid, Customer>
    {
        public CreateCustomerCommand(Guid id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        [Required]
        public string Name { get;  set; }

        public int Age { get;  set; }
    }

   
}

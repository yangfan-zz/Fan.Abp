using System;
using System.Threading;
using System.Threading.Tasks;
using Fan.Abp.Cqrs.Commands;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace DomainCqrs.Customers.Commands
{
    [UnitOfWork]
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Customer>
    {
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CreateCustomerCommandHandler(IRepository<Customer, Guid> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public virtual Task<Customer> HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            return _customerRepository.InsertAsync(new Customer(command.Id, command.Name, command.Age), cancellationToken: cancellationToken);
        }

    }
}

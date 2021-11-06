using System;
using System.Threading.Tasks;
using DomainCqrs.Customers.Commands;
using Fan.Abp.Cqrs.Commands;
using Shouldly;
using Xunit;

namespace DomainCqrs.Samples
{
    public class Customers_Tests : DomainCqrsDomainTestBase
    {
        private readonly ICommandsExecutor _commandsExecutor;

        public Customers_Tests()
        {
           _commandsExecutor = GetRequiredService<ICommandsExecutor>();
        }

        [Fact]
        public async Task CreateCustomerCommandTest()
        {
            var id = Guid.NewGuid();
            var customer = await _commandsExecutor.ExecuteAsync(new CreateCustomerCommand(id, "Fan", 18));

            customer.ShouldNotBeNull();
            customer.Id.ShouldBe(id);
        }
    }
}

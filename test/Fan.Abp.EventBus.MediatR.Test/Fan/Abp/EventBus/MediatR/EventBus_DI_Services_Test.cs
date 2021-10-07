using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Fan.Abp.EventBus.MediatR
{
    public class EventBus_DI_Services_Test : EventBusTestBase
    {
        [Fact]
        public async Task Test()
        {
            await LocalEventBus.PublishAsync(new MySimpleEventData(1));
            MySimpleEventDataNotificationHandler.TotalData.ShouldBe(1);
        }
    }
}

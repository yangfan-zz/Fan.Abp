using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Fan.Abp.EventBus.MediatR
{
    public class MySimpleEventDataNotificationHandler: INotificationHandler<MySimpleEventData>
    {
        public static int TotalData { get; private set; }

        public Task Handle(MySimpleEventData eventData, CancellationToken cancellationToken)
        {
            TotalData += eventData.Value;
            return Task.CompletedTask;
        }
    }
}

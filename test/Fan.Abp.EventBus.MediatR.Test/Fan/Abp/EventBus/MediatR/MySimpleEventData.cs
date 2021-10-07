using System;
using MediatR;
using Volo.Abp.MultiTenancy;

namespace Fan.Abp.EventBus.MediatR
{
    public class MySimpleEventData : IMultiTenant, INotification
    {
        public int Value { get; set; }

        public Guid? TenantId { get; }

        public MySimpleEventData(int value, Guid? tenantId = null)
        {
            Value = value;
            TenantId = tenantId;
        }
    }
}

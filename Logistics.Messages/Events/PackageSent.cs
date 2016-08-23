using System;
using NServiceBus;

namespace Logistic.Messages.Events
{
    public class PackageSent : IEvent
    {
        public Guid OrderId { get; set; }
        public int PackageId { get; set; }
    }
}

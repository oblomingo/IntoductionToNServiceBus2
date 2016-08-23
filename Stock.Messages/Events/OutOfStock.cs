using System;
using NServiceBus;

namespace Stock.Messages.Events
{
    public class OutOfStock : IEvent
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
    }
}

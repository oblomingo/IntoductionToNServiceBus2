using System;
using NServiceBus;

namespace Sales.Messages.Events
{
    public class SaleCompleted : IEvent
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}

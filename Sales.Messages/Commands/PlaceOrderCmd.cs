using System;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class PlaceOrderCmd : ICommand
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public Guid OrderId { get; set; }
        public int ShippingTypeId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

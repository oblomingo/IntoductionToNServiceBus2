using System;
using NServiceBus;
using Stock.Messages.Dto;

namespace Stock.Messages.Events
{
    public class PackagePrepared : IEvent
    {
        public Guid OrderId { get; set; }
        public int PackageId { get; set; }
        public PackageSize Size { get; set; }
    }
}

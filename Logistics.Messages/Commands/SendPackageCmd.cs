using System;
using NServiceBus;
using Stock.Messages.Dto;

namespace Logistic.Messages.Commands
{
    public class SendPackageCmd : ICommand
    {
        public Guid OrderId { get; set; }
        public int PackageId { get; set; }
        public int UserId { get; set; }
        public int ShippingTypeId { get; set; }
        public PackageSize Size { get; set; }
    }
}
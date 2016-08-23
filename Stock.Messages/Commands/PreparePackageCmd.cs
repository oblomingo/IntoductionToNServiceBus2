using System;
using NServiceBus;

namespace Stock.Messages.Commands
{
    public class PreparePackageCmd : ICommand
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
    }
}

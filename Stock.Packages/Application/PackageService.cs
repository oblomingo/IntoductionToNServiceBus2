using NServiceBus;
using NServiceBus.Logging;
using Stock.Messages.Commands;
using Stock.Messages.Events;
using Stock.Packages.Infrastructure;

namespace Stock.Packages.Application
{
    public class PackageService : IHandleMessages<PreparePackageCmd>
    {
        private readonly ILog _log;
        private readonly PackageRepository _packageRepository;
        public PackageService()
        {
            _packageRepository = new PackageRepository();
            _log = LogManager.GetLogger(typeof(PackageService));
        }

        public IBus Bus { get; set; }

        public void Handle(PreparePackageCmd message)
        {
            var package = _packageRepository.PreparePackage(message.ProductId, message.OrderId);
            if (package != null)
            {
                _log.InfoFormat("Package prepared for order id {0}, package size and weigth: {1}", message.OrderId, package.SizeAndWeigth);

                var packagePrepared = new PackagePrepared
                {
                    OrderId = message.OrderId,
                    PackageId = package.PackageId,
                    Size = package.Size
                };
                Bus.Publish(packagePrepared);
            }
            else
            {
                _log.InfoFormat("Product with id {0} is out of stock  {1}", message.ProductId);
                var outOfStock = new OutOfStock
                {
                    OrderId = message.OrderId,
                    ProductId = message.ProductId
                };
                Bus.Publish(outOfStock);
            }
        }
    }
}

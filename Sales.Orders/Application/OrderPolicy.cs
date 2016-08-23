using System;
using Logistic.Messages.Commands;
using Logistic.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Sales.Messages.Commands;
using Sales.Messages.Events;
using Sales.Orders.Domain;
using Sales.Orders.Infrastructure;
using Stock.Messages.Commands;
using Stock.Messages.Events;

namespace Sales.Orders.Application
{
    public class OrderPolicy : Saga<OrderPolicyData>,
        IAmStartedByMessages<PlaceOrderCmd>,
        IHandleMessages<PackagePrepared>,
        IHandleMessages<OutOfStock>,
        IHandleMessages<PackageSent>,
        IHandleTimeouts<VerifyPackagePreparationExpiredTimeout>
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(OrderPolicy));
        private readonly int _maxDaysForPackagePreparation;
        public OrderPolicy()
        {
            _maxDaysForPackagePreparation = 10;
        }
        
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderPolicyData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrderCmd>(msg => msg.OrderId).ToSaga(saga => saga.OrderId);
            mapper.ConfigureMapping<PackagePrepared>(msg => msg.OrderId).ToSaga(saga => saga.OrderId);
            mapper.ConfigureMapping<OutOfStock>(msg => msg.OrderId).ToSaga(saga => saga.OrderId);
            mapper.ConfigureMapping<PackageSent>(msg => msg.OrderId).ToSaga(saga => saga.OrderId);
        }

        public void Handle(PlaceOrderCmd message)
        {
            Data.Status = (int) Enums.OrderStatuses.OrderCreated;
            Data.UserId = message.UserId;
            Data.ProductId = message.ProductId;
            Data.ShippingTypeId = message.ShippingTypeId;
            Data.OrderId = message.OrderId;

            _log.InfoFormat("Order created for user id {0} with product id {1}", message.UserId, message.ProductId);

            var preparePackage = new PreparePackageCmd
            {
                OrderId = Data.OrderId,
                ProductId = message.ProductId
            };

            Bus.Send(preparePackage);

            RequestTimeout<VerifyPackagePreparationExpiredTimeout>(TimeSpan.FromDays(_maxDaysForPackagePreparation));
        }

        public void Handle(PackagePrepared message)
        {
            Data.Status = (int) Enums.OrderStatuses.PackagePrepared;

            var sendPackage = new SendPackageCmd
            {
                OrderId = message.OrderId,
                PackageId = message.PackageId,
                ShippingTypeId = Data.ShippingTypeId,
                Size = message.Size,
                UserId = this.Data.UserId
            };
            Bus.Send(sendPackage);

        }

        public void Handle(OutOfStock message)
        {
            Data.Status = (int) Enums.OrderStatuses.OutOfStock;
            MailProvider.SendMessageTo(Data.UserId);
        }

        public void Handle(PackageSent message)
        {
            Data.Status = (int)Enums.OrderStatuses.Complete;

            var saleCompleted = new SaleCompleted
            {
                UserId = Data.UserId,
                ProductId = Data.ProductId,
                Date = DateTime.UtcNow,
            };
            Bus.Publish(saleCompleted);
            MarkAsComplete();
        }

        public void Timeout(VerifyPackagePreparationExpiredTimeout state)
        {
            //Notify user about problem
            MailProvider.SendMessageTo(Data.UserId);
            MarkAsComplete();
        }
    }

    public class OrderPolicyData : ContainSagaData
    {
        public virtual int ProductId { get; set; }
        public virtual int ShippingTypeId { get; set; }
        public virtual Guid OrderId { get; set; }
        public virtual int Status { get; set; }
        public virtual int UserId { get; set; }
    }

    public class VerifyPackagePreparationExpiredTimeout
    {
    }
}

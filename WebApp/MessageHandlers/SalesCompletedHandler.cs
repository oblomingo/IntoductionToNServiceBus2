using NServiceBus;
using Sales.Messages.Events;
using WebApp.Controllers;

namespace WebApp.MessageHandlers
{
    public class PaymentAcceptedHandler : IHandleMessages<SaleCompleted>
    {
        public void Handle(SaleCompleted message)
        {
            OrderController.SaleCompletedQueue.Enqueue(message);
        }
    }
}
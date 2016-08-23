using NServiceBus;
using NServiceBus.Logging;
using Sales.Messages.Events;

namespace Marketing.BusinessIntelligence.Application
{
    public class BusinessIntelligenceService : IHandleMessages<SaleCompleted>
    {
        private readonly ILog _log;
        public BusinessIntelligenceService()
        {
            _log = LogManager.GetLogger(typeof(BusinessIntelligenceService));
        }

        public void Handle(SaleCompleted message)
        {
            _log.InfoFormat("Sale data added to business intelligence module. Product Id {0} to user Id {1} {2}", message.ProductId, message.UserId, message.Date.ToLongDateString());
        }
    }
}

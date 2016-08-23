
using NServiceBus;

namespace Marketing.BusinessIntelligence.Infrastructure
{

    [EndpointName("Marketing.BusinessIntelligence")]
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.EnableInstallers();
        }
    }
}

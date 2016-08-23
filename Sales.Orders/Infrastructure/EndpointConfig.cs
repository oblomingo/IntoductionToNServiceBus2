
using NServiceBus;

namespace Sales.Orders.Infrastructure
{
    [EndpointName("Sales.Orders")]
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.EnableInstallers();
        }
    }
}

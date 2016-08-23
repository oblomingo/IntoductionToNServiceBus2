
using NServiceBus;

namespace Logistic.Deliveries.Infrastructure
{
	[EndpointName("Logistic.Deliveries")]
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		public void Customize(BusConfiguration configuration)
		{
			configuration.UsePersistence<InMemoryPersistence>();
			configuration.EnableInstallers();
		}
	}
}

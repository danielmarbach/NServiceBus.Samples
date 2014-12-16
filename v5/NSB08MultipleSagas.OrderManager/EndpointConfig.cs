
namespace NSB08MultipleSagas.OrderManager
{
    using NServiceBus;
	using System.Linq;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
			configuration.EndpointName( this.GetType().Namespace.Split( '.' ).Last() );
			configuration.UsePersistence<InMemoryPersistence>();

			configuration.Conventions()
				.DefiningMessagesAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Messages" ) )
				.DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
				.DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );
        }
    }
}

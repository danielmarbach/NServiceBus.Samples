
namespace NSB08MultipleSagas.OrderFrontEnd
{
	using NSB08MultipleSagas.Messages.Events;
	using NServiceBus;
	using System;
	using System.Linq;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
			configuration.EndpointName( this.GetType().Namespace.Split('.').Last() );
			configuration.UsePersistence<InMemoryPersistence>();

			configuration.Conventions()
				.DefiningMessagesAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Messages" ) )
				.DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
				.DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );
        }
    }

	class Startup : IWantToRunWhenBusStartsAndStops
	{
		public IBus Bus { get; set; }

		
		public void Start()
		{
			Console.WriteLine("Press any key to place a new order");
			Console.Read();

			this.Bus.Publish<ICheckoutRequested>( e => e.ShoppingCartId = Guid.NewGuid().ToString() );

			Console.WriteLine("Order request published...");

			this.Start();
		}

		public void Stop()
		{
			
		}
	}
}

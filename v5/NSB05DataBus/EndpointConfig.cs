
namespace NSB05DataBus
{
	using NSB05DataBus.Commands;
	using NServiceBus;
	using NServiceBus.DataBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
	{
		public void Customize( BusConfiguration configuration )
		{
			configuration.UsePersistence<InMemoryPersistence>();

			configuration.Conventions()
				.DefiningDataBusPropertiesAs( pi => pi.Name.StartsWith( "Large" ) )
				.DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
				.DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );

			//configuration.UseDataBus<FileShareDataBus>();

			//configuration.RegisterComponents( c =>
			//{
			//	c.RegisterSingleton<IDataBus>( new CustomDataBus() );
			//} );

			configuration.UseDataBus( typeof( CustomDataBus ) );
		}
	}

	class Startup : IWantToRunWhenBusStartsAndStops
	{
		public IBus Bus { get; set; }

		public void Start()
		{
			var message = new MessageWithLargePayload
			{
				SomeProperty = "This message contains a large blob that will be sent on the data bus",
				LargeBlob = new byte[ 1024 * 1024 * 5 ] //5MB
			};

			Bus.SendLocal( message );
		}

		public void Stop()
		{

		}
	}

}

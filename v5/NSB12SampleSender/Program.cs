using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB12SampleSender
{
	class Program
	{
		static void Main( string[] args )
		{
			var cfg = new BusConfiguration();

			cfg.UsePersistence<InMemoryPersistence>();
			cfg.Conventions()
				.DefiningMessagesAs( t => t.Namespace != null && t.Namespace.EndsWith( "Messages" ) );

			using( var bus = Bus.Create( cfg ).Start() )
			{
				bus.Send( new NSB12SampleMessages.MyMessage() 
				{ 
					Content = "This is the message content" 
				} );

				Console.Read();
			}
		}
	}
}

using NSB13SampleMessages.Events;
using System;
using Topics.Radical;

namespace NSB13SampleSubscriber
{
	class SomethingHappenedHandler : NServiceBus.IHandleMessages<ISomethingHappened>
	{
		public void Handle( ISomethingHappened message )
		{
			using( ConsoleColor.Cyan.AsForegroundColor() )
			{
				Console.WriteLine( "Event received, data: {0}", message.Data );
			}
		}
	}
}

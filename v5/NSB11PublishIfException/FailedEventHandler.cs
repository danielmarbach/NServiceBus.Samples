using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB11PublishIfException
{
	class FailedEventHandler: NServiceBus.IHandleMessages<Messages.FailedEvent>
	{
		public void Handle( Messages.FailedEvent message )
		{
			Console.WriteLine( "Messages.FailedEvent" );
		}
	}
}

using NSB05DataBus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical;

namespace NSB05DataBus.Handlers
{
	class MessageWithLargePayloadHandler : NServiceBus.IHandleMessages<MessageWithLargePayload>
	{
		public void Handle( MessageWithLargePayload message )
		{
			using( ConsoleColor.Green.AsForegroundColor() ) 
			{
				Console.WriteLine( "Handled" );
			}
		}
	}
}

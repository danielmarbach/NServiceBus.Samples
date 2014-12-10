using NSB06Retries.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSB06Retries.Receiver
{
	class SampleCommandHandler : NServiceBus.IHandleMessages<SampleCommand>
	{
		public void Handle( SampleCommand message )
		{
			//var timeout = 75;
			//var slept = 0;
			//while( slept < timeout )
			//{
			//	Thread.Sleep( 1000 );
			//	slept++;
			//}

			throw new ArgumentException( "Something went wrong." );
		}
	}
}

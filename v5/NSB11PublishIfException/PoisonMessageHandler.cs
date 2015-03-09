using NSB11PublishIfException.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NSB11PublishIfException
{
	class PoisonMessageHandler : NServiceBus.IHandleMessages<PoisonMessage>
	{
		public IBus Bus { get; set; }

		public void Handle( PoisonMessage message )
		{
			try
			{
				throw new NotImplementedException();
			}
			catch 
			{
				using( var tx = new TransactionScope( TransactionScopeOption.RequiresNew ) ) 
				{
					/*
					 * Since NServiceBus retry logic will kick in on "throw"
					 * each time the message is retried an event will be published.
					 * The way to go is via ServiceControl events:
					 * http://t.co/FCuGTsEQGY
					 */
					this.Bus.Publish<Messages.FailedEvent>( e => { } );

					tx.Complete();
				}
 
				throw;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSB14WarehouseService.Messages.Commands;
using NSB14WarehouseService.Messages.Events;
using NServiceBus;
using Topics.Radical;

namespace NSB14WarehouseService
{
	class CollectItemsHandler : IHandleMessages<CollectItems>
	{
		public IBus Bus { get; set; }

		public void Handle( CollectItems message )
		{
			using( ConsoleColor.Magenta.AsForegroundColor() )
			{
				Console.WriteLine( "Collect items request for cart: {0}", message.CartId );

				this.Bus.Publish<IItemsCollected>( e =>
				{
					e.OrderId = message.OrderId;
				} );

				Console.WriteLine( "Items collected." );
			}
		}
	}
}

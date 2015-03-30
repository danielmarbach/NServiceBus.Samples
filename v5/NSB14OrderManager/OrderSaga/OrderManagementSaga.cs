﻿using NServiceBus;
using NServiceBus.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSB14Customer.Messages.Events;
using NSB14OrderManager.Messages.Timeouts;
using NSB14OrderManager.Messages.Events;
using NSB14Common;
using NSB14WarehouseService.Messages.Commands;
using NSB14WarehouseService.Messages.Events;
using NSB14ShippingService.Messages.Commands;
using NSB14ShippingService.Messages.Events;
using Topics.Radical;

namespace NSB14OrderManager.OrderSaga
{
	public class OrderManagementSaga
		: Saga<OrderManagementSagaData>,
		IAmStartedByMessages<IShoppingCartCheckedout>,
		IHandleMessages<IItemsCollected>,
		IHandleTimeouts<ItemCollectionTimeout>,
		IHandleMessages<IOrderShipped>
	{
		protected override void ConfigureHowToFindSaga( SagaPropertyMapper<OrderManagementSagaData> mapper )
		{
			mapper.ConfigureMapping<IItemsCollected>( m => m.OrderId ).ToSaga( d => d.OrderId );
			mapper.ConfigureMapping<IOrderShipped>( m => m.OrderId ).ToSaga( d => d.OrderId );

			mapper.ConfigureMapping<ItemCollectionTimeout>( m => m.OrderId ).ToSaga( d => d.OrderId );
		}

		public void Handle( IShoppingCartCheckedout message )
		{
			using ( ConsoleColor.Green.AsForegroundColor() )
			{
				this.Data.OrderId = Guid.NewGuid().ToString();
				this.Data.CartId = message.CartId;

				Console.WriteLine( "Ready to create a new order: ID: {0}, Cart ID: {1}", this.Data.OrderId, this.Data.CartId );

				this.Bus.Send( new CollectItems()
				{
					OrderId = this.Data.OrderId,
					CartId = this.Data.CartId
				} );

				Console.WriteLine( "Collect items request sent..." );

				this.RequestTimeout( TimeSpan.FromSeconds( 10 ), new ItemCollectionTimeout()
				{
					OrderId = this.Data.OrderId
				} );

				Console.WriteLine( "Waiting for the warehouse service..." );

				this.Bus.Publish<IOrderCreated>( e => e.OrderId = this.Data.OrderId );

				Console.WriteLine( "Order created." );
			}
		}

		public void Handle( IItemsCollected message )
		{
			using ( ConsoleColor.Green.AsForegroundColor() )
			{
				this.Data.CollectionCompleted = true;
				Console.WriteLine( "Item collection completed." );

				this.Bus.Send( new ShipOrder()
				{
					OrderId = this.Data.OrderId
				} );

				Console.WriteLine( "Ship request sent." );
			}
		}

		public void Timeout( ItemCollectionTimeout state )
		{
			if ( !this.Data.CollectionCompleted )
			{
				using ( ConsoleColor.DarkYellow.AsForegroundColor() )
				{
					Console.WriteLine( "Item collection is late, notifying the world." );
				}

				this.Bus.Publish<IOrderProcessingDelayed>( e =>
				{
					e.OrderId = this.Data.OrderId;
					e.Reason = ProcessingDelayReason.SlowItemCollection;
				} );
			}
			else
			{
				using ( ConsoleColor.Green.AsForegroundColor() )
				{
					Console.WriteLine( "Item collection completed on time." );
				}
			}
		}

		public void Handle( IOrderShipped message )
		{
			using ( ConsoleColor.Green.AsForegroundColor() )
			{
				Console.WriteLine( "Well done...order shipped." );
			}

			this.MarkAsComplete();
		}
	}
}

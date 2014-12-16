using NSB08MultipleSagas.Messages;
using NSB08MultipleSagas.Messages.Commands;
using NSB08MultipleSagas.Messages.Events;
using NServiceBus;
using NServiceBus.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.DeliveryManager
{
	public class DeliverySaga : Saga<DeliverySaga.Snapshop>,
		IAmStartedByMessages<IOrderReadyForDelivery>,
		IHandleTimeouts<OrderDeliveryCompletedTimeout>,
		IHandleMessages<DeliveAddressChangeResponse>
	{
		protected override void ConfigureHowToFindSaga( SagaPropertyMapper<Snapshop> mapper )
		{
			mapper.ConfigureMapping<DeliveAddressChangeResponse>( m => m.DeliveryId ).ToSaga( s => s.DeliveryId );
		}

		public class Snapshop : ContainSagaData
		{
			[Unique]
			public String OrderId { get; set; }

			[Unique]
			public String DeliveryId { get; set; }

			public bool Delivered { get; set; }
		}

		public void Handle( IOrderReadyForDelivery message )
		{
			//tries to deliver the order
			//wait for delivery completition
			this.Data.OrderId = message.OrderId;
			this.Data.DeliveryId = Guid.NewGuid().ToString();
			this.Data.Delivered = false;

			this.RequestTimeout( TimeSpan.FromSeconds( 5 ), new OrderDeliveryCompletedTimeout() );		
		}

		public void Timeout( OrderDeliveryCompletedTimeout state )
		{
			if( !this.Data.Delivered )
			{
				//assume the address is wrong
				this.Bus.SendLocal( new ChangeDeliveryAddress() 
				{
					OrderId = this.Data.OrderId,
					DeliveryId =  this.Data.DeliveryId
				} );
			}
		}

		public void Handle( DeliveAddressChangeResponse message )
		{
			//delivery
			this.Bus.Publish<IOrderDelivered>( e => e.OrderId = this.Data.OrderId );
			this.MarkAsComplete();
		}
	}

	public class OrderDeliveryCompletedTimeout { }
}

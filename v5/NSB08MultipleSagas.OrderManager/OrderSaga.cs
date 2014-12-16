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

namespace NSB08MultipleSagas.OrderManager
{
	public class OrderSaga : Saga<OrderSaga.Snapshop>,
		IAmStartedByMessages<ICheckoutRequested>,
		IHandleMessages<IOrderDelivered>,
		IHandleMessages<OrderPaymentResponse>,
		IHandleTimeouts<OrderPaymentRequestTimeout>
	{
		protected override void ConfigureHowToFindSaga( SagaPropertyMapper<Snapshop> mapper )
		{
			mapper.ConfigureMapping<IOrderDelivered>( message => message.OrderId ).ToSaga( snapshot => snapshot.OrderId );
			mapper.ConfigureMapping<OrderPaymentResponse>( message => message.OrderId ).ToSaga( snapshot => snapshot.OrderId );
		}

		public class Snapshop : ContainSagaData
		{
			[Unique]
			public string ShoppingCartId { get; set; }

			[Unique]
			public string OrderId { get; set; }

			public bool Payed { get; set; }
		}

		public void Handle( ICheckoutRequested message )
		{
			this.Data.ShoppingCartId = message.ShoppingCartId;
			this.Data.OrderId = Guid.NewGuid().ToString();

			this.Bus.Send( new SubmitOrderPaymentRequest()
			{
				OrderId = this.Data.OrderId
			} );

			this.RequestTimeout( TimeSpan.FromSeconds( 15 ), new OrderPaymentRequestTimeout() );
		}

		public void Handle( OrderPaymentResponse message )
		{
			if( message.Payed )
			{
				this.Data.Payed = true;
				this.Bus.Publish<IOrderReadyForDelivery>( e => e.OrderId = this.Data.OrderId );
			}
			else 
			{
				this.NotifyFailedAndComplete();
			}
		}

		public void Timeout( OrderPaymentRequestTimeout state )
		{
			if( !this.Data.Payed )
			{
				this.NotifyFailedAndComplete();
			}
		}

		void NotifyFailedAndComplete() 
		{
			this.ReplyToOriginator( new OrderCencelledResponse()
			{
				OrderId = this.Data.OrderId,
				ShoppingCartId = this.Data.ShoppingCartId
			} );

			this.MarkAsComplete();
		}

		//public void Handle( IOrderDeliveryFailed message )
		//{
		//	this.NotifyFailedAndComplete();
		//}

		public void Handle( IOrderDelivered message )
		{
			this.ReplyToOriginator( new OrderdDeliveredResponse()
			{
				OrderId = this.Data.OrderId,
				ShoppingCartId = this.Data.ShoppingCartId
			} );

			this.MarkAsComplete();
		}
	}

	public class OrderPaymentRequestTimeout { }
}

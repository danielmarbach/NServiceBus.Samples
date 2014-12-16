using NSB08MultipleSagas.Messages;
using NSB08MultipleSagas.Messages.Commands;
using NServiceBus;
using NServiceBus.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.DeliveryManager
{
	public class DeliveryAddressChangeSaga: Saga<DeliveryAddressChangeSaga.Snapshop>,
		IAmStartedByMessages<ChangeDeliveryAddress>,
		IHandleMessages<AddressChangeUserRequest>
	{
		protected override void ConfigureHowToFindSaga( SagaPropertyMapper<Snapshop> mapper )
		{
			mapper.ConfigureMapping<AddressChangeUserRequest>( m => m.DeliveryId ).ToSaga( s => s.DeliveryId );
		}

		public class Snapshop : ContainSagaData
		{
			[Unique]
			public String DeliveryId { get; set; }

			[Unique]
			public string OrderId { get; set; }
		}

		public void Handle( ChangeDeliveryAddress message )
		{
			this.Data.DeliveryId = message.DeliveryId;
			this.Data.OrderId = message.OrderId;

			//sends out an email to the user asking for a change in the address
			this.Bus.SendLocal( new AddressChangeUserRequest()
			{
				DeliveryId = this.Data.DeliveryId
			} );
		}

		public void Handle( AddressChangeUserRequest message )
		{
			this.ReplyToOriginator( new DeliveAddressChangeResponse() { DeliveryId = this.Data.DeliveryId } );
			this.MarkAsComplete();
		}
	}
}

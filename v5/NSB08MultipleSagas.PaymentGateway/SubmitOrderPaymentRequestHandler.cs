using NSB08MultipleSagas.Messages;
using NSB08MultipleSagas.Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.PaymentGateway
{
	public class SubmitOrderPaymentRequestHandler : IHandleMessages<SubmitOrderPaymentRequest>
	{
		public IBus Bus { get; set; }

		public void Handle( SubmitOrderPaymentRequest message )
		{
			this.Bus.Reply( new OrderPaymentResponse()
			{
				OrderId = message.OrderId,
				Payed = true
			} );
		}
	}
}

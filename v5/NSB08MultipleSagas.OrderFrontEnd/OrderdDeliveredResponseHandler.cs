using NSB08MultipleSagas.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.OrderFrontEnd
{
	class OrderdDeliveredResponseHandler : NServiceBus.IHandleMessages<OrderdDeliveredResponse>
	{
		public void Handle( OrderdDeliveredResponse message )
		{
			
		}
	}
}

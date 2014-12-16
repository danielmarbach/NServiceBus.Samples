using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.Messages.Commands
{
	public class ChangeDeliveryAddress
	{
		public string DeliveryId { get; set; }

		public string OrderId { get; set; }
	}
}

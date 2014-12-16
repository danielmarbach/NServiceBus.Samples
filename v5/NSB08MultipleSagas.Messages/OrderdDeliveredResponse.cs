using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.Messages
{
	public class OrderdDeliveredResponse
	{
		public String OrderId { get; set; }

		public String ShoppingCartId{ get; set; }
	}
}

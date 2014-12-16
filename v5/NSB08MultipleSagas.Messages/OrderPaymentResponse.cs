using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.Messages
{
	public class OrderPaymentResponse
	{
		public String OrderId { get; set; }
		public Boolean Payed { get; set; }
	}
}

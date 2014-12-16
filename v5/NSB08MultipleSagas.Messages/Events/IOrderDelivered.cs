using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB08MultipleSagas.Messages.Events
{
	public interface IOrderDelivered
	{
		String OrderId { get; set; }
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB14ShippingService.Messages.Events
{
	public interface IOrderShipped
	{
		String OrderId { get; set; }
	}
}

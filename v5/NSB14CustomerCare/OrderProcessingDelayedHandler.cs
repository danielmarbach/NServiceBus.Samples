﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSB14OrderManager.Messages.Events;
using Topics.Radical;

namespace NSB14CustomerCare
{
	class OrderProcessingDelayedHandler : NServiceBus.IHandleMessages<IOrderProcessingDelayed>
	{
		public void Handle( IOrderProcessingDelayed message )
		{
			using ( ConsoleColor.DarkYellow.AsForegroundColor() ) 
			{
				Console.WriteLine("We should send a gift to the customer...the order has been dalyed.");
			}
		}
	}
}

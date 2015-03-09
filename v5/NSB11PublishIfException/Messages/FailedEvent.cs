using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB11PublishIfException.Messages
{
	public interface FailedEvent : IEvent
	{
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSB14Common;

namespace NSB14OrderManager.Messages.Events
{
    public interface IOrderProcessingDelayed
    {

        String OrderId { get; set; }

        ProcessingDelayReason Reason { get; set; }
    }
}

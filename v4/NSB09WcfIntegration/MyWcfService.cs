using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSB09WcfIntegration.Messages;
using NSB09WcfIntegration.Messages.Commands;

namespace NSB09WcfIntegration
{
    public class MyWcfService : NServiceBus.WcfService<MyCommand, MyResponse>
    {
    }
}

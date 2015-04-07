using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NSB17WebApiInjection.Controllers
{
    public class SampleController : ApiController
    {
		IBus bus;

		public SampleController( IBus bus)
		{
			this.bus = bus;
		}

		public string Get() 
		{
			return "Hi, there";
		}
    }
}

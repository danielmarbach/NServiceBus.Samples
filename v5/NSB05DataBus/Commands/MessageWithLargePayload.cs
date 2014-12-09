using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB05DataBus.Commands
{
	public class MessageWithLargePayload
	{
		public string SomeProperty { get; set; }
		public byte[] LargeBlob { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB13SampleMessages.Events
{
	public interface ISomethingHappened
	{
		String Data { get; set; }
	}
}

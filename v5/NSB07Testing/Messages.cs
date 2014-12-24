using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB07Testing.Messages
{
	public class AppTypeRequest
	{
		public int AppId { get; set; }
	}


	public class AppTypeResponse
	{
		public int AppId { get; set; }
		public AppType AppType { get; set; }
	}

	public enum AppType
	{
		Known,
		Unknown
	}

	public class SetAppType
	{
		public int AppId { get; set; }
	}

}

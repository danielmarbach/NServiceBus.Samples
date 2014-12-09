using NServiceBus.DataBus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB05DataBus
{
	class CustomDataBus : IDataBus
	{
		public Stream Get( string key )
		{
			return File.OpenRead( "blob.dat" );
		}

		public string Put( Stream stream, TimeSpan timeToBeReceived )
		{
			using( var destination = File.OpenWrite( "blob.dat" ) )
			{
				stream.CopyTo( destination );
			}
			return "the-key-of-the-stored-file-such-as-the-full-path";
		}

		public void Start()
		{

		}
	}
}

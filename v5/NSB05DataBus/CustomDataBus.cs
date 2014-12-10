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
		/// <summary>
		/// Gets a data item from the bus.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>
		/// The data <see cref="T:System.IO.Stream" />.
		/// </returns>
		public Stream Get( string key )
		{
			return File.OpenRead( "blob.dat" );
		}

		/// <summary>
		/// Adds a data item to the bus and returns the assigned key.
		/// </summary>
		/// <param name="stream">A create containing the data to be sent on the databus.</param>
		/// <param name="timeToBeReceived">The time to be received specified on the message type. TimeSpan.MaxValue is the default.</param>
		/// <returns></returns>
		public string Put( Stream stream, TimeSpan timeToBeReceived )
		{
			using( var destination = File.OpenWrite( "blob.dat" ) )
			{
				stream.CopyTo( destination );
			}
			return "the-key-of-the-stored-file-such-as-the-full-path";
		}

		/// <summary>
		/// Called when the bus starts up to allow the data bus to active background tasks.
		/// </summary>
		public void Start()
		{

		}
	}
}

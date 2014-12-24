using NServiceBus;
using ServiceControl.Plugin.CustomChecks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB09CustomChecks
{
	class Program
	{
		static void Main( string[] args )
		{
			var cfg = new BusConfiguration();

			cfg.UsePersistence<InMemoryPersistence>();
			cfg.Conventions()
				.DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) );

			//using( var bus = Bus.CreateSendOnly( cfg ) )
			//{
			//	Console.Read();
			//}

			using( var bus = Bus.Create( cfg ).Start() )
			{
				Console.Read();
			}
		}
	}

	class MyCheck : PeriodicCheck
	{
		public MyCheck()
			: base( "Directory check", "My Server", TimeSpan.FromSeconds( 5 ) ) { }

		public override CheckResult PerformCheck()
		{
			var dir = @"C:\Foo";
			if( !Directory.Exists( dir ) )
			{
				return CheckResult.Failed( string.Format( "Storage directory '{0}' does not exist", dir ) );
			}
			return CheckResult.Pass;
		}
	}
}

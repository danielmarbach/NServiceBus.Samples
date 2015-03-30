
using NServiceBus;
using NServiceBus.Persistence;
using Raven.Client.Embedded;
using System;
namespace NSB14ShippingService
{
	class Program
	{
		static void Main( string[] args )
		{
			var cfg = new BusConfiguration();
			cfg.EnableInstallers();

			var embeddedSore = new EmbeddableDocumentStore
			{
				ResourceManagerId = new Guid( "{1FD7BDB9-A219-4A90-AAB3-B15F050A86A8}" ),
				DataDirectory = @"~\RavenDB\Data"
			}.Initialize();

			cfg.UsePersistence<RavenDBPersistence>()
				.DoNotSetupDatabasePermissions()
				.SetDefaultDocumentStore( embeddedSore );

			cfg.Conventions()
				.DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
				.DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );

			using( var bus = Bus.Create( cfg ).Start() )
			{
				Console.Read();
			}
		}
	}
}

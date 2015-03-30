
using NServiceBus;
using NServiceBus.Persistence;
using Raven.Client.Embedded;
using System;

namespace NSB14WarehouseService
{
	class Program
	{
		static void Main( string[] args )
		{
			var cfg = new BusConfiguration();
			cfg.EnableInstallers();

			var embeddedSore = new EmbeddableDocumentStore
			{
				ResourceManagerId = new Guid( "d5723e19-92ad-4531-adad-8611e6e05c8a" ),
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

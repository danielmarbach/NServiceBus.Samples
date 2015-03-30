using NServiceBus;
using NServiceBus.Persistence;
using Raven.Client.Embedded;
using System;

namespace NSB14CustomerCare
{
	class Program
	{
		static void Main( string[] args )
		{
			var cfg = new BusConfiguration();

			var embeddedSore = new EmbeddableDocumentStore
			{
				ResourceManagerId = new Guid( "{6B8BF798-24D2-402E-AED2-0F3D801571A0}" ),
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

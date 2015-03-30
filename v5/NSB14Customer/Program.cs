using Topics.Radical;
using NServiceBus;
using NServiceBus.Persistence;
using Raven.Client.Embedded;
using System;
using NSB14Customer.Messages.Events;

namespace NSB14Customer
{
	class Program
	{
		static void Main( string[] args )
		{
			var cfg = new BusConfiguration();

			var embeddedSore = new EmbeddableDocumentStore
			{
				ResourceManagerId = new Guid( "{B9EC41C8-6DAB-4EF2-9805-9181F0A8B208}" ),
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
				using( ConsoleColor.Red.AsForegroundColor() )
				{
					Console.WriteLine( "Isn't this an amazing web site? :-D" );
				}

				using( ConsoleColor.Cyan.AsForegroundColor() )
				{
					Console.WriteLine( "Publishing IShoppingCartCheckedout..." );

					bus.Publish<IShoppingCartCheckedout>( e =>
					{
						e.CartId = Guid.NewGuid().ToString();
					} );

					Console.WriteLine( "IShoppingCartCheckedout published." );
				}

				Console.Read();
			}
		}
	}
}

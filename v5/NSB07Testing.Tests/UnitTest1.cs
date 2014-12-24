using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceBus.Testing;

namespace NSB07Testing.Tests
{
	[TestClass]
	public class AppTypePolicyTests
	{
		[TestMethod]
		public void RequestWithNoSetEventShouldReturnUnknown()
		{
			var request = new Messages.AppTypeRequest();
			request.AppId = 111;

			Test.Initialize();
			Test.Saga<Sagas.AppTypePolicy>()
				.ExpectReply<Messages.AppTypeResponse>( r => r.AppType == Messages.AppType.Unknown )
				.When( r => r.Handle( request ) );
		}

		[TestMethod]
		public void RequestShouldReturnKnownIfApplicationReceivedEventReceivedFirst()
		{
			var request = new Messages.AppTypeRequest();
			request.AppId = 111;

			var appReceivedEvent = new Messages.SetAppType();
			appReceivedEvent.AppId = 111;

			Test.Initialize();
			Test.Saga<Sagas.AppTypePolicy>()
				.When( r => r.Handle( appReceivedEvent ) )
				.ExpectReply<Messages.AppTypeResponse>( r => r.AppType == Messages.AppType.Known )
				.When( r => r.Handle( request ) );
		}


		[TestMethod]
		public void RequestShouldReturnUnknownAsSetAndRequestEventAppIdsDoNotMatch()
		{
			var request = new Messages.AppTypeRequest();
			request.AppId = 111;

			var appReceivedEvent = new Messages.SetAppType();
			appReceivedEvent.AppId = 222;

			Test.Initialize();
			Test.Saga<Sagas.AppTypePolicy>()
				.When( r => r.Handle( appReceivedEvent ) )
				.ExpectReply<Messages.AppTypeResponse>( r => r.AppType == Messages.AppType.Unknown )
				.When( r => r.Handle( request ) );

		}
	}

}

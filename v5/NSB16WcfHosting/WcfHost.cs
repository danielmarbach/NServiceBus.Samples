using NSB16WcfHosting;
using NServiceBus;
using System;
using System.ServiceModel;

class WcfHost : IWantToRunWhenBusStartsAndStops
{
	ServiceHost host = null;

	public void Start()
	{
		this.host = new ServiceHost( typeof( MySampleService ) );
		this.host.Open();

		Console.WriteLine("WCF Host started...");
	}

	public void Stop()
	{
		if(this.host != null && this.host.State == CommunicationState.Opened)
		{
			this.host.Close();
		}

		if( this.host != null ) 
		{
			( ( IDisposable )this.host ).Dispose();
			this.host = null;
		}

		Console.WriteLine( "WCF Host stopped..." );
	}
}
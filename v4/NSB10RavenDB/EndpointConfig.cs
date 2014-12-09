
namespace NSB10RavenDB
{
    using NServiceBus;
	using NServiceBus.RavenDB;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {

	}

	class CustomInitialization : INeedInitialization
	{
		public void Init()
		{
			//INeedInitialization cannot be implemented on the EndpointConfig class
			//Called after Configure.With() is completed and a container has been set.
			Configure.Instance.RavenDBStorage();
		}
	}
}

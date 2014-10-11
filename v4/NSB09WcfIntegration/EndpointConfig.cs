
namespace NSB09WcfIntegration
{
    using NServiceBus;

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
            var embeddedSore = new Raven.Client.Embedded.EmbeddableDocumentStore
            {
                DataDirectory = @"~\..\RavenDB\Data"
            }.Initialize();

            Configure.Instance.DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) );
            Configure.Instance.DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );
            Configure.Instance.RavenPersistenceWithStore( embeddedSore );

            var s = typeof(NServiceBus.IWcfService<NSB09WcfIntegration.Messages.Commands.MyCommand, NSB09WcfIntegration.Messages.MyResponse>).ToString();
        }
    }
}

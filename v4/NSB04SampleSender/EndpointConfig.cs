
namespace NSB04SampleSender
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
    }

    public class CustomInitialization : INeedInitialization
    {
        public void Init()
        {
            var embeddedSore = new Raven.Client.Embedded.EmbeddableDocumentStore
            {
                DataDirectory = @"~\..\RavenDB\Data"
            }.Initialize();

            Configure.Instance.DefiningMessagesAs(t => t.Namespace != null && t.Namespace == "NSB04SampleMessages");
            Configure.Instance.RavenPersistenceWithStore(embeddedSore);
        }
    }
}

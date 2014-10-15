
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB01SelfHost
{
    class Program
    {
        static void Main( string[] args )
        {
            var cfg = new BusConfiguration();

            //cfg.AutoSubscribe();
            //cfg.DefineCriticalErrorAction();
            //cfg.DisableDurableMessages();
            //cfg.DisableFeature();
            //cfg.DiscardFailedMessagesInsteadOfSendingToErrorQueue();
            //cfg.DoNotCreateQueues();
            //cfg.EnableCriticalTimePerformanceCounter();
            //cfg.EnableDurableMessages();
            //cfg.EnableFeature();
            //cfg.EnableInstallers();
            //cfg.EnableOutbox();
            //cfg.EndpointName();
            //cfg.EndpointVersion();
            //cfg.License();
            //cfg.LicensePath();
            //cfg.LoadMessageHandlers();
            //cfg.OverrideLocalAddress();
            //cfg.OverridePublicReturnAddress();
            //cfg.Pipeline;
            //cfg.PurgeOnStartup();
            //cfg.RegisterComponents();
            //cfg.ScaleOut();
            //cfg.ScanAssembliesInDirectory();
            //cfg.SecondLevelRetries();
            //cfg.Transactions();
            //cfg.UsePersistence();
            //cfg.UseSerialization();
            //cfg.UseTransport();

            cfg.UsePersistence<InMemoryPersistence>();
            cfg.Conventions()
                .DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
                .DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );

            using ( var bus = Bus.Create( cfg ).Start() )
            {
                Console.Read();
            }
        }
    }
}

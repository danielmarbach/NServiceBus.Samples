﻿using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using Common;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;

            var manager = NamespaceManager.Create();
			if ( !manager.QueueExists( "queue" ) )
			{
				manager.CreateQueue( "queue" );
			}

            var client = QueueClient.Create("queue");
            Console.WriteLine("SENDER");
            while (true)
            {
                var message = new BrokeredMessage(new Message());
                client.Send(message);
                Console.Write(".");

                Thread.Sleep(100);
            }
        }
    }


}
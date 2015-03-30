﻿using NSB12SampleMessages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Radical;

namespace NSB12SampleReceiver
{
    class MyMessageHandler : IHandleMessages<MyMessage>
    {
        public IBus Bus { get; set; }

        public void Handle(MyMessage message)
        {
            using (ConsoleColor.Cyan.AsForegroundColor())
            {
                Console.WriteLine("Sending MyReply to:  {0}", this.Bus.CurrentMessageContext.ReplyToAddress);

                var reply = new MyReply()
                {
                    Content = "How you doing?"
                };

                this.Bus.Reply(reply);

                Console.WriteLine("Reply sent.");
            }
        }
    }
}

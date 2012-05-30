﻿using System;
using Topshelf;

namespace Receiver
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(host =>
            {
                host.Service<Consumer>(service =>
                {
                    service.SetServiceName("Polling Consumer");
                    service.ConstructUsing(name => new Consumer(ConfigurationSettings.ChannelName));
                    service.WhenStarted(consumer => consumer.Start());
                    service.WhenContinued(consumer => consumer.Start());
                    service.WhenPaused(consumer => consumer.Pause());
                    service.WhenStopped(consumer => consumer.Stop());
                });
                host.RunAsLocalService();
                host.SetDisplayName("Simple Polling Message Consumer");
                host.SetDescription("A simple message consumer that polls for messages");
                host.SetServiceName("Simple.Polling.Consumer");
            });

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

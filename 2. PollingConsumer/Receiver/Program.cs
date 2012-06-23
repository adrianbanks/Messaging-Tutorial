using System;
using MessageUtilities;
using Topshelf;

namespace Receiver
{
    internal class Program
    {
        private static void Main()
        {
            ReceiverUtil.Delay();
            HostFactory.Run(host =>
                                {
                                    host.Service<Consumer>(service =>
                                                               {
                                                                   service.SetServiceName("Polling Consumer");
                                                                   service.ConstructUsing(name => new Consumer(ChannelConfiguration.Name));
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

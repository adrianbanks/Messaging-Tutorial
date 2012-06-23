using System;
using MessageUtilities;
using Topshelf;

namespace Receiver
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Broker");

            HostFactory.Run(host =>
                                {
                                    host.Service<MessageBroker>(service =>
                                                                    {
                                                                        service.SetServiceName("MessageBroker");
                                                                        service.ConstructUsing(name => new MessageBroker(ChannelConfiguration.Name));
                                                                        service.WhenStarted(consumer => consumer.Start());
                                                                        service.WhenContinued(consumer => consumer.Start());
                                                                        service.WhenPaused(consumer => consumer.Pause());
                                                                        service.WhenStopped(consumer => consumer.Stop());
                                                                    });

                                    host.RunAsLocalService();
                                    host.SetDisplayName("Simple Message Broker");
                                    host.SetDescription("A simple broker that connects producers to consumers");
                                    host.SetServiceName("Simple.MessageBroker");
                                });

            ConsolePause.PauseForInput();
        }
    }
}

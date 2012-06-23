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
            Console.WriteLine("Receiver");

            HostFactory.Run(host =>
                                {
                                    host.Service<Consumer>(service =>
                                                               {
                                                                   service.SetServiceName("Polling Consumer");
                                                                   Console.WriteLine("Topic: {0}", ConfigurationSettings.Topic);
                                                                   string channelName = string.Format("{0}_{1}", ChannelConfiguration.Name, ConfigurationSettings.Topic);
                                                                   service.ConstructUsing(name => new Consumer(channelName));
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

            ConsolePause.PauseForInput();
        }
    }
}

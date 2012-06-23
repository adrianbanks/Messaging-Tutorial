using System;
using MessageUtilities;

namespace Receiver
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Receiver");
            ReceiverUtil.Delay();

            Consumer consumer = new Consumer(ChannelConfiguration.Name);
            consumer.Consume();

            ConsolePause.PauseForInput();
        }
    }
}

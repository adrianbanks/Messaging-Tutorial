using System;
using MessageUtilities;

namespace Sender
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Sender");

            Producer producer = new Producer(ChannelConfiguration.Name);
            producer.Send("Hello World");

            ConsolePause.PauseForInput();
        }
    }
}

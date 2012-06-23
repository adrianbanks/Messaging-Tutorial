using System;
using MessageUtilities;

namespace Sender
{
    internal class Program
    {
        private static void Main()
        {
            Producer producer = new Producer(ChannelConfiguration.Name);

            while (true)
            {
                Console.Write("Enter message :> ");
                string message = Console.ReadLine();
                producer.Send(message);
            }
        }
    }
}

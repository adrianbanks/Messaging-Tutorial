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
            
            while (true)
            {
                string message = SenderUtil.GetInput();
                producer.Send(message);
            }
        }
    }
}

using System;
using System.Messaging;
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
                string topic = SenderUtil.GetInput("topic");
                string message = SenderUtil.GetInput("message");
                Message msg = new Message(message) {Extension = Convert.FromBase64String(topic)};
                producer.Send(msg);
            }
        }
    }
}

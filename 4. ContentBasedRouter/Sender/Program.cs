using System;
using System.Messaging;
using System.Text;
using MessageUtilities;

namespace Sender
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Sender");

            // Create an instance of the producer to send to the broker via the outbound channel
            Producer producer = new Producer(ChannelConfiguration.Name);

            while (true)
            {
                string topic = SenderUtil.GetInput("topic");
                string message = SenderUtil.GetInput("message");

                // Create a message from the input and set its Extension property from the topic 
                byte[] bytes = Encoding.Unicode.GetBytes(topic);
                Message mess = new Message(message) {Extension = bytes};

                // Send the message
                producer.Send(mess);
            }
        }
    }
}

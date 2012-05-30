using System;
using System.Collections.Generic;
using System.Messaging;
using MessageUtilities;
using System.Text;

namespace Receiver
{
    internal class MessageBroker 
    {
        private readonly MessageQueue inputChannel;
        private bool isRunning;
        private readonly IDictionary<string, MessageQueue>routingTable = new Dictionary<string, MessageQueue>();  

        public MessageBroker(string inputChannelName)
        {
            inputChannel = EnsureQueueExists(inputChannelName);

            inputChannel.MessageReadPropertyFilter.SetAll();

            // Build up the routing table. For any topic you want to send, create an output queue
            // For extra points, read this routing table from configuration
            //HINT: Note that the queue name here must match one provided by one of your recievers otherwise you won't see this working
            //HINT: In practice using configuration makes adding new consumers a config not code choise which reduces testing requirements
            Array.ForEach(ConfigurationSettings.Topics, t => routingTable.Add(t, EnsureQueueExists(inputChannelName + "_" + t)));
 
            inputChannel.ReceiveCompleted += Route;
        }

        public void Start()
        {
            isRunning = true;
            Receive();
            Console.WriteLine("Service started");
        }


        public void Pause()
        {
            isRunning = false;
            Console.WriteLine("Service paused");
        }

        public void Stop()
        {
            isRunning = false;
            inputChannel.Close();
            Console.WriteLine("Service stopped");
        }

        public MessageQueue EnsureQueueExists(string channelName)
        {
            var channel = !MessageQueue.Exists(channelName) ? MessageQueue.Create(channelName) : new MessageQueue(channelName);
            channel.Formatter = new XmlMessageFormatter(new[] {typeof (string)});
            return channel;
        }

        private void Route(object source, ReceiveCompletedEventArgs result)
        {
            try
            {
                var queue = (MessageQueue) source;
                var message = queue.EndReceive(result.AsyncResult);

                TraceMessage(message);

                var bytes = message.Extension;
                // read topic from the message Extension
                //HINT: Use Convert to change bytes to string
                string topic = Encoding.Unicode.GetString(bytes);

                // Look up the target queue for the topic
                MessageQueue topicQueue = null;

                if (routingTable.TryGetValue(topic, out topicQueue))
                {
                    // Send to the target queue
                    topicQueue.Send(message);
                }

            }
            catch (MessageQueueException mqe)
            {
                Console.WriteLine("{0} {1}", mqe.Message, mqe.MessageQueueErrorCode);
            }

            Receive();
        }

        private void Receive()
        {
            if (isRunning)
            {
                inputChannel.BeginReceive(new TimeSpan(0, 0, 0, ConfigurationSettings.PollingTimeout));
            }
        }

        private static void TraceMessage(Message message)
        {
            if (message != null)
            {
                message.TraceMessage();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Messaging;
using System.Text;
using MessageUtilities;

namespace Receiver
{
    internal class MessageBroker
    {
        private readonly MessageQueue inputChannel;
        private bool isRunning;
        private readonly IDictionary<string, MessageQueue> routingTable = new Dictionary<string, MessageQueue>();

        public MessageBroker(string inputChannelName)
        {
            inputChannel = EnsureQueueExists(inputChannelName);
            inputChannel.MessageReadPropertyFilter.SetAll();

            // Build up the routing table. For any topic you want to send, create an output queue
            Array.ForEach(ConfigurationSettings.Topics, t => routingTable.Add(t, EnsureQueueExists(inputChannelName + "_" + t)));
            inputChannel.ReceiveCompleted += Route;
        }

        private MessageQueue EnsureQueueExists(string channelName)
        {
            MessageQueue channel = !MessageQueue.Exists(channelName) ? MessageQueue.Create(channelName) : new MessageQueue(channelName);
            channel.Formatter = new XmlMessageFormatter(new[] {typeof(string)});
            return channel;
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

        private void Route(object source, ReceiveCompletedEventArgs result)
        {
            try
            {
                MessageQueue queue = (MessageQueue) source;
                Message message = queue.EndReceive(result.AsyncResult);

                TraceMessage(message);

                // read topic from the message Extension
                byte[] bytes = message.Extension;
                string topic = Encoding.Unicode.GetString(bytes);

                // Look up the target queue for the topic
                MessageQueue topicQueue;

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
                message.TraceReceivedMessage();
            }
        }
    }
}

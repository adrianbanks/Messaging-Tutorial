using System;
using System.Collections.Generic;
using System.Messaging;
using MessageUtilities;

namespace Receiver
{
    [Flags]
    internal enum Queues
    {
        None = 0,
        Input = 1,
        Control = 2
    }

    internal class MessageBroker
    {
        private readonly MessageQueue inputChannel;
        private bool isRunning;
        private readonly IDictionary<string, MessageQueue> routingTable = new Dictionary<string, MessageQueue>();

        public MessageBroker(string inputChannelName)
        {
            inputChannel = EnsureQueueExists(inputChannelName);
            inputChannel.MessageReadPropertyFilter.SetAll();
            inputChannel.ReceiveCompleted += Route;

            //TODO: Create a control channel to recieve routing information from subscribers 
            //HINT: Create a control channel in the Configuration Settings, pass into this method
            //TODO: Add a recieve completed event to add subscribers to call Subscribe (see below)
        }

        public void Start()
        {
            isRunning = true;
            Receive(Queues.Input | Queues.Control);
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
            MessageQueue channel = !MessageQueue.Exists(channelName) ? MessageQueue.Create(channelName) : new MessageQueue(channelName);
            channel.Formatter = new XmlMessageFormatter(new[] {typeof(string)});
            return channel;
        }

        private void Route(object source, ReceiveCompletedEventArgs result)
        {
            try
            {
                MessageQueue queue = (MessageQueue) source;
                Message message = queue.EndReceive(result.AsyncResult);

                TraceMessage(message);

                string topic = Convert.ToBase64String(message.Extension);
                Console.WriteLine("Message Topic is {0}", topic);

                MessageQueue targetQueue = routingTable[topic];
                targetQueue.Send(message);
            }
            catch (MessageQueueException mqe)
            {
                Console.WriteLine("{0} {1}", mqe.Message, mqe.MessageQueueErrorCode);
            }

            Receive(Queues.Input);
        }

        private void Receive(Queues queuesToListenOn)
        {
            //TODO: When we receive a message we need to subscribe to notifications from the queue again, this method lets us subscribe to one or both
            if (isRunning)
            {
                if (queuesToListenOn.HasFlag(Queues.Input))
                {
                    inputChannel.BeginReceive(new TimeSpan(0, 0, 0, ConfigurationSettings.PollingTimeout));
                }

                if (queuesToListenOn.HasFlag(Queues.Control))
                {
                    //TODO: subscribe to messages on the control queue
                }
            }
        }

        private void Subscribe(object source, ReceiveCompletedEventArgs result)
        {
            //TODO: Get the message off the control queue
            //TODO: parse the message (format is Topic:QueueName)
            //TODO: Create queue if does not exist or attach to queue
            //TODO: Add mapping to the routing table for topic and queue
            //TODO: Re-subscribe to the control queue
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

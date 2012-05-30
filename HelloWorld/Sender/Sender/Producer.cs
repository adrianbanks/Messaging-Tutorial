using System.Messaging;
using MessageUtilities;

namespace Sender
{
    class Producer
    {
        private MessageQueue channel;

        public Producer(string channelName)
        {
            EnsureQueueExists(channelName);
        }

        public void EnsureQueueExists(string channelName)
        {
            // If the channel does not exist create it, otherwise attach to it
            if (!MessageQueue.Exists(channelName))
            {
                channel = MessageQueue.Create(channelName);
            }
            else
            {
                channel = new MessageQueue(channelName);
            }
        }

        public void Send(string message)
        {
            var requestMessage = new Message {Body = message};

            // Send the message over the queue.
            channel.Send(requestMessage);
            requestMessage.TraceMessage();
        }

    }
}

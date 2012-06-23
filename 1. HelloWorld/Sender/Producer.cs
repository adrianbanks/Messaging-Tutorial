using System.Messaging;
using MessageUtilities;

namespace Sender
{
    internal class Producer
    {
        private MessageQueue channel;

        public Producer(string channelName)
        {
            EnsureQueueExists(channelName);
        }

        public void EnsureQueueExists(string channelName)
        {
            // If the channel does not exist create it, otherwise attach to it
            channel = MessageQueue.Exists(channelName)
                                  ? new MessageQueue(channelName)
                                  : MessageQueue.Create(channelName);
        }

        public void Send(string message)
        {
            Message requestMessage = new Message {Body = message};

            // Send the message over the queue.
            channel.Send(requestMessage);
            requestMessage.TraceSentMessage();
        }
    }
}

using System.Messaging;
using MessageUtilities;

namespace Sender
{
    internal class Producer
    {
        private readonly MessageQueue channel;

        public Producer(string channelName)
        {
            channel = !MessageQueue.Exists(channelName)
                                  ? MessageQueue.Create(channelName)
                                  : new MessageQueue(channelName);
        }

        public void Send(string message)
        {
            Message requestMessage = new Message {Body = message};
            channel.Send(requestMessage);
            requestMessage.TraceSentMessage();
        }
    }
}

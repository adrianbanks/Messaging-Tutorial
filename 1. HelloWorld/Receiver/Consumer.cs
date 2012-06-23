using System.Messaging;
using MessageUtilities;

namespace Receiver
{
    internal class Consumer
    {
        private readonly MessageQueue channel;

        public Consumer(string channelName)
        {
            // Attach to a message queue identified in channelName and set the formatter
            channel = new MessageQueue(channelName) {Formatter = new XmlMessageFormatter(new[] {typeof(string)})};

            // We want to trace message headers such as correlation id, so we need to tell MSMQ to retrieve those by setting the property filter
            MessagePropertyFilter filter = new MessagePropertyFilter();
            filter.SetAll();
            channel.MessageReadPropertyFilter = filter;
        }

        public void Consume()
        {
            // receive a message on the queue
            Message message = channel.Receive();

            // Trace the message out to the command line. HINT: Use the extension method.
            message.TraceReceivedMessage();
        }
    }
}

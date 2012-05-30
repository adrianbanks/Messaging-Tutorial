using System;
using System.Messaging;
using MessageUtilities;

namespace Receiver
{
    internal class Consumer
    {
        private readonly MessageQueue channel;

        public Consumer(string channelName)
        {
            // Attach to a message queue identified in channelName. 
            channel = new MessageQueue(channelName);
            
            // Set the formatter
            channel.Formatter = new XmlMessageFormatter(new[] {typeof(string)});
            
            // We want to trace message headers such as correlation id, so we need to tell MSMQ to retrieve those by setting the property filter
            var filter = new MessagePropertyFilter();
            filter.SetAll();
            channel.MessageReadPropertyFilter = filter;
        }

        public void Consume()
        {
            // recieve a message on the queue
            var message = channel.Receive();
            // Trace the message out to the command line. HINT: Use the extension method.
            message.TraceMessage();
        }
    }
}
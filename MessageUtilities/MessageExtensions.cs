using System;
using System.Messaging;

namespace MessageUtilities
{
    public static class MessageExtensions
    {
        public static void TraceSentMessage(this Message message)
        {
            Console.WriteLine("Sent request");
            TraceMessage(message);
        }

        public static void TraceReceivedMessage(this Message message)
        {
            Console.WriteLine("Received request");
            TraceMessage(message);
        }

        private static void TraceMessage(this Message message)
        {
            Console.WriteLine("\tTime:       {0}", DateTime.Now.ToString("HH:mm:ss.ffffff"));
            Console.WriteLine("\tMessage ID: {0}", message.Id);
            Console.WriteLine("\tCorrel. ID: {0}", message.CorrelationId);
            Console.WriteLine("\tContents:   {0}", message.Body);
        }
    }
}

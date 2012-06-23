using System;
using NDesk.Options;

namespace Sender
{
    internal class Program
    {
        //sender -c=hello_world
        private static void Main(string[] args)
        {
            string channel = string.Empty;
            OptionSet p = new OptionSet {{"c|channel=", "The name of the channel that we should send messages to", c => channel = c}};
            p.Parse(args);

            if (string.IsNullOrEmpty(channel))
            {
                Console.WriteLine("You must provide a channel name");
                return;
            }

            string channelName = string.Format(@".\private$\{0}", channel);

            Producer producer = new Producer(channelName);
            producer.Send("Hello World");

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}

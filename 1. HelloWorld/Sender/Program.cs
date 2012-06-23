using MessageUtilities;

namespace Sender
{
    internal class Program
    {
        private static void Main()
        {
            string channel = ChannelConfiguration.Name;
            string channelName = string.Format(@".\private$\{0}", channel);

            Producer producer = new Producer(channelName);
            producer.Send("Hello World");

            ConsolePause.PauseForInput();
        }
    }
}

using System.Threading;
using MessageUtilities;

namespace Receiver
{
    internal class Program
    {
        private static void Main()
        {
            Thread.Sleep(2000);
            string channel = ChannelConfiguration.Name;
            string channelName = string.Format(@".\private$\{0}", channel);

            Consumer consumer = new Consumer(channelName);
            consumer.Consume();

            ConsolePause.PauseForInput();
        }
    }
}

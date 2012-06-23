using MessageUtilities;

namespace Sender
{
    internal class Program
    {
        private static void Main()
        {
            Producer producer = new Producer(ChannelConfiguration.Name);
            producer.Send("Hello World");

            ConsolePause.PauseForInput();
        }
    }
}

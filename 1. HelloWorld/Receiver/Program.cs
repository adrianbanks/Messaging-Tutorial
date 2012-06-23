using MessageUtilities;

namespace Receiver
{
    internal class Program
    {
        private static void Main()
        {
            ReceiverUtil.Delay();

            Consumer consumer = new Consumer(ChannelConfiguration.Name);
            consumer.Consume();

            ConsolePause.PauseForInput();
        }
    }
}

using MessageUtilities;

namespace Sender
{
    internal class Program
    {
        private static void Main()
        {
            Producer producer = new Producer(ChannelConfiguration.Name);
            
            while (true)
            {
                string message = SenderUtil.GetMessage();
                producer.Send(message);
            }
        }
    }
}

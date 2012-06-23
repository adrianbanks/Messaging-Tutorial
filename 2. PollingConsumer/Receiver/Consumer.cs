using System;
using System.Messaging;
using System.Timers;
using MessageUtilities;

namespace Receiver
{
    internal class Consumer : IDisposable
    {
        private readonly MessageQueue channel;
        private readonly Timer timer;

        public Consumer(string channelName)
        {
            // Attach to a message queue and set the formatter so we can read the messages
            channel = new MessageQueue(channelName) {Formatter = new XmlMessageFormatter(new[] {typeof(string)})};

            // We want to trace message headers such as correlation id, so we need to tell MSMQ to retrieve those
            MessagePropertyFilter filter = new MessagePropertyFilter();
            filter.SetAll();
            channel.MessageReadPropertyFilter = filter;

            //we use a timer to poll the queue at a regular interval, of course this may need to be re-entrant but we have no state to worry about
            timer = new Timer(ConfigurationSettings.PollingInterval) {AutoReset = true};

            // on the Timer's Elapsed event we want to consume messages, so set the callback to our Consume method
            timer.Elapsed += timer_Elapsed;
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("Service started, will read queue every {0} ms", ConfigurationSettings.PollingInterval);
        }

        public void Pause()
        {
            timer.Stop();
            Console.WriteLine("Service paused");
        }

        public void Stop()
        {
            timer.Stop();
            timer.Close();

            // Shut the queue
            channel.Close();
            Console.WriteLine("Service stopped");
        }

        // A callback method for the elapsed event that receives a message from the queue
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Set a timeout on the receive call using the polling timeout configuration setting
                TimeSpan timeout = TimeSpan.FromSeconds((int) ConfigurationSettings.PollingTimeout);
                Message message = channel.Receive(timeout);
                message.TraceReceivedMessage();
            }
            catch (MessageQueueException mqe)
            {
                Console.WriteLine("{0} {1}", mqe.Message, mqe.MessageQueueErrorCode);
            }
        }

        public void Dispose()
        {
            timer.Close();
        }
    }
}

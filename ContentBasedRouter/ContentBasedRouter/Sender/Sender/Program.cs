using System;
using System.Messaging;
using NDesk.Options;
using System.Text;

namespace Sender
{
    class Program
    {
        //sender -m="my name is" -t=Greeting
        static void Main(string[] args)
        {
            var topic = "sdfgds";
            var message = "asdfgasdfgsadfg";
            var p = new OptionSet()
                        {
                            {"t|topic=", t => topic = t},
                            {"m|message=", m => message = m}
                        };
            p.Parse(args);

            if (CheckArguments(message, topic))
            {
                return;
            }

            // Create an instance of the producer to send to the broker via the outbound channel
            var producer = new Producer(ConfigurationSettings.OutBoundChannel);

            // Create a message from the input and set its Extension property from the topic 
            //var bytes = Convert.FromBase64String(topic);
            var bytes = Encoding.Unicode.GetBytes(topic);
            var mess = new Message(message) {Extension = bytes};

            //HINT: Use Convert from and to Base 64 string to convert a string to bytes and vice-versa
            // Send the message
            producer.Send(mess);
        }

        private static bool CheckArguments(string message, string topic)
        {
            bool errors = false;
            if (string.IsNullOrEmpty(topic))
            {
                Console.WriteLine("You must provide a topic");
                errors = true;
            }

            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine("You must provide a message");
                errors = true;
            }
            return errors;
        }
    }
}

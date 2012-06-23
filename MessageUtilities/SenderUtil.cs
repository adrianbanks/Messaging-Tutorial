using System;

namespace MessageUtilities
{
    public static class SenderUtil
    {
        public static string GetMessage()
        {
            Console.Write("Enter message :> ");
            string message = Console.ReadLine();
            return message;
        }
    }
}

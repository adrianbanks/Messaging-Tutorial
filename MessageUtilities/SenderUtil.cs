using System;

namespace MessageUtilities
{
    public static class SenderUtil
    {
        public static string GetInput(string prompt)
        {
            Console.Write("Enter {0} :> ", prompt ?? "text");
            string message = Console.ReadLine();
            return message;
        }
    }
}

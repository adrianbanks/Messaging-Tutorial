using System;

namespace MessageUtilities
{
    public static class ConsolePause
    {
         public static void PauseForInput()
         {
             Console.WriteLine();
             Console.WriteLine("Press enter to continue");
             Console.ReadLine();
         }
    }
}

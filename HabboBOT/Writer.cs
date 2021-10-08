using System;

namespace HabboBOT
{
    public class Writer
    {
        public const string format = "[{0}] => {1}";

        public static void LogSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(string.Format(format, "SUCCESS", message));
            Console.ResetColor();
        }
        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(string.Format(format, "ERROR", message));
            Console.ResetColor();
        }
        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(string.Format(format, "WARNING", message));
            Console.ResetColor();
        }
    }
}
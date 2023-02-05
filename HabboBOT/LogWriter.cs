using System;

namespace HabboBOT
{
    internal class LogWriter
    {
        private static readonly string format = "[{0}] => {1}";

        public static void Log(string message, ConsoleColor color, string logType)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(string.Format(format, logType, message));
            Console.ResetColor();
        }

        public static void LogSuccess(string message) => Log(message, ConsoleColor.DarkGreen, "SUCCESS");
        public static void LogError(string message) => Log(message, ConsoleColor.DarkRed, "ERROR");
        public static void LogWarning(string message) => Log(message, ConsoleColor.DarkYellow, "WARNING");
    }
}
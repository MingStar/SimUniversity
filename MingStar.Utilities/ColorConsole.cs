using System;

namespace MingStar.Utilities
{
    public static class ColorConsole
    {
        public static void WriteLine(ConsoleColor color, string format, params object[] objects)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (objects == null)
            {
                Console.WriteLine(format);
            }
            else
            {
                Console.WriteLine(format, objects);
            }
            Console.ForegroundColor = previousColor;
        }

        public static void WriteLine(ConsoleColor color, object obj)
        {
            WriteLine(color, obj.ToString(), null);
        }

        public static void Write(ConsoleColor color, string format, params object[] objects)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (objects == null)
            {
                Console.Write(format);
            }
            else
            {
                Console.Write(format, objects);
            }
            Console.ForegroundColor = previousColor;
        }

        public static void Write(ConsoleColor color, object obj)
        {
            Write(color, obj.ToString(), null);
        }


        public static void WriteLineIf(bool predicate, ConsoleColor color, string format, params object[] objects)
        {
            if (predicate)
            {
                WriteLine(color, format, objects);
            }
        }

        public static void WriteLineIf(bool predicate, ConsoleColor color, object obj)
        {
            if (predicate)
            {
                WriteLine(color, obj);
            }
        }
    }
}
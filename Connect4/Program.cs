using System;
using static System.Console;

namespace Connect4
{
    class Program
    {
        private static string name;
        private static Int16 debth;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("A Change");
            int change2 = 0;
        }
        public static string Name{get{ return name; }
            set {
                bool accept = false;
                while(accept == false)
                {
                    if (value.Length > 30 || value.Length < 1)
                    {
                        WriteLine("Please enter a name between 1-30 Characters");
                    }
                    else
                        name = value;
                }
            } }
    }
}
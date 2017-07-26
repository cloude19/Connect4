using System;
using static System.Console;

namespace Connect4
{
    class Program
    {
        private static string name;
        private static string debth;

        static void Main(string[] args)
        {
            //Run the Test Function?
            Console.Write("Would you like to run the Test function?: Enter Y or N");
            if (Console.ReadLine() == "Y")
                Testing();

            //get users name and diffculty
            Console.Write("Enter your Name: ");
            Name = ReadLine();
            Console.Write("Enter 1-4 for diffculty: ");
            Debth = ReadLine();
            //Create and intilized board
            Board Game = new Board(6, 6);
            

        }
        public static string Name{get{ return name; }
            set {
                bool accept = false;
                while(accept == false)
                {
                    if (value.Length > 30 || value.Length < 1)
                    {
                        WriteLine("Please enter a name between 1-30 Characters");
                        value = ReadLine();
                    }
                    else
                    {
                        name = value;
                        accept = true;
                    }
                }
            } }
        public static string Debth
        {
            get
            {
                return debth;
            }
            set
            {
                bool accept = false;
                while(accept == false)
                {
                    if (Int16.TryParse(value, out Int16 test))
                    {
                        if (Int16.Parse(value) < 1 || Int16.Parse(value) > 4)
                        {
                            WriteLine("Please enter a number between 1-4");
                            value = ReadLine();
                        }
                        else
                        {
                            debth = value;
                            accept = true;
                        }
                    }
                    else
                    {
                        WriteLine("Please enter a number between 1-4");
                        value = ReadLine();
                    }

                }
            }
        }

        public static void Testing()
        {
            //function used to test various actions
            Console.WriteLine("Test Display function(A)\nTest add token function(B)\n" +
                "Test Search Points function(C)\n");
            Console.Write("Please Enter option: ");
            string toTest = ReadLine();
            Board Game = new Board();

            switch (toTest)
            {
                case "A":
                    //Test Display and Add token funciton
                    Game.DisplayGird();
                    Game.AddToken(3);
                    Game.DisplayGird();
                    break;
                case "B":
                    //Test add token function
                    Game.AddToken(3);
                    Game.AddToken(2);
                    Game.AddToken(1);
                    Game.AddToken(1);
                    Game.DisplayGird();
                    break;
                case "C":
                    //Test Search Points Functuion
                    Game.AddToken(1);
                    Game.AddToken(1);
                    Game.AddToken(1);
                    Game.AddToken(1);
                    Game.AddToken(2);
                    Game.AddToken(2);
                    Game.AddToken(2);
                    Game.AddToken(3);
                    Game.AddToken(3);
                    Console.WriteLine(Game.SearchPoints(Game.AddToken(4), 'P'));
                    Game.DisplayGird();
                    break;
                default:
                    Console.WriteLine("Invalid Test Command");
                    break;


            }
        }
    }
}
﻿using System;
using static System.Console;
using System.Collections.Generic;


namespace Connect4
{
    class Program 
    { 
        private static string name;
        private static string debth; //also used for player choice
        
        static void Main(string[] args)
        {
            //Run the Test Function?
            //   Console.Write("Would you like to run the Test function?: Enter Y or N");
            //   if (Console.ReadLine() == "Y")
            // Testing();

            bool AIwin = false;
            bool Pwin = false;
            Objective AI = new Objective();
            int DepthC = 0;

            //get users name and diffculty
            Console.Write("Enter your Name: ");
            Name = ReadLine();
            Console.Write("Enter 1-4 for diffculty: ");
            Debth = ReadLine();
            DepthC = int.Parse(Debth);
            //Create and intilized board
            Board Game = new Board(6, 6);

            //Example of pass value problem [is passing by reference]
            /* int, double, 
            Board temp = new Board();
            Board.AddToken(temp, 1, true);
            
            Game.DisplayGird();
            temp.DisplayGird();
            Board.AddToken(temp, 1, true);
            Game.DisplayGird();
            temp.DisplayGird();
            */

            //Continue till win is confirmed or board get's full[need to add function for this]
            while (AIwin == false || Pwin == false)
            {
                //player turn
                bool Added = false;
                int PW = 0;
                Tuple<int, int, bool> Check;

                Game.DisplayGird();
                while (Added == false)
                {
                    Console.WriteLine("Please Select a column 0 - 5");
                    debth = Console.ReadLine();
                    Check = Board.AddToken(Game, int.Parse(debth),false);
                    if (Check.Item3 == true)
                    {
                        PW = Game.SearchPoints(Check, 'P');
                        Added = Check.Item3;
                    }
                }
                
                if (PW == 4) 
                {
                    WriteLine($"Congradulations {name} You won!!!!");
                    Pwin = true;
                }

                //AI turn
               Tuple<int,bool> AIW = AI.objectiveFun(Game, DepthC, 6);
                if (AIW.Item2 == true)
                {
                    Console.WriteLine("The AI Has wone");
                    AIwin = true;
                }
                else
                    Board.AddToken(Game, AIW.Item1, true);

                Game.DisplayGird();
                //End*************************************************
            }
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
                "Test Search Points function(C)\n test Sorted Dict Tree\n");
            Console.Write("Please Enter option: ");
            string toTest = ReadLine();
            Board Game = new Board();

            switch (toTest)
            {
                case "A":
                    //Test Display and Add token funciton
                    Game.DisplayGird();
                    Board.AddToken(Game,3,false);
                    Game.DisplayGird();
                    break;
                case "B":
                    //Test add token function
                    Board.AddToken(Game, 3, false);
                    Board.AddToken(Game, 2, false);
                    Board.AddToken(Game, 1, false);
                    Board.AddToken(Game, 1, false);
                    Game.DisplayGird();
                    break;
                case "C":
                    //Test Search Points Functuion
                    Board.AddToken(Game, 1, false);
                    Board.AddToken(Game, 1, false);
                    Board.AddToken(Game, 1, false);
                    Board.AddToken(Game, 1, false);
                    Board.AddToken(Game, 2, false);
                    Board.AddToken(Game, 2, false);
                    Board.AddToken(Game, 2, true);
                    Board.AddToken(Game, 3, false);
                    Board.AddToken(Game, 3, true);
                    Console.WriteLine(Game.SearchPoints(Board.AddToken(Game, 4, false), 'P'));
                    Game.DisplayGird();
                    break;
                case "D":
                    //test Sorted Dict Tree
                    Objective objective = new Objective();
                    Console.WriteLine(objective.objectiveFun(Game, Int32.Parse(Debth), 6));
                    break;
                default:
                    Console.WriteLine("Invalid Test Command");
                    break;


            }
        }
        
    }
}
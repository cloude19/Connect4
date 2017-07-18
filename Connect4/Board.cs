using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    /*Create 2D grid array*, manage player input into grid, 
     * Search selected points[], get grid state[just copy int temp gridd], display grid
     * note: replace x with row and y with column [done]
     */
    class Board
    {
        private char[,] Gridd;

      public Board(){
            for (int Rowlimit = 0; Rowlimit < 5; Rowlimit++)
            {
                for (int ColumnLimit = 0; ColumnLimit < 5; ColumnLimit++)
                {
                    this.Gridd[Rowlimit, ColumnLimit] = '*';
                }
            }
        }
      public Board(int Row, int Column)
        {
            Gridd = new char[Row, Column];
            for(int limitx = 0; limitx < Row; limitx++)
            {   for(int limity = 0; limity < Column; limity++)
                {
                    this.Gridd[limitx, limity] = '*';
                }

            }
            
        }

        //start at the bottom of row value for Column and work way up till * is found else no space
        public void AddToken(int Column)
        {
            bool Legal = false;
            for(int row = this.Gridd.GetLength(1) - 1; row >= 0; row--)
            {
                //need to add condition for AI
                if(this.Gridd[row, Column] == '*')
                {
                    this.Gridd[row, Column] = 'P';
                    Legal = true;
                    break;
                }
            }
            if(Legal == false)
            {
                Console.WriteLine("This pace is full Please choose another");
            }
        }
        public void DisplayGird()
        {
            for(int x = 0; x < this.Gridd.GetLength(0); x++)
            {
                for (int y=0; y < this.Gridd.GetLength(1); y++)
                {
                    Console.Write(" "+this.Gridd[x, y]+" ");
                }
                Console.WriteLine(" ");
            }
        }
        public int SearchPoints(int row, int column, char focus)
        {
            int points = 0;
            //Try to check diagonals if it exeist in memory

            //Try to check vertical if it exist in memory

            //Try to check horizontals if it exist in memory

            return points;
        }
        
    }
}

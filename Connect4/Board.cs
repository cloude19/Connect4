using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    /*Create 2D grid array*, manage player input into grid, 
     * Search selected points[], get grid state[just copy int temp gridd], display grid
     * note: replace x with row and y with column [done]
     */
   public class Board
    {
        private char[,] Gridd;
    
      public Board(){
            Gridd = new char[5, 5];
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
        static public Tuple<int, int, bool> AddToken(Board Target, int Column, bool AI)
        {
            for(int row = Target.Gridd.GetLength(0) - 1; row >= 0; row--)
            {
                //need to add condition for AI
                if(Target.Gridd[row, Column] == '*')
                {
                    if(AI == false)
                        Target.Gridd[row, Column] = 'P';
                    else
                        Target.Gridd[row, Column] = 'A';
                    return Tuple.Create(row, Column, true);
                    //break;
                }
            }
                //Console.WriteLine("This pace is full Please choose another");
                return Tuple.Create(0, 0, false);
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

        //copy grid from another grid [Need to find out why class objects can't be directly passed]
        public void CopyBoard(Board CopyTo, Board ToCopy)
        {
            if (CopyTo.Gridd.Length == ToCopy.Gridd.Length)
            {
                for(int x = 0; x < ToCopy.Gridd.GetLength(0); x++)
                {
                    for(int y = 0; y < ToCopy.Gridd.GetLength(0); y++)
                    {
                        CopyTo.Gridd[x, y] = ToCopy.Gridd[x, y];
                    }
                }
            }
            else
                Console.WriteLine("Warning gridds are not of equal dimensions");
        }

        //searh a token based off it's location and return how good that token is
        public int SearchPoints(Tuple<int, int, bool> Board, char focus)
        {
            int pointsFinal = 0;
            int pointsRD = 0;
            int pointsLD = 0;
            int pointsVertical = 0;
            int pointsHorizontal = 0;

            //Try to check diagonals if it exist in memory

            //Check Right Diagonal 
            //Left downward
            if (Board.Item1 != this.Gridd.GetLength(0) - 1) {
                try {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1 + step, Board.Item2 - step] == focus)
                        {
                            pointsRD++;
                        }
                        else
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Diagonals left downward reached end");
                }
            }
            //Right upward
            if (Board.Item2 != this.Gridd.GetLength(1) - 1)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1 - step, Board.Item2 + step] == focus)
                        {
                            pointsRD++;
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Diagonals Right upward reached end");
                }
            }
            if (pointsFinal < pointsRD)
                pointsFinal = pointsRD;

            //Check Left Diagonal 
            //Right Downward
            if (Board.Item1 != this.Gridd.GetLength(0) - 1)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1 + step, Board.Item2 + step] == focus)
                        {
                            pointsLD++;
                        }
                        else
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Diagonals left downward reached end");
                }
            }

            //Left Upward
            if (Board.Item2 != 0)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1 - step, Board.Item2 - step] == focus)
                        {
                            pointsLD++;
                        }
                        else
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Diagonals left Upward reached end");
                }
            }
            if (pointsFinal < pointsLD)
                pointsFinal = pointsLD;

            //Try to check vertical if it exist in memory
            //Check up
            if (Board.Item1 != 0)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1 - step, Board.Item2] == focus)
                        {
                            pointsVertical++;
                        }
                        else
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vertical up reached end");
                }
            }

            //Check Down
            if (Board.Item1 != this.Gridd.GetLength(0) - 1)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1 + step, Board.Item2] == focus)
                        {
                            pointsVertical++;
                        }
                        else
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vertical Down reached end");
                }
            }
            if (pointsFinal < pointsVertical)
                pointsFinal = pointsVertical;

            //Try to check horizontals if it exist in 
            //Check Horizontal left
            if (Board.Item2 != 0)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1, Board.Item2 - step] == focus)
                        {
                            pointsHorizontal++;
                        }
                        else
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Horizontal Left reached end");
                }
            }

            //Check Horizontal Right 
            if (Board.Item2 != this.Gridd.GetLength(1) - 1)
            {
                try
                {
                    for (int step = 1; step < 5; step++)
                    {
                        if (this.Gridd[Board.Item1, Board.Item2 + step] == focus)
                        {
                            pointsHorizontal++;
                        }
                        else
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Horizontal Left reached end");
                }
            }
            if (pointsFinal < pointsHorizontal)
                pointsFinal = pointsHorizontal;

            return pointsFinal + 1;
        }
        
    }
}

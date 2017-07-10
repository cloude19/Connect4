using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{
    /*Create grid array, manage player input into grid, 
     * Search selected points, get grid state, display grid
     */
    class Board
    {
        private char[,] Gridd;

        Board(){}
        Board(int x, int y)
        {
            Gridd = new char[x, y];
            for(int limitx = 0; limitx < x; limitx++)
            {   for(int limity = 0; limity < y; limity++)
                {
                    Gridd[limitx, limity] = '*';
                }

            }
            
        }
        
    }
}

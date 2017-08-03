using System;
using System.Collections.Generic;
using System.Text;

namespace Connect4
{

    class Objective: Board
    {
       public SortedDictionary<int, Node> Tree = new SortedDictionary<int, Node>();
        
        //take the current state of the board and begin creating tree
               
    }

    public class Node
    {
        Board BoardState;
        int weight = 9;
        Node Parent;
        Node Child;
    }
}

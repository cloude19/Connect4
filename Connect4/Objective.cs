using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Connect4
{

    class Objective
    {    
       public int objectiveFun(Board cState, int level, int boardWidth)
        {
            //add first item to dict 
            Dictionary<int, List<Node>> Tree = new Dictionary<int, List<Node>>();
            //List<List<Node>> Tree = new List<List<Node>>();

            List<Node> States = new List<Node>();
            List<Node> TreeList = new List<Node>();
            bool AI = false;
            int weight = 0;
            int CurrentItem = 0;
            Tuple<int, int, bool> CFull;
            States.Add(new Node { BoardState = cState, Origin = true});
            Tree.Add(0, new List<Node>(States));

            //Tree.Add(new List<Node>(States));
            Console.Write(Tree[0]);
            States.Clear();
            level *= 2;

            //Create temporary board for adding tokens
            Board temp = new Board(boardWidth,boardWidth);

            //create tree to transverse
            for(int limit = 1; limit <= level; limit++)
            {
                //pass past grid states into list
                int next = 0;
                States = Tree[limit -1];
                //States[0].BoardState.DisplayGird();
               

                //alternate between Player and AI

                if (AI == false)
                    AI = true;
                else
                    AI = false;
                    
                while(States[next] != null) 
                {
                    //if state is terminal skip it
                    if (States[next].Terminal != true)
                    {
                        //add tokens and pass to tree list [use switch for swap between AI and Player]
                        // States = Tree[next];
                        for (int x = 0; x < boardWidth; x++)
                        {
                            temp.CopyBoard(temp, States[next].BoardState);
                            //States[next].BoardState.DisplayGird();
                            temp.DisplayGird();

                            if (AI == true)
                            {
                                CFull = Board.AddToken(temp, x, AI);
                                if (CFull.Item3 == false)
                                {
                                    TreeList.Add(new Node { Skip = true });
                                }
                                else
                                {
                                    weight = temp.SearchPoints(CFull, 'A');
                                    if (weight == 4)
                                    {
                                        TreeList.Add(new Node { position = x, aiW = weight, Terminal = true, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);
                                    }

                                    else
                                    {
                                        TreeList.Add(new Node { position = x, aiW = weight, Terminal = false, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);

                                    }
                                }
                            }
                            else
                            {
                                CFull = Board.AddToken(temp, x, AI);
                                if (CFull.Item3 == false)
                                {
                                    TreeList.Add(new Node { Skip = true});
                                }
                                else
                                {
                                    weight = temp.SearchPoints(CFull, 'P');
                                    if (weight == 4)
                                    {
                                        TreeList.Add(new Node { position = x, aiW = weight, Terminal = true, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);
                                    }

                                    else
                                    {
                                        TreeList.Add(new Node { position = x, aiW = weight, Terminal = false, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);
                                    }
                                }
                            }
                            weight = 0;
                            CurrentItem++;
                        }
                        try
                        {
                           next++;
                           bool exist = States[next].Terminal;
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine(exp);
                            break;
                        }
                    }
                    else
                        next++;
                }
                Tree.Add(limit, new List<Node>(TreeList));

                /*
                foreach(Node node in TreeList)
                {
                    node.BoardState.DisplayGird();
                }
                */

                /*
                TreeList = Tree[1];
                Tree[1][0].BoardState.DisplayGird();
                TreeList[0].BoardState.DisplayGird();
                */
                CurrentItem = 0;
                TreeList.Clear();
                States.Clear();
            }
            
            //tranverse tree from bottom to top in order to find min value 
            //test tree content
            int Xnext = 0; //when equal to boardwidth pass maxes to parent nodes
            int Ynext = 0; //Increment after parent node gets maxes
            int AMax = 0; //max AI weight found
            int PMax = 0; //max Player weight found
            int T = level; // max depth
          //  TreeList = Tree[level - parent];
          //  States = Tree[level - current];

            while (T > 1)
            {

                for(int x = 0; x < Tree[T].Count(); x++)
                {
                    if (Tree[T][x].Skip == true)
                    {
                        //skip
                    }
                    else
                    {
                        //compare AMax and PMax to current state if bigger replace
                        if (Tree[T][x].aiW > AMax)
                            AMax = Tree[T][x].aiW;
                        if (Tree[T][x].pW > PMax)
                            PMax = Tree[T][x].pW;
                    }
                    Xnext++;

                    if(Xnext == boardWidth)
                    {
                        Tree[T - 1][Ynext].pW = PMax;
                        Tree[T - 1][Ynext].aiW = AMax;
                        Ynext++;
                    }

                }
                T++;
              
            }

            //look at list 1 to determine the value to return
            bool foundTerminal = false;
            int Fposition = 0;
            int pMin = 0;
            for(int x = 0; x < Tree[1].Count(); x++)
            {
                //first check if node is terminal
                if(Tree[1][x].Terminal == true)
                {
                    foundTerminal = true;
                    //if it is in AI favor return value
                    if (Tree[1][x].aiW == 4)
                        return Tree[1][x].position;
                    //if not pass position and check other nodes for favorable terminal node
                    if (Tree[1][x].pW == 4)
                        Fposition = Tree[1][x].position;
                }
                //find node with miniume pW and return position
                if (Tree[1][x].pW < pMin && foundTerminal == false)
                {
                    pMin = Tree[1][x].pW;
                    Fposition = Tree[1][x].position;
                }

            }
            return 9;
        }
        
        /*
        void CopyDictValu(Dictionary<int, List<Node>> dict, List<Node> State)
        {
            foreach(List<> val in dict[0])
            {

            }
        }
        */
    }

    public class Node 
    {
        public Board BoardState = new Board(6,6); //need to set in hard code
        public int position = 9;
        public int pW = 9;
        public int aiW = 9;
        public bool Terminal = false;
        public Node Parent; //state[Next]
        public bool Origin = false;
        public bool Skip = false; //Can skip state due to it being a dummy
    }
}

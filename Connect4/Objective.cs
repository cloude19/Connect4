using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Connect4
{

    class Objective
    {    
       public Tuple<int,bool> objectiveFun(Board cState, int level, int boardWidth)
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
            States.Add(new Node { BoardState = cState, Origin = true, pW = 1, aiW = 1});
            Tree.Add(0, new List<Node>(States));

            //Tree.Add(new List<Node>(States));
            
          //  Console.Write(Tree[0]);
            States.Clear();
            level *= 2;

            //Create temporary board for adding tokens
            Board temp = new Board(boardWidth,boardWidth);

            //create tree to transverse
            for (int limit = 1; limit <= level; limit++)
            {
                //pass past grid states into list
                int next = 0;
                States = Tree[limit - 1];
                //States[0].BoardState.DisplayGird();


                //alternate between Player and AI

                if (AI == false)
                    AI = true;
                else
                    AI = false;

                while (States[next] != null)
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
                            //temp.DisplayGird();

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
                                        TreeList[CurrentItem].pW = TreeList[CurrentItem].Parent.pW;
                                        TreeList[CurrentItem].Favor = 100;
                                    }

                                    else
                                    {
                                        TreeList.Add(new Node { position = x, aiW = weight, Terminal = false, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);
                                        TreeList[CurrentItem].pW = TreeList[CurrentItem].Parent.pW;
                                        TreeList[CurrentItem].Favor = TreeList[CurrentItem].Parent.aiW - TreeList[CurrentItem].Parent.pW;

                                    }
                                }
                            }
                            else
                            {
                                CFull = Board.AddToken(temp, x, AI);
                                if (CFull.Item3 == false)
                                {
                                    TreeList.Add(new Node { Skip = true });
                                }
                                else
                                {
                                    weight = temp.SearchPoints(CFull, 'P');
                                    if (weight == 4)
                                    {
                                        TreeList.Add(new Node { position = x, pW = weight, Terminal = true, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);
                                        TreeList[CurrentItem].aiW = TreeList[CurrentItem].Parent.aiW;
                                        TreeList[CurrentItem].Favor = -100;
                                    }

                                    else
                                    {
                                        TreeList.Add(new Node { position = x, pW = weight, Terminal = false, Parent = States[next] });
                                        TreeList[CurrentItem].BoardState.CopyBoard(TreeList[CurrentItem].BoardState, temp);
                                        TreeList[CurrentItem].aiW = TreeList[CurrentItem].Parent.aiW;
                                        TreeList[CurrentItem].Favor = TreeList[CurrentItem].Parent.aiW - TreeList[CurrentItem].Parent.pW;
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
                          //  Console.WriteLine(exp);
                            break;
                        }
                    }
                    else
                        next++;
                }
                Tree.Add(limit, new List<Node>(TreeList));

                //view just added tree list
                foreach (Node node in TreeList)
                {
                  //  node.BoardState.DisplayGird();
                }



               // Console.WriteLine(Tree[1][0].pW);

                CurrentItem = 0;
                TreeList.Clear();
                // States.Clear [Warning: can delete a state outta the Tree]
            }

            //tranverse tree from bottom to top in order to find min value 
            //test tree content
            int Xnext = 0; //when equal to boardwidth pass maxes to parent nodes
            int Ynext = 0; //Increment after parent node gets maxes
            /*
            int AMin = 9; //max AI weight found
            int PMin = 9; //max Player weight found
            */
            int Fmin = 9;
            int T = level; // max depth
          //  TreeList = Tree[level - parent];
          //  States = Tree[level - current];

            //test weight on pw 1
            /*
            for(int x = 0; x < 6; x++)
            {
                Console.WriteLine(Tree[1][x].Favor);
            }
            */

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
                        //Compare Fmax to favor to find favor minimue 
                        if (Tree[T][x].Favor < Fmin && (Tree[T][x].Favor != -100 && T > 2))
                            Fmin = Tree[T][x].Favor;
                        else if(Tree[T][x].Favor < Fmin && T == 2)
                            Fmin = Tree[T][x].Favor;
                        /*
                        if (Tree[T][x].pW < PMin && Tree[T][x].pW != 9)
                            PMin = Tree[T][x].pW;
                            */
                    }
                    Xnext++;

                   if(Xnext == boardWidth && T == 2 && Tree[T - 1][Ynext].Favor == 100)
                    {
                        //skip
                    }
                    else if(Xnext == boardWidth)
                    {
                        /*
                        Tree[T - 1][Ynext].pW = PMin;
                        Tree[T - 1][Ynext].aiW = AMin;
                        */
                        Tree[T - 1][Ynext].Favor = Fmin;

                        try
                        {
                            Ynext++;
                            Tree[T - 1][Ynext].Favor = Fmin;
                        }
                        catch(Exception exp)
                        {
                            Console.WriteLine(exp);
                            break;
                        }
                        Xnext = 0;
                    }

                }
                T--;
              
            }
            /*
            for (int x = 0; x < 6; x++)
            {
                Console.WriteLine(Tree[1][x].Favor);
            }
            */

            //look at list 1 to determine the value to return
            bool foundTerminal = false;
            int Fposition = 0;
            int FMax = -9;
            for(int x = 0; x < Tree[1].Count(); x++)
            {
                //first check if node is terminal
                if(Tree[1][x].Favor == -100 || Tree[1][x].Favor == 100)
                {
                    foundTerminal = true;
                    //if it is in AI favor return value
                    if (Tree[1][x].Favor == 100)
                        return  Tuple.Create(Tree[1][x].position,true);
                    //if not pass position and check other nodes for favorable terminal node
                    if (Tree[1][x].Favor == -100)
                        Fposition = Tree[1][x].position;
                }
                //find node with miniume pW and return position
                if (Tree[1][x].Favor > FMax == false)
                {
                    FMax = Tree[1][x].Favor;
                    Fposition = Tree[1][x].position;
                }

            }
            return Tuple.Create(Fposition,false);
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
        public int Favor = 0; //How favorable is a given state for the AI
        public bool Terminal = false;
        public Node Parent; //state[Next]
        public bool Origin = false;
        public bool Skip = false; //Can skip state due to it being a dummy
    }
}

using System;

namespace Przesuwanka
{
    static class TreeSearch
    {

        public static Node<State> TreeSearchWithQueue<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
        {
            fringe.Add(new Node<State>(problem.InitialState, null));///tworzy root na stosie
            
            while(!fringe.IsEmpty)
            {
                Node<State> node = fringe.Pop();//zdjecie ze stosu
                if(problem.IsGoal(node.StateOfNode))//sprawdzenie zdjetego elementu ze stosu
                {
                    return node;
                }

                foreach(State actualState in problem.Expand(node.StateOfNode))///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                {
                    Console.Write(".");
                    
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))//Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        //problem.showState(actualState);
                        // Console.ReadKey();
                        //Console.WriteLine();
                        fringe.Add(new Node<State>(actualState, node));
                    }

                }
            }
            return null;
        }
    }
}
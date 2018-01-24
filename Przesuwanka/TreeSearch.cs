using System;

namespace Przesuwanka
{
    internal static class TreeSearch
    {
        public static Node<State> TreeSearchMetod<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
        {
            fringe.Add(new Node<State>(problem.InitialState, null, 1)); ///tworzy root na stosie

            while (!fringe.IsEmpty)
            {

                var node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                    return node;

                foreach (var actualState in problem.Expand(node.StateOfNode)
                    ) ///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare)
                    ) //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                        fringe.Add(new Node<State>(actualState, node, node.CountOfSteps));
            }

            return null;
        }

        public static Node<State> TreeSearchPriorityQueue<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
        {
            fringe.Add(new Node<State>(problem.InitialState, null, 1)); ///tworzy root na stosie

            while (!fringe.IsEmpty)
            {

                var node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                    return node;

                foreach (var actualState in problem.ExpandPriorityQueue(node.StateOfNode))
                    ///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                        fringe.Add(new Node<State>(actualState, node, node.CountOfSteps));
            }

            return null;
        }


        public static Node<State> TreeSearchAStar<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
        {
            fringe.Add(new Node<State>(problem.InitialState, null, 1)); ///tworzy root na stosie

            while (!fringe.IsEmpty)
            {

                var node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                    return node;

                foreach (var actualState in problem.ExpandAStar(node.StateOfNode))
                    ///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                        fringe.Add(new Node<State>(actualState, node, node.CountOfSteps));
            }

            return null;
        }
    }
}
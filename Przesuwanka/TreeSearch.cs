using System;

namespace Przesuwanka
{
    internal static class TreeSearch<State>
    {

        public static Node<State> TreeSearchMethod(IProblem<State> problem, IFringe<Node<State>> fringe, Enum method)
        {
            Func<Node<State>, int> calculatePriorityForBestFirstSearch = newState =>
                problem.CountOfConflicts(newState.StateOfNode);

            Func<Node<State>, int> calculatePriorityForAStar = newstate =>
                problem.CountDistancesToGoal(newstate.StateOfNode);


            fringe.SetCompareMethod(ComparePriority);
            
            var initNode = new Node<State>(problem.InitialState, null);
            initNode.Priority = CalculatePriorityMethod(method,
                calculatePriorityForBestFirstSearch,
                calculatePriorityForAStar, initNode);

            fringe.Add(initNode); ///tworzy root na stosie

            while (!fringe.IsEmpty)
            {
                var node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                    return node;
                
                problem.CountOfSteps++;

                foreach (var actualState in problem.Expand(node.StateOfNode))
                    //foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    // już nie wystąpił, wywołując OnPathToRoot,
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        var nodeToAdd = new Node<State>(actualState, node);
                        nodeToAdd.Priority = CalculatePriorityMethod(method,
                            calculatePriorityForBestFirstSearch,
                            calculatePriorityForAStar, nodeToAdd);

                        fringe.Add(nodeToAdd);
                    }
            }

            return null;
        }

        private static int CalculatePriorityMethod(Enum method,
            Func<Node<State>, int> calculatePriorityBestFirstSearch,
            Func<Node<State>, int> calculatePriorityForAStar, Node<State> newState)
        {
            switch (method.ToString())
            {
                case ("PriorityQueue"):
                {
                    return calculatePriorityBestFirstSearch(newState); //Priorytet względem ilości płytek na złej pozycji
                }

                case ("AStar"):
                {
                    return calculatePriorityForAStar(newState) + newState.StepsForSolution;
                } //Priorytet względem ilości płytek na złej pozycji + odległość Manhattan dla każdej płytki od pozycji,
                //gdzie powinna się znajdować + ilość kroków, im ich mniej tym stan jest bardziej obiecujący
                default: return 0;
            }
        }

        public static bool ComparePriority(Node<State> node1, Node<State> node2) //Metoda używana w sortowaniu
        {
            if (node1.Priority < node2.Priority) return true;
            return false;
        }
    }
}
using System;

namespace nHetmans
{
    internal static class TreeSearch<State>
    {
        public static int CountOfSteps { get; set; }

        public static Node<State> TreeSearchMethod(IProblem<State> problem, IFringe<Node<State>> fringe, Enum method)
        {
            Func<Node<State>, int> calculatePriorityForBestFirstSearch = newNode =>
                problem.CountOfConflicts(newNode.StateOfNode);

            Func<Node<State>, int> calculatePriorityForAStar = newNode =>
                problem.CountOfConflicts(newNode.StateOfNode) + newNode.StepsForSolution;

            fringe.SetCompareMethod(ComparePriority);
            fringe.Add(new Node<State>(problem.InitialState, null)); ///tworzy root na stosie

            while (!fringe.IsEmpty)
            {
                var node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                    return node;

                foreach (var actualState in problem.Expand(node.StateOfNode))
                    //foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    // już nie wystąpił, wywołując OnPathToRoot,
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        var nodeToAdd = new Node<State>(actualState, node, node.StepsForSolution++);
                        nodeToAdd.Priority = CalculatePriorityMethod(method, calculatePriorityForBestFirstSearch,
                            calculatePriorityForAStar, nodeToAdd);

                        fringe.Add(nodeToAdd);
                        CountOfSteps++;
                    }
            }

            return null;
        }

        private static int CalculatePriorityMethod(Enum method,
            Func<Node<State>, int> calculatePriorityBestFirstSearch,
            Func<Node<State>, int> calculatePriorityForAStar, Node<State> newNode)
        {
            switch (method)
            {
                case Method.PriorityQueue:
                {
                    return calculatePriorityBestFirstSearch(newNode); //Priorytet względem ilości konfliktów
                }

                case Method.AStar:
                {
                    return calculatePriorityForAStar(newNode);
                } // Priorytet względem ilości konfliktów + ilość kroków, im ich mniej tym stan jest bardziej obiecujący

                default: return 0;
            }
        }

        public static bool ComparePriority(Node<State> node1, Node<State> node2) //Metoda używana w sortowaniu
        {
            if (node1.Priority <= node2.Priority) return true;
            return false;
        }

        private enum Method
        {
            Stack,
            Queue,
            PriorityQueue,
            AStar
        }
    }
}
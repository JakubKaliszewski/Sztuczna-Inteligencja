using System;

namespace MapaRumunii
{
    enum Method
    {
        Stack,
        Queue,
        PriorityQueue,
        AStar
    };


    static class TreeSearch<State>
    {
        public static int CountOfSteps { get; set; }
        
        public static Node<State> TreeSearchMetod(IProblem<State> problem, IFringe<Node<State>> fringe, Enum method)
        {
            Func<State, double> calculatePriority = newState =>
                problem.CalculateDistanceToDestinyCity(newState);

            Func<Node<State>, State, double> calculatePriorityWithDistanceInAStraightLine = (parent, newState) =>
                problem.CalculatePriorityWithDistanceInAStraightLine(parent.StateOfNode, newState);


            fringe.SetCompareMethod(ComparePriority);
            fringe.Add(new Node<State>(problem.InitialState, null, 1, Double.MaxValue)); //tworzy root na stosie

            while (!fringe.IsEmpty)
            {
                Node<State> node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                {
                    return node;
                }

                foreach (State actualState in problem.Expand(node.StateOfNode))
                    //foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    // już nie wystąpił, wywołując OnPathToRoot,
                {
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        Node<State> nodeToAdd = new Node<State>(actualState, node, node.StepsForSolution++,
                            CalculatePriorityMethod(method, calculatePriority,
                                calculatePriorityWithDistanceInAStraightLine, node, actualState));
                        
                        nodeToAdd.TotalRoad = node.TotalRoad + problem.GetDistanceToCity(node.StateOfNode, nodeToAdd.StateOfNode);
                        fringe.Add(nodeToAdd);
                        CountOfSteps++;
                    }
                }
            }
            return null;
        }

        private static double CalculatePriorityMethod(Enum method,
            Func<State, double> calculatePriority,
            Func<Node<State>, State, double> calculatePriorityWithDistanceInAStraightLine, Node<State> parent,
            State newState)
        {
            switch (method)
            {
                case Method.PriorityQueue:
                {
                    return calculatePriority(newState);
                    break;
                }

                case Method.AStar:
                {
                    return calculatePriorityWithDistanceInAStraightLine(parent, newState);
                    break;
                }
                
                default: return 0.0;
            }
        }

        public static bool ComparePriority(Node<State> node1, Node<State> node2)
        {
            if (node1.Priority >= node2.Priority)
                return true;
            return false;
        }
    }
}
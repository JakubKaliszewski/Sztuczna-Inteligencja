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
            Func<State, double> calculatePriorityForBestFirstSearch = newState =>
                problem.CalculateDistanceToDestinyCity(newState);
            //odleglosc w lini prostej

            Func<Node<State>, State, double> calculatePriorityForAStar = (parent, newState) =>
                problem.CalculatePriorityForAStar(parent.StateOfNode, newState);
            //Linia prosta + odleglosc krawedziowa, pozniej dodana jest 
            //droga juz pokonana


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
                            CalculatePriorityMethod(method, calculatePriorityForBestFirstSearch,
                                calculatePriorityForAStar, node, actualState));
                        
                        nodeToAdd.TotalRoad = node.TotalRoad + problem.GetDistanceToCity(node.StateOfNode, nodeToAdd.StateOfNode);
                        fringe.Add(nodeToAdd);
                        CountOfSteps++;
                    }
                }
            }
            return null;
        }

        private static double CalculatePriorityMethod(Enum method,
            Func<State, double> calculatePriorityBestFirstSearch,
            Func<Node<State>, State, double> calculatePriorityForAStar, Node<State> parent,
            State newState)
        {
            switch (method)
            {
                case Method.PriorityQueue:
                {
                    return calculatePriorityBestFirstSearch(newState);//Priorytet względem odległości linii prostej do miasta docelowego
                }

                case Method.AStar:
                {
                    return parent.TotalRoad + calculatePriorityForAStar(parent, newState);
                }// Priorytet względem Pokonanej już drogi + drogi od rodzica(do pokonania) + odległość "nowego Miasta" w linii prostej do celu
                
                default: return 0.0;
            }
        }

        public static bool ComparePriority(Node<State> node1, Node<State> node2)//Metoda używana w sortowaniu
        {
            if (node1.Priority <= node2.Priority) return true;
            else return false;
        }
    }
}
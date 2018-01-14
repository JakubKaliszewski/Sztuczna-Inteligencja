using System;
using System.Runtime.Remoting.Messaging;

namespace MapaRumuniiOdleglosciLiniaProsta
{
    static class TreeSearch
    {
        public static int countOfSteps;

        public static Node<State> TreeSearchMetod<State>(IProblem<State> problem, IFringe<Node<State>> fringe, Func<State,State,int> GetDistance)
        {
            fringe.Add(new Node<State>(problem.InitialState, null)); //tworzy root na stosie

            while (!fringe.IsEmpty)
            {
                countOfSteps++;
                Console.Write(".");

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
                        Node<State> nodeToAdd = new Node<State>(actualState, node);
                        nodeToAdd.CurrentDistance = node.CurrentDistance + GetDistance(actualState, node.StateOfNode);
                        fringe.Add(nodeToAdd);
                    }
                }
            }

            return null;
        }

        public static Node<State> TreeSearchPriorityQueue<State>(IProblem<State> problem, IFringe<Node<State>> fringe, Func<State,State,int> GetDistance)
        {
            fringe.Add(new Node<State>(problem.InitialState, null)); //tworzy root na stosie

            while (!fringe.IsEmpty)
            {
                countOfSteps++;
                Console.Write(".");

                Node<State> node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                {
                    return node;
                }

                foreach (State actualState in problem.ExpandPriority(node.StateOfNode))
                    //foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    // już nie wystąpił, wywołując OnPathToRoot,
                {
                    Console.Write(".");

                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        Node<State> nodeToAdd = new Node<State>(actualState, node);
                        nodeToAdd.CurrentDistance = node.CurrentDistance + GetDistance(actualState, node.StateOfNode);
                        fringe.Add(nodeToAdd);
                    }
                }
            }

            return null;
        }


        public static Node<State> TreeSearchAStar<State>(IProblem<State> problem, IFringe<Node<State>> fringe, Func<State,State,int> GetDistance)
        {
            fringe.Add(new Node<State>(problem.InitialState, null)); ///tworzy root na stosie

            while (!fringe.IsEmpty)
            {
                countOfSteps++;
                Console.Write(".");

                Node<State> node = fringe.Pop(); //zdjecie ze stosu
                if (problem.IsGoal(node.StateOfNode)) //sprawdzenie zdjetego elementu ze stosu
                {
                    return node;
                }

                foreach (State actualState in problem.ExpandAStar(node.StateOfNode))
                    //foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    // już nie wystąpił, wywołując OnPathToRoot,
                {
                    Console.Write(".");

                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        Node<State> nodeToAdd = new Node<State>(actualState, node);
                        nodeToAdd.CurrentDistance = node.CurrentDistance + GetDistance(actualState, node.StateOfNode);
                        fringe.Add(nodeToAdd);
                    }
                }
            }

            return null;
        }
    }
}
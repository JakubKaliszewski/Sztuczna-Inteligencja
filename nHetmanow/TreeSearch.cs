﻿using System;

namespace nHetmans
{
    static class TreeSearch
    {
        public static int countOfSteps;

        public static Node<State> TreeSearchMetod<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
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

                foreach (State actualState in problem.Expand(node.StateOfNode)
                    ) ///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                {
                    Console.Write(".");

                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare)
                    ) //Wykonuje sie gdy nie ma znalezionego identycznego stanu
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

        public static Node<State> TreeSearchPriorityQueue<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
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

                foreach (State actualState in problem.ExpandPriority(node.StateOfNode))
                    ///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                {
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        fringe.Add(new Node<State>(actualState, node));
                    }
                }
            }

            return null;
        }
        
        public static Node<State> TreeSearchAStar<State>(IProblem<State> problem, IFringe<Node<State>> fringe)
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

                node.CountOfSteps++;

                foreach (State actualState in problem.ExpandAStar(node.StateOfNode, node.CountOfSteps))
                    ///foreach-a z możliwymy stanami, to tam sprawdzam czy dany stan z IListy
                    /// już nie wystąpił, wywołując OnPathToRoot,
                {
                    if (!node.OnPathToRoot(node.StateOfNode, actualState, problem.Compare))
                        //Wykonuje sie gdy nie ma znalezionego identycznego stanu
                    {
                        fringe.Add(new Node<State>(actualState, node, node.CountOfSteps));
                    }
                }
            }

            return null;
        }
    }
}
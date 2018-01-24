using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MapaRumuniiOdleglosciLiniaProsta
{
    class Node<State>
    {
        public State StateOfNode { get; private set; }
        private Node<State> parent;
        public int CurrentDistance { get; set; }
        public int CountOfSteps { get; set; }

        public Node(State state, Node<State> parent)
        {
            StateOfNode = state;
            this.parent = parent;
            CountOfSteps = 1;
        }
        
        public Node(State state, Node<State> parent, int steps)
        {
            StateOfNode = state;
            this.parent = parent;
            CountOfSteps = steps;
        }


        public void ShowRoad(Action<State, int> ShowState)
        {
            ShowState(StateOfNode, this.CurrentDistance);
            if (parent == null) return;
            ShowRoad(this.parent, ShowState);
        }

        public void ShowRoad(Node<State> node, Action<State, int> ShowState)
        {
            ShowState(node.StateOfNode, node.CurrentDistance);
            if (node.parent == null) return;
            ShowRoad(node.parent, ShowState);
        }

        public bool OnPathToRoot(State stateOfNode, State checkingState, Func<State, State, bool> Compare)
        {
            if (Compare(stateOfNode, checkingState)) //Stany są identyczne, 
            {
                Debug.WriteLine("True");
                return true;
            }

            if (this.parent == null) //Dalej juz nie moge, wiec nie wystapil
            {
                Debug.WriteLine("False");
                return false;
            }

            Debug.WriteLine("Rekurencja");
            return OnPathToRoot(parent, checkingState, Compare); //Dalej szukam stanu
        }


        private bool OnPathToRoot(Node<State> node, State checkingState, Func<State, State, bool> Compare)
        {
            if (Compare(node.StateOfNode, checkingState)) //Stany identyczne
            {
                Debug.WriteLine("True");
                return true;
            }

            if (node.parent == null)
            {
                Debug.WriteLine("False");
                return false;
            }

            Debug.WriteLine("Rekurencja");
            return OnPathToRoot(node.parent, checkingState, Compare); //Dalej szukam stanu
        }
    }
}
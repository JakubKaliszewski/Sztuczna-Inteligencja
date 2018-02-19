using System;

namespace Przesuwanka
{
    internal class Node<State>
    {
        private readonly Node<State> parent;

        public Node(State state, Node<State> parent)
        {
            StateOfNode = state;
            this.parent = parent;
            if (parent != null)
                StepsForSolution = parent.StepsForSolution++;
            else
                StepsForSolution = 1;
        }

        public State StateOfNode { get; }
        public int StepsForSolution { get; set; }
        public int Priority { get; set; }

        public bool OnPathToRoot(State stateOfNode, State checkingState, Func<State, State, bool> Compare)
        {
            if (Compare(stateOfNode, checkingState)) //Stany są identyczne, 
                return true;

            if (parent == null) //Dalej juz nie moge, wiec nie wystapil
                return false;

            return OnPathToRoot(parent, checkingState, Compare); //Dalej szukam stanu
        }


        private bool OnPathToRoot(Node<State> node, State checkingState, Func<State, State, bool> Compare)
        {
            if (Compare(node.StateOfNode, checkingState)) //Stany identyczne
                return true;

            if (node.parent == null) return false;

            return OnPathToRoot(node.parent, checkingState, Compare); //Dalej szukam stanu
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    class Node<State>
    {
        public State StateOfNode { get; private set; }
        private Node<State> parent;

        public Node(State state, Node<State> parent)
        {
            this.StateOfNode = state;
            this.parent = parent;
        }


        public bool OnPathToRoot(State stateOfNode, State checkingState, Func<State,State,bool> Compare)
        {
            if (Compare(stateOfNode, checkingState)) //Stany są identyczne, 
            {
                Debug.WriteLine("True");
                return true;
            }

            if (this.parent == null)//Dalej juz nie moge, wiec nie wystapil
            {
                Debug.WriteLine("False");
                return false;
            }
  
            Debug.WriteLine("Rekurencja");
            return OnPathToRoot(parent, checkingState, Compare);//Dalej szukam stanu
        }


        private bool OnPathToRoot(Node<State> node, State checkingState, Func<State, State, bool> Compare)
        {
            if (Compare(node.StateOfNode, checkingState))//Stany identyczne
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
            return OnPathToRoot(node.parent, checkingState, Compare);//Dalej szukam stanu
        }

    }
}
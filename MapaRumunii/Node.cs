﻿using System;
using System.Diagnostics;

namespace MapaRumunii
{
    public class Node<State>
    {
        public State StateOfNode { get; private set; }
        private Node<State> parent;
        public double TotalRoad { get; set; }
        public double Priority { get; set; }
        public int StepsForSolution { get; set; }

        public Node(State state, Node<State> parent)
        {
            StateOfNode = state;
            this.parent = parent;
            StepsForSolution = 0;
        }
        
        public Node(State state, Node<State> parent, int stepsForSolution, double priority)
        {
            StateOfNode = state;
            this.parent = parent;
            StepsForSolution = stepsForSolution;
            Priority = priority;
        }

   
        public void ShowRoad(Action<State, double> ShowState)
        {
            ShowState(StateOfNode, this.TotalRoad);
            if (parent == null) return;
            ShowRoad(this.parent, ShowState);
        }

        public void ShowRoad(Node<State> node, Action<State, double> ShowState)
        {
            ShowState(node.StateOfNode, node.TotalRoad);
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
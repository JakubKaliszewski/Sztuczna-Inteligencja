﻿using System.Collections.Generic;

namespace MapaRumunii
{
    internal interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        bool Compare(State stateOfNode, State checkingState);
        IList<State> Expand(State state);
        double GetDistanceToCity(State parentCity, State nextCity);
        double CalculateDistanceToDestinyCity(State nextCity);
        double CalculatePriorityForAStar(State parentCity, State nextCity);
    }
}
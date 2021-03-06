﻿using System.Collections.Generic;

namespace nHetmans
{
    public interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        bool Compare(State stateOfNode, State checkingState);
        IList<State> Expand(State state);
        int CountOfConflicts(State state);
    }
}
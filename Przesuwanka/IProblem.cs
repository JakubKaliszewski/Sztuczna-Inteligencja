using System;
using System.Collections.Generic;

namespace Przesuwanka
{
    interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        bool Compare(State stateOfNode, State checkingState);
        IList<State> Expand(State state);
        void showState(State state);
    }
}
using System.Collections.Generic;

namespace MapaRumuniiOdleglosciLiniaProsta
{
    interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        bool Compare(State stateOfNode, State checkingState);
        IList<State> Expand(State state);
        IList<State> ExpandPriority(State state);
    }
}
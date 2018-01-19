using System.Collections.Generic;

namespace Przesuwanka
{
    internal interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        bool Compare(State stateOfNode, State checkingState);
        IList<State> Expand(State state);
        IList<State> ExpandPriorityQueue(State state);
        IList<State> ExpandAStar(State state);
        void showState(State state);
    }
}
using System.Collections.Generic;

namespace Przesuwanka
{
    internal interface IProblem<State>
    {
        State InitialState { get; }
        bool IsGoal(State state);
        bool Compare(State stateOfNode, State checkingState);
        IList<State> Expand(State state);
        int CountOfConflicts(State state);
        int CountDistancesToGoal(State state);
        double CountOfSteps { get; set; }
    }
}
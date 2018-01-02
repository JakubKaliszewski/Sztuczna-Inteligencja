using System.Collections.Generic;

namespace MapaRumunii
{
    public class MapOfRomania : IProblem<Map>
    {
        public Map InitialState { get; }
        public bool IsGoal(Map state)
        {
            throw new System.NotImplementedException();
        }

        public bool Compare(Map stateOfNode, Map checkingState)
        {
            throw new System.NotImplementedException();
        }

        public IList<Map> Expand(Map state)
        {
            throw new System.NotImplementedException();
        }

        public void showState(Map state)
        {
            throw new System.NotImplementedException();
        }
    }
}
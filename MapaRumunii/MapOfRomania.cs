using System.Collections.Generic;

namespace MapaRumunii
{
    public class MapOfRomania : IProblem<City>
    {
        public City InitialState { get; }
        public City Destiny;

        public MapOfRomania(Map map, string startCity, string destinyCity)
        {
               
        }
        
        public bool IsGoal(City state)
        {
            throw new System.NotImplementedException();
        }

        public bool Compare(City stateOfNode, City checkingState)
        {
            throw new System.NotImplementedException();
        }

        public IList<City> Expand(City state)
        {
            throw new System.NotImplementedException();
        }

        public void showState(City state)
        {
            throw new System.NotImplementedException();
        }
    }
}
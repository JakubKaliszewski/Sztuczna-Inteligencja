using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;

namespace MapaRumunii
{
    public class MapOfRomania : IProblem<City>
    {
        public City InitialState { get; }
        public string startCity { get; }
        public string destinyCity { get; }
        public City Destiny;

        public MapOfRomania(Map map, string startCity, string destinyCity)
        {
            InitialState = GetCityByName(map, startCity);
            Destiny = GetCityByName(map, destinyCity);
        }

        private City GetCityByName(Map map, string name)
        {
            City returnedCity = new City();
            foreach (City city in map.Cities)
            {
                if (city.Name == name)
                {
                    returnedCity = city;
                }
            }

            if (returnedCity != null)
                return returnedCity;
            throw new SystemException("City not found");
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
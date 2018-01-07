using System;
using System.Collections.Generic;

namespace MapaRumunii
{
    public class MapOfRomania : IProblem<City>
    {
        public City InitialState { get; }
        public Map CurrentMap { get; }
        public City Destiny { get; }

        public MapOfRomania(Map map, string startCity, string destinyCity)
        {
            CurrentMap = map;
            InitialState = GetCityByName(startCity);
            Destiny = GetCityByName(destinyCity);
        }

        private City GetCityByName(string name)
        {
            City returnedCity = new City();
            foreach (City city in CurrentMap.Cities)
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
            return state.Name == Destiny.Name;
        }

        public bool Compare(City stateOfNode, City checkingState)
        {
            return stateOfNode.Name == checkingState.Name;
        }

        public IList<City> Expand(City state)
        {
            List<City> possibleStates = createStatesToExpand(state);
            return possibleStates;
        }


        private List<City> createStatesToExpand(City state)
        {
            List<City> possibleStates = GeneratePosisbleStates(state);

            return possibleStates;
        }

        private List<City> GeneratePosisbleStates(City state)
        {
            List<City> returnedList = new List<City>();
            returnedList = sortCitiesByDistances(state);

            return returnedList;
        }

        private List<City> sortCitiesByDistances(City state)
        {
            List<City> returnedList = new List<City>();
            List<int> distances = new List<int>();


            foreach (Neighbor neighbor in state.neighborsCities)
            {
                distances.Add(neighbor.distance);
            }

            distances.Sort();

            foreach (Neighbor neighbor in state.neighborsCities)
            {
                foreach (int distance in distances)
                {
                    if (distance == neighbor.distance)
                        returnedList.Add(neighbor.city);
                }
            }

            return returnedList;
        }

        public void ShowState(City state)
        {
            Console.WriteLine(state.Name);
        }
    }
}
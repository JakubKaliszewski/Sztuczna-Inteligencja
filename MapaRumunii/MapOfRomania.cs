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
            
            throw new Exception("City not found");
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
            List<City> possibleStates = GeneratePosisbleStates(state);
            return possibleStates;
        }

        public double GetDistanceToCity(City parentCity, City nextCity)
        {
            foreach (var city in parentCity.neighborsCities)
            {
                if (city.city.Name == nextCity.Name)
                {
                    return city.distance;
                }
            }
            
            throw new Exception("Distance not found!");
        }
               
        public double CalculatePriorityForAStar(City parentCity, City nextCity)
        {
            return CalculateDistanceToDestinyCity(nextCity) + GetDistanceToCity(parentCity, nextCity);
        }

        private List<City> GeneratePosisbleStates(City state)
        {
            List<City> returnedList = new List<City>();
            foreach (var neighbor in state.neighborsCities)
            {
                returnedList.Add(neighbor.city);
            }

            return returnedList;
        }

        public double CalculateDistanceToDestinyCity(City state)
        {
            return Math.Sqrt((Math.Pow(state.Xcoordinate - Destiny.Xcoordinate, 2))
                             + (Math.Pow(state.Ycoordinate - Destiny.Ycoordinate, 2)));
        }

        public void ShowState(City state, double distance)
        {
            Console.WriteLine("Miasto: " + state.Name + "\t\tPrzebyta odległość: " + distance);
        }
    }
}
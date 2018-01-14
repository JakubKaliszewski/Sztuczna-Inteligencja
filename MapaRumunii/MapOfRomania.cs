using System;
using System.Collections.Generic;

namespace MapaRumuniiOdleglosciLiniaProsta
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

        public IList<City> ExpandPriority(City state)
        {
            List<City> possibleStates = GeneratePosisbleStatesPriorityQueue(state);
            return possibleStates;
        }

        public IList<City> Expand(City state)
        {
            List<City> possibleStates = GeneratePosisbleStates(state);
            return possibleStates;
        }

        public IList<City> ExpandAStar(City state)
        {
            List<City> possibleStates = GeneratePosisbleStatesAStar(state);
            return possibleStates;
        }

        private List<City> GeneratePosisbleStatesAStar(City state)
        {
            List<City> returnedList = new List<City>();
            List<double> distances = new List<double>();
            List<Tuple<City, double>> citiesAndDistances = new List<Tuple<City, double>>();

            foreach (var neighbor in state.neighborsCities)
            {
                double distance = CalculateDistanceToDestinyCity(neighbor.city) + neighbor.distance;
                distances.Add(distance);
                citiesAndDistances.Add(new Tuple<City, double>(neighbor.city, distance));
            }


            distances.Sort();
            foreach (Tuple<City, double> city in citiesAndDistances)
            {
                foreach (double distance in distances)
                {
                    if (distance == city.Item2)
                    {
                        returnedList.Add(city.Item1);
                    }
                }
            }

            return returnedList;
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

        private List<City> GeneratePosisbleStatesPriorityQueue(City state)
        {
            List<City> returnedList = new List<City>();
            returnedList = sortCitiesByDistances(state);

            return returnedList;
        }
        
        private List<City> sortCitiesByDistances(City state)
        {
            List<City> returnedList = new List<City>();
            List<double> distances = new List<double>();
            List<Tuple<City, double>> citiesAndDistances = new List<Tuple<City, double>>();

            foreach (var neighbor in state.neighborsCities)
            {
                double distance = neighbor.distance;
                distances.Add(distance);
                citiesAndDistances.Add(new Tuple<City, double>(neighbor.city, distance));
            }

            distances.Sort();
            foreach (Tuple<City, double> city in citiesAndDistances)
            {
                foreach (double distance in distances)
                {
                    if (distance == city.Item2)
                    {
                        returnedList.Add(city.Item1);
                    }
                }
            }

            return returnedList;
        }

        private double CalculateDistanceToDestinyCity(City state)
        {
            return Math.Sqrt((Math.Pow(state.Xcoordinate - Destiny.Xcoordinate, 2))
                             + (Math.Pow(state.Ycoordinate - Destiny.Ycoordinate, 2)));
        }

        public void ShowState(City state, int distance)
        {
            Console.WriteLine("Miasto: " + state.Name + "\tPrzebyta odległość: " + distance);
        }
    }
}
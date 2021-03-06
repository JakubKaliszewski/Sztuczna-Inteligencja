﻿using System;
using System.Collections.Generic;

namespace MapaRumunii
{
    public class MapOfRomania : IProblem<City>
    {
        public MapOfRomania(Map map, string startCity, string destinyCity)
        {
            CurrentMap = map;
            InitialState = GetCityByName(startCity);
            Destiny = GetCityByName(destinyCity);
        }

        public Map CurrentMap { get; }
        public City Destiny { get; }
        public City InitialState { get; }

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
            var possibleStates = GeneratePosisbleStates(state);
            return possibleStates;
        }

        public double GetDistanceToCity(City parentCity, City nextCity)
        {
            foreach (var city in parentCity.neighborsCities)
                if (city.city.Name == nextCity.Name)
                    return city.distance;

            throw new Exception("Distance not found!");
        }

        public double CalculatePriorityForAStar(City parentCity, City nextCity)
        {
            return CalculateDistanceToDestinyCity(nextCity) + GetDistanceToCity(parentCity, nextCity);
        }

        public double CalculateDistanceToDestinyCity(City state)
        {
            return Math.Sqrt(Math.Pow(state.Xcoordinate - Destiny.Xcoordinate, 2)
                             + Math.Pow(state.Ycoordinate - Destiny.Ycoordinate, 2));
        }

        private City GetCityByName(string name)
        {
            var returnedCity = new City();
            foreach (var city in CurrentMap.Cities)
                if (city.Name == name)
                    returnedCity = city;

            if (returnedCity != null)
                return returnedCity;

            throw new Exception("City not found");
        }

        private List<City> GeneratePosisbleStates(City state)
        {
            var returnedList = new List<City>();
            foreach (var neighbor in state.neighborsCities) returnedList.Add(neighbor.city);

            return returnedList;
        }

        public void ShowState(City state, double distance)
        {
            Console.WriteLine("Miasto: " + state.Name + "\t\tPrzebyta odległość: " + distance);
        }
    }
}
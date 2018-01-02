using System.Collections.Generic;

namespace MapaRumunii
{
    public class City
    {
        private static int number;
        private int id { get;  }
        private string name { get; } 
        private List<Neighbor> neighborsCities { get; }

        public City(string name)
        {
            id = number;
            this.name = name;
            neighborsCities = new List<Neighbor>();
            number++;
        }

        public void AddNeighbor(City cityToAdd, int distance)
        {
            Neighbor neighbor = new Neighbor(distance,cityToAdd);
            neighborsCities.Add(neighbor);
        }
    }
}
using System.Collections.Generic;

namespace MapaRumunii
{
    public class City
    {
        private static int number;
        public int id { get;  }
        public string Name { get; } 
        public List<Neighbor> neighborsCities { get; }

        public City()
        {
            
        }
        
        public City(string name)
        {
            id = number;
            this.Name = name;
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
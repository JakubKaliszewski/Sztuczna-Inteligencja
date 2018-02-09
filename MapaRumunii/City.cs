using System.Collections.Generic;

namespace MapaRumunii
{
    public class City
    {
        private static int number;
        public int Xcoordinate { get; }
        public int Ycoordinate { get; }

        public int Id { get; }
        public string Name { get; }
        public List<Neighbor> neighborsCities { get; }

        public City()
        {
        }

        public City(string name, int x, int y)
        {
            Id = number;
            Name = name;
            Xcoordinate = x;
            Ycoordinate = y;
            neighborsCities = new List<Neighbor>();
            number++;
        }

        public void AddNeighbor(City cityToAdd, int distance)
        {
            Neighbor neighbor = new Neighbor(distance, cityToAdd);
            neighborsCities.Add(neighbor);
        }

        public int GetDistance(City city)
        {
            int distance = 0;
            foreach (var neighbor in neighborsCities)
            {
                if (neighbor.city.Name == city.Name)
                {
                    distance = neighbor.distance;
                }
            }
                      
            return distance;
        }
        
    }
}
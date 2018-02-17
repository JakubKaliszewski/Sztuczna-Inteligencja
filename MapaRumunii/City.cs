using System.Collections.Generic;

namespace MapaRumunii
{
    public class City
    {
        private static int number;

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

        public int Xcoordinate { get; }
        public int Ycoordinate { get; }

        public int Id { get; }
        public string Name { get; }
        public List<Neighbor> neighborsCities { get; }

        public void AddNeighbor(City cityToAdd, int distance)
        {
            var neighbor = new Neighbor(distance, cityToAdd);
            neighborsCities.Add(neighbor);
        }

        public int GetDistance(City city)
        {
            var distance = 0;
            foreach (var neighbor in neighborsCities)
                if (neighbor.city.Name == city.Name)
                    distance = neighbor.distance;

            return distance;
        }
    }
}
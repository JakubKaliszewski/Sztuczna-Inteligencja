using System.Collections.Generic;

namespace MapaRumuniiOdleglosciLiniaProsta
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

        public void AddNeighbor(City cityToAdd)
        {
            Neighbor neighbor = new Neighbor(cityToAdd);
            neighborsCities.Add(neighbor);
        }
    }
}
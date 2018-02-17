namespace MapaRumunii
{
    public class Neighbor
    {
        public Neighbor(int distance, City city)
        {
            this.distance = distance;
            this.city = city;
        }

        public int distance { get; }
        public City city { get; }
    }
}
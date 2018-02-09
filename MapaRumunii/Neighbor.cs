namespace MapaRumunii
{
    public class Neighbor
    {
        public int distance { get; }
        public City city { get; }

        public Neighbor(int distance, City city)
        {
            this.distance = distance;
            this.city = city;
        }
    }
}
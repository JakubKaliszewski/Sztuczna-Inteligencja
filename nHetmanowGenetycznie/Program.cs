using System;

namespace nHetmanowGenetycznie
{
    internal class Program
    {
        public static void Main()
        {           
            Console.WriteLine("Podaj liczbę iteracji algorytmu genetycznego: ");
            int liczbaIteracji = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Podaj rozmiar populacji: ");
            int rozmiarPopulacji = Convert.ToInt32(Console.ReadLine());
            
            Hetmani problemHetmani = new Hetmani(rozmiarPopulacji);
            problemHetmani.Szukaj(liczbaIteracji);
        }
    }
}
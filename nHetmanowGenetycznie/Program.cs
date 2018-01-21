using System;

namespace nHetmanowGenetycznie
{
    internal class Program
    {
        public static void PokazRezultat(byte[] rezultat)
        {
            Console.WriteLine();
            byte rozmiar = (byte) rezultat.GetLength(0);

            Console.Write("  ");
            for (byte i = 0; i < rozmiar; i++) // 0 1 2 3 4 5 6 7 - naglowek 
            {
                Console.Write(i + " ");
            }

            Console.Write("\n"); // przejscie do wlasciwego wyswietlania tresci

            for (byte wiersz = 0; wiersz < rozmiar; wiersz++)
            {
                Console.Write(wiersz + " ");
                for (byte kolumna = 0; kolumna < rozmiar; kolumna++)
                {
                    if (rezultat[kolumna] == wiersz)
                    {
                        Console.Write('\u25A0' + " ");
                        //Console.Write('H' + " ");
                    }
                    else Console.Write("  ");
                }

                Console.Write("\n");
            }
        }
        
        public static void Main()
        {           
        
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Podaj liczbę iteracji algorytmu genetycznego: ");
            int liczbaIteracji = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Podaj rozmiar populacji: ");
            int rozmiarPopulacji = Convert.ToInt32(Console.ReadLine());
            
            
            Hetmani problemHetmani = new Hetmani(rozmiarPopulacji);
            var rezultat =  problemHetmani.Szukaj(liczbaIteracji);
            PokazRezultat(rezultat);        
        }
    }
}
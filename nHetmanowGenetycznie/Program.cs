using System;
using System.Diagnostics;

namespace nHetmanowGenetycznie
{
    internal class Program
    {
        public static void PokazRezultat(byte[] rezultat, Stopwatch stoper)
        {
            Console.Beep();
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
                    }
                    else Console.Write("  ");
                }

                Console.Write("\n");
            }

            Console.WriteLine("Czas: " + stoper.Elapsed); //zmienna z czasem);
        }

        public static void Main()
        {           
        
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Podaj liczbę iteracji algorytmu genetycznego: ");
            int liczbaIteracji = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Podaj rozmiar populacji: ");
            int rozmiarPopulacji = Convert.ToInt32(Console.ReadLine());
            
            
            Hetmani problemHetmani = new Hetmani(rozmiarPopulacji);
            Stopwatch stoper = Stopwatch.StartNew();   
            var rezultat =  problemHetmani.Szukaj(liczbaIteracji);
            stoper.Stop();
            PokazRezultat(rezultat, stoper);        
        }
    }
}
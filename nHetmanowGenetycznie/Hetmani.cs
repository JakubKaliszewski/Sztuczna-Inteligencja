using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace nHetmanowGenetycznie
{
    public class Hetmani : AlgorytmGenetyczny<byte[]>
    {
        public Hetmani(int rozmiar)
        {
            RozmiarPopulacji = rozmiar;
        }

        protected override byte[][] LosowaPopulacja(int rozmiar)
        {
            byte[][] zwracanaPopulacja = new byte[rozmiar][];
            byte[] przykladowyOsobnik = new byte[8];
            Random losowa = new Random();

            for (int osobnik = 0; osobnik < rozmiar; osobnik++)
            {
                for (int i = 0; i < rozmiar; i++)
                {
                    przykladowyOsobnik[i] = (byte)losowa.Next(0, 8);
                }

                zwracanaPopulacja[osobnik] = przykladowyOsobnik;
            }

            return zwracanaPopulacja;
        }

        protected override int Koniec(bool bestPossible, float[] przystosowanie)
        {
            if (bestPossible == false)
            {
                for (int index = 0; index < przystosowanie.Length; index++)
                {
                    if (przystosowanie[index] == 1.0) return index;
                }

                return -1;
            }

            int maxIndex = 0;
            for (int index = 0; index < przystosowanie.Length; index++)
            {
                if (przystosowanie[index] > przystosowanie[maxIndex])
                {
                    maxIndex = index;
                }
            }

            return maxIndex;
        }

        protected override float Przystosowanie(byte[] osobnik)
        {
            int maxSzachowan = 56;

            int rozmiarOsobnika = osobnik.Length;

            int szachowania = 0;
            szachowania += SprawdzPoziomo(osobnik);
            for (byte kolumna = 0; kolumna < rozmiarOsobnika; kolumna++)
            {
                for (int wartosc = 1; wartosc + kolumna < rozmiarOsobnika; wartosc++) //skos prawo
                {
                    if (osobnik[kolumna + wartosc] == osobnik[kolumna] + wartosc)
                        szachowania++;
                    if (osobnik[kolumna + wartosc] == osobnik[kolumna] - wartosc)
                        szachowania++;
                }

                for (int wartosc = 1; kolumna - wartosc > 0; wartosc++) //skos w lewo
                {
                    if (osobnik[kolumna - wartosc] == osobnik[kolumna] + wartosc)
                        szachowania++;
                    if (osobnik[kolumna - wartosc] == osobnik[kolumna] - wartosc)
                        szachowania++;
                }
            }

            return maxSzachowan - szachowania / maxSzachowan;
        }

        private int SprawdzPoziomo(byte[] osobnik)
        {
            int szachowania = 0;
            byte rozmiarOsobnika = (byte) osobnik.Length;
            List<byte> odwiedzone = new List<byte>();

            for (int kolumna = 0; kolumna < rozmiarOsobnika; kolumna++)
            {
                if (odwiedzone.Contains(osobnik[kolumna])) szachowania++;
                odwiedzone.Add(osobnik[kolumna]);
            }

            return szachowania;
        }

        protected override void Krzyzuj(byte[] osobnik1, byte[] osobnik2, out byte[] nowyOsobnik1,
            out byte[] nowyOsobnik2)
        {
            throw new NotImplementedException();
        }

        protected override byte[] Mutacja(byte[] osobnik)
        {
            throw new NotImplementedException();
        }
    }
}
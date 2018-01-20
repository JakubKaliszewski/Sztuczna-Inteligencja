using System;
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
            byte[] przykladowyOsobnik = {0, 1, 2, 3, 4, 5, 6, 7};
            Random losowa = new Random();
            const int iloscLosowan = 30;

            for (int osobnik = 0; osobnik < rozmiar; osobnik++)
            {
                for (int i = 0; i < iloscLosowan; i++)
                {
                    byte tymczasowaDoZamiany;
                    int wylosowana1, wylosowana2;

                    wylosowana1 = losowa.Next(1, 8);
                    wylosowana2 = losowa.Next(1, 8);

                    tymczasowaDoZamiany = przykladowyOsobnik[wylosowana1];
                    przykladowyOsobnik[wylosowana1] = przykladowyOsobnik[wylosowana2];
                    przykladowyOsobnik[wylosowana2] = tymczasowaDoZamiany;
                    zwracanaPopulacja[osobnik] = przykladowyOsobnik;
                }
            }
        
            return zwracanaPopulacja;
        }

        protected override byte[] Koniec(bool bestPossible = false)
        {
            throw new NotImplementedException();
        }

        protected override float Przystosowanie(byte[] osobnik)
        {
            throw new NotImplementedException();
        }

        protected override void Krzyzuj(byte[] osobnik1, byte[] osobnik2, out byte[] nowyOsobnik1, out byte[] nowyOsobnik2)
        {
            throw new NotImplementedException();
        }

        protected override byte[] Mutacja(byte[] osobnik)
        {
            throw new NotImplementedException();
        }
    }
}
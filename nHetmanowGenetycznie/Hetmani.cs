using System;

namespace nHetmanowGenetycznie
{
    public class Hetmani : AlgorytmGenetyczny<byte>
    {
        protected override byte[] LosowaPopulacja(int rozmiar)
        {
            byte[] zwracanaPopulacja = {0,1,2,3,4,5,6,7};
            Random losowa = new Random();
            const int iloscLosowan = 30;

            
            for (int i = 0; i < iloscLosowan; i++)
            {
                byte tymczasowaDoZamiany;
                int wylosowana1, wylosowana2;
                
                wylosowana1 = losowa.Next(1, 8);
                wylosowana2 = losowa.Next(1, 8);

                tymczasowaDoZamiany = zwracanaPopulacja[wylosowana1];
                zwracanaPopulacja[wylosowana1] = zwracanaPopulacja[wylosowana2];
                zwracanaPopulacja[wylosowana2] = tymczasowaDoZamiany;
            }

            return zwracanaPopulacja;
        }

        protected override byte Koniec(bool bestPossible = false)
        {
            throw new NotImplementedException();
        }

        protected override float Przystosowanie(byte osobnik)
        {
            throw new NotImplementedException();
        }

        protected override void Krzyzuj(byte osobnik1, byte osobnik2, out byte nowyOsobnik1, out byte nowyOsobnik2)
        {
            throw new NotImplementedException();
        }

        protected override byte Mutacja(byte osobnik)
        {
            throw new NotImplementedException();
        }
    }
}
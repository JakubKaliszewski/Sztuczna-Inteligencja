using System;
using System.Collections.Generic;

namespace MapaRumuniiOdleglosciLiniaProsta
{
    public class Map
    {
        private List<string> NamesList;
        private List<Tuple<int, int>> CoordinatesList;
        public List<City> Cities;

        public Map()
        {
            Console.WriteLine("Konstruktor!");
            Cities = new List<City>();
            NamesList = new List<string>()
            {
                "Oradea",
                "Zerind",
                "Sibiu",
                "Arad",
                "Timisoara",
                "Lugoj",
                "Mehadia",
                "Drobeta",
                "Craiova",
                "Rimnicu Vilcea",
                "Pitesti",
                "Fagaras",
                "Bucharest",
                "Giurgiu",
                "Urziceni",
                "Hirsova",
                "Eforie",
                "Vaslui",
                "Iasi",
                "Neamt"
            };

            CoordinatesList = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(135, 17),
                new Tuple<int, int>(94, 84),
                new Tuple<int, int>(62, 154),
                new Tuple<int, int>(70, 294),
                new Tuple<int, int>(189, 346),
                new Tuple<int, int>(193, 419),
                new Tuple<int, int>(189, 487),
                new Tuple<int, int>(341, 506),
                new Tuple<int, int>(261, 212),
                new Tuple<int, int>(306, 296),
                new Tuple<int, int>(436, 225),
                new Tuple<int, int>(459, 367),
                new Tuple<int, int>(597, 437),
                new Tuple<int, int>(552, 535),
                new Tuple<int, int>(604, 72),
                new Tuple<int, int>(723, 126),
                new Tuple<int, int>(785, 236),
                new Tuple<int, int>(698, 395),
                new Tuple<int, int>(827, 399),
                new Tuple<int, int>(878, 496)
            };

            //Oradea 135,17
            //Zerind 94,84
            //Arad 62,154
            //Timisoara 70,294
            //Lugoj 189,346
            //Mehadia 193,419
            //Doboreta 189,487
            //Craiova 341,506
            //Sibiu 261,212
            //Rimnicu Vilcea 306,296
            //Fagaras 436,225
            //Pitesti 459,367
            //Bucharest 597, 437
            //Giurgiu 552,535
            //Neamt 604,72
            //Iasi 723,126
            //Vaslui 785,236
            //Urziceni 698,395
            //Hirsova 827,399
            //Eforie 878,496

            for (int i = 0; i < NamesList.Count; i++)
            {
                string name = NamesList[i];
                int x = CoordinatesList[i].Item1;
                int y = CoordinatesList[i].Item2;
                Cities.Add(new City(name, x, y));
            }

           //Oradea
            Cities[0].AddNeighbor(Cities[1], 71);
            Cities[0].AddNeighbor(Cities[2], 151);

            //Zerind
            Cities[1].AddNeighbor(Cities[0], 71);
            Cities[1].AddNeighbor(Cities[3], 75);

            //Sibiu
            Cities[2].AddNeighbor(Cities[0], 151);
            Cities[2].AddNeighbor(Cities[3], 140);
            Cities[2].AddNeighbor(Cities[11], 99);
            Cities[2].AddNeighbor(Cities[9], 80);

            //Arad
            Cities[3].AddNeighbor(Cities[1], 75);
            Cities[3].AddNeighbor(Cities[2], 140);
            Cities[3].AddNeighbor(Cities[4], 118);

            //Timisoara
            Cities[4].AddNeighbor(Cities[3], 118);
            Cities[4].AddNeighbor(Cities[5], 111);

            //Lugoj
            Cities[5].AddNeighbor(Cities[4], 111);
            Cities[5].AddNeighbor(Cities[6], 70);

            //Mehadia
            Cities[6].AddNeighbor(Cities[5], 70);
            Cities[6].AddNeighbor(Cities[7], 75);

            //Drobeta
            Cities[7].AddNeighbor(Cities[6], 75);
            Cities[7].AddNeighbor(Cities[8], 120);

            //Craiova
            Cities[8].AddNeighbor(Cities[7], 120);
            Cities[8].AddNeighbor(Cities[9], 146);
            Cities[8].AddNeighbor(Cities[10], 138);

            //Rimnicu Vilcea
            Cities[9].AddNeighbor(Cities[8], 146);
            Cities[9].AddNeighbor(Cities[10], 97);
            Cities[9].AddNeighbor(Cities[2], 80);

            //Pitesti
            Cities[10].AddNeighbor(Cities[8], 138);
            Cities[10].AddNeighbor(Cities[9], 97);
            Cities[10].AddNeighbor(Cities[12], 101);

            //Fagaras
            Cities[11].AddNeighbor(Cities[12], 211);
            Cities[11].AddNeighbor(Cities[2], 99);

            //Bucharest
            Cities[12].AddNeighbor(Cities[11], 211);
            Cities[12].AddNeighbor(Cities[13], 90);
            Cities[12].AddNeighbor(Cities[14], 85);

            //Giurgiu
            Cities[13].AddNeighbor(Cities[12], 90);

            //Urziceni
            Cities[14].AddNeighbor(Cities[12], 85);
            Cities[14].AddNeighbor(Cities[15], 98);
            Cities[14].AddNeighbor(Cities[17], 142);

            //Hirsova
            Cities[15].AddNeighbor(Cities[14], 98);
            Cities[15].AddNeighbor(Cities[16], 86);

            //Eforie
            Cities[16].AddNeighbor(Cities[15], 86);

            //Vaslui
            Cities[17].AddNeighbor(Cities[14], 142);
            Cities[17].AddNeighbor(Cities[18], 92);

            //Iasi
            Cities[18].AddNeighbor(Cities[17], 92);
            Cities[18].AddNeighbor(Cities[19], 87);

            //Neamt
            Cities[19].AddNeighbor(Cities[18], 87);
        }
    }
}
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
                new Tuple<int, int>(135,17),
                new Tuple<int, int>(94,84),
                new Tuple<int, int>(62,154),
                new Tuple<int, int>(70,294),
                new Tuple<int, int>(189,346),
                new Tuple<int, int>(193,419),
                new Tuple<int, int>(189,487),
                new Tuple<int, int>(341,506),
                new Tuple<int, int>(261,212),
                new Tuple<int, int>(306,296),
                new Tuple<int, int>(436,225),
                new Tuple<int, int>(459,367),
                new Tuple<int, int>(597,437),
                new Tuple<int, int>(552,535),
                new Tuple<int, int>(604,72),
                new Tuple<int, int>(723,126),
                new Tuple<int, int>(785,236),
                new Tuple<int, int>(698,395),
                new Tuple<int, int>(827,399),
                new Tuple<int, int>(878,496)
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
            Cities[0].AddNeighbor(Cities[1]);
            Cities[0].AddNeighbor(Cities[2]);

            //Zerind
            Cities[1].AddNeighbor(Cities[0]);
            Cities[1].AddNeighbor(Cities[3]);

            //Sibiu
            Cities[2].AddNeighbor(Cities[0]);
            Cities[2].AddNeighbor(Cities[3]);
            Cities[2].AddNeighbor(Cities[11]);
            Cities[2].AddNeighbor(Cities[9]);

            //Arad
            Cities[3].AddNeighbor(Cities[1]);
            Cities[3].AddNeighbor(Cities[2]);
            Cities[3].AddNeighbor(Cities[4]);

            //Timisoara
            Cities[4].AddNeighbor(Cities[3]);
            Cities[4].AddNeighbor(Cities[5]);

            //Lugoj
            Cities[5].AddNeighbor(Cities[4]);
            Cities[5].AddNeighbor(Cities[6]);

            //Mehadia
            Cities[6].AddNeighbor(Cities[5]);
            Cities[6].AddNeighbor(Cities[7]);

            //Drobeta
            Cities[7].AddNeighbor(Cities[6]);
            Cities[7].AddNeighbor(Cities[8]);

            //Craiova
            Cities[8].AddNeighbor(Cities[7]);
            Cities[8].AddNeighbor(Cities[9]);
            Cities[8].AddNeighbor(Cities[10]);

            //Rimnicu Vilcea
            Cities[9].AddNeighbor(Cities[8]);
            Cities[9].AddNeighbor(Cities[10]);
            Cities[9].AddNeighbor(Cities[2]);

            //Pitesti
            Cities[10].AddNeighbor(Cities[8]);
            Cities[10].AddNeighbor(Cities[9]);
            Cities[10].AddNeighbor(Cities[12]);

            //Fagaras
            Cities[11].AddNeighbor(Cities[12]);
            Cities[11].AddNeighbor(Cities[2]);

            //Bucharest
            Cities[12].AddNeighbor(Cities[11]);
            Cities[12].AddNeighbor(Cities[13]);
            Cities[12].AddNeighbor(Cities[14]);

            //Giurgiu
            Cities[13].AddNeighbor(Cities[12]);

            //Urziceni
            Cities[14].AddNeighbor(Cities[12]);
            Cities[14].AddNeighbor(Cities[15]);
            Cities[14].AddNeighbor(Cities[17]);

            //Hirsova
            Cities[15].AddNeighbor(Cities[14]);
            Cities[15].AddNeighbor(Cities[16]);

            //Eforie
            Cities[16].AddNeighbor(Cities[15]);

            //Vaslui
            Cities[17].AddNeighbor(Cities[14]);
            Cities[17].AddNeighbor(Cities[18]);

            //Iasi
            Cities[18].AddNeighbor(Cities[17]);
            Cities[18].AddNeighbor(Cities[19]);

            //Neamt
            Cities[19].AddNeighbor(Cities[18]);
        }
    }
}


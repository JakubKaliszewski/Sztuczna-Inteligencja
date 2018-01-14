using System;

namespace MapaRumuniiOdleglosciLiniaProsta
{
    internal class Program
    {
        private static void DisplaySolution(MapOfRomania problem, Node<City> result)
        {
            if (result == null)
            {
                Console.WriteLine("\nNo solutions!");
            }
            else
            {
                Console.WriteLine("\nRoad: \nSteps: " + TreeSearch.countOfSteps);
                result.ShowRoad(problem.ShowState);
            }
        }

        public static void Main(string[] args)
        {
            Map initMap = new Map();
            Console.WriteLine("Podaj miasto startowe: ");
            string startCity = Console.ReadLine();
            Console.WriteLine("Podaj miasto docelowe: ");
            string destinyCity = Console.ReadLine();
            Console.WriteLine("Wybierz metodę rozwiązania:\n1. wgłąb,\n2. wszerz,\n3. best-first search,\n4. A*.");
            int choose = int.Parse(Console.ReadLine());
            MapOfRomania problem = new MapOfRomania(initMap, startCity, destinyCity);
            Node<City> result;

            switch (choose)
            {
                case 1:
                {
                    StackFringe<Node<City>> stackSolution = new StackFringe<Node<City>>();
                    result = TreeSearch.TreeSearchMetod(problem, stackSolution, GetDistance);
                    DisplaySolution(problem, result);
                    break;
                }

                case 2:
                {
                    QueueFringe<Node<City>> queueSolution = new QueueFringe<Node<City>>();
                    result = TreeSearch.TreeSearchMetod(problem, queueSolution, GetDistance);
                    DisplaySolution(problem, result);
                    break;
                }

                case 3:
                {
                    QueueFringe<Node<City>> queueSolution = new QueueFringe<Node<City>>();
                    result = TreeSearch.TreeSearchPriorityQueue(problem, queueSolution, GetDistance);
                    DisplaySolution(problem, result);
                    break;
                }

                case 4:
                {
                    QueueFringe<Node<City>> aStarSolution = new QueueFringe<Node<City>>();
                    result = TreeSearch.TreeSearchAStar(problem, aStarSolution, GetDistance);
                    DisplaySolution(problem, result);
                    break;
                }
            }
        }

        private static int GetDistance(City city, City city1)
        {
            int distance = 0;
            foreach (var neighbor in city.neighborsCities)
            {
                if (neighbor.city.Name == city1.Name)
                {
                    distance = neighbor.distance;
                }
            }
                      
            return distance;
        }
    }
}
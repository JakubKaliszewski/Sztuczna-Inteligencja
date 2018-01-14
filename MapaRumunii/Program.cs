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
            MapOfRomania problem = new MapOfRomania(initMap, startCity, destinyCity);
            Node<City> result;

            Console.WriteLine("\nSolving with StackFringe...");
            StackFringe<Node<City>> stackSolution = new StackFringe<Node<City>>();
            result = TreeSearch.TreeSearchMetod(problem, stackSolution, GetDistance);
            DisplaySolution(problem, result);

            Console.WriteLine("\nSolving with QueueFringe...");
            QueueFringe<Node<City>> queueSolution = new QueueFringe<Node<City>>();
            result = TreeSearch.TreeSearchMetod(problem, queueSolution, GetDistance);
            DisplaySolution(problem, result);

            Console.WriteLine("\nSolving with PriorityQueueFringe...");
            QueueFringe<Node<City>> queuePrioritySolution = new QueueFringe<Node<City>>();
            result = TreeSearch.TreeSearchPriorityQueue(problem, queuePrioritySolution, GetDistance);
            DisplaySolution(problem, result);

            Console.WriteLine("\nSolving with AStarFringe...");
            QueueFringe<Node<City>> aStarSolution = new QueueFringe<Node<City>>();
            result = TreeSearch.TreeSearchAStar(problem, aStarSolution, GetDistance);
            DisplaySolution(problem, result);
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
using System;
using System.Diagnostics;

namespace MapaRumunii
{
    internal class Program
    {
        private static void DisplaySolution(MapOfRomania problem, Node<City> result, Stopwatch stoper)
        {
            if (result == null)
            {
                Console.WriteLine("\nNo solutions!");
            }
            else
            {
                Console.WriteLine("\nRoad: \nSteps: " + result.CountOfSteps);
                result.ShowRoad(problem.ShowState);
                Console.WriteLine("Time: " + stoper.Elapsed.Milliseconds / 1000.0 + " s"); //zmienna z czasem);
                Console.WriteLine("---------------------------------------------------------------------------");
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
            Stopwatch stoper = Stopwatch.StartNew();
            result = TreeSearch.TreeSearchMetod(problem, stackSolution, GetDistance);
            stoper.Stop();
            DisplaySolution(problem, result, stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with QueueFringe...");
            QueueFringe<Node<City>> queueSolution = new QueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch.TreeSearchMetod(problem, queueSolution, GetDistance);
            stoper.Stop();
            DisplaySolution(problem, result,stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with PriorityQueueFringe...");
            //QueueFringe<Node<City>> queuePrioritySolution = new QueueFringe<Node<City>>();
            PriorityQueueFringe<Node<City>> queuePrioritySolution =new PriorityQueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch.TreeSearchPriorityQueue(problem, queuePrioritySolution, GetDistance);
            stoper.Stop();
            DisplaySolution(problem, result,stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with AStarFringe...");
            QueueFringe<Node<City>> aStarSolution = new QueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch.TreeSearchAStar(problem, aStarSolution, GetDistance);
            stoper.Stop();
            DisplaySolution(problem, result,stoper);
            stoper.Reset();
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
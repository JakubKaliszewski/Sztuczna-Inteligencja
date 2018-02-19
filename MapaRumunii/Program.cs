using System;
using System.Diagnostics;

namespace MapaRumunii
{
    internal class Program
    {
        private static void DisplaySolution(MapOfRomania problem, Node<City> result, Stopwatch stoper)
        {
            Console.Beep();
            if (result == null)
            {
                Console.WriteLine("\nBrak rozwiązań!");
            }
            else
            {
                Console.WriteLine("Czas poszukiwania rozwiązania: " + stoper.Elapsed);//zmienna z czasem
                Console.WriteLine("Liczba kroków do znalezienia rozwiązania: " + TreeSearch<City>.CountOfSteps);
                Console.WriteLine("Otrzymana Droga:");
                result.ShowRoad(problem.ShowState);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public static void Main()
        {
            var initMap = new Map();
            Console.WriteLine("Podaj miasto startowe: ");
            var startCity = Console.ReadLine();
            Console.WriteLine("Podaj miasto docelowe: ");
            var destinyCity = Console.ReadLine();
            var problem = new MapOfRomania(initMap, startCity, destinyCity);
            Node<City> result;

            Console.WriteLine("\nRozwiązywanie za pomocą Stosu...");
            var stackSolution = new StackFringe<Node<City>>();
            var stoper = Stopwatch.StartNew();
            result = TreeSearch<City>.TreeSearchMetod(problem, stackSolution, Method.Stack);
            stoper.Stop();
            DisplaySolution(problem, result, stoper);
            stoper.Reset();
            TreeSearch<City>.CountOfSteps = 0;

            Console.WriteLine("\nRozwiązywanie za pomocą kolejki...");
            var queueSolution = new QueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch<City>.TreeSearchMetod(problem, queueSolution, Method.Queue);
            stoper.Stop();
            DisplaySolution(problem, result, stoper);
            stoper.Reset();
            TreeSearch<City>.CountOfSteps = 0;

            Console.WriteLine("\nRozwiązywanie za pomocą BestFirstSearch (odległość w linii prostej)...");
            var queuePrioritySolution = new PriorityQueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch<City>.TreeSearchMetod(problem, queuePrioritySolution, Method.PriorityQueue);
            stoper.Stop();
            DisplaySolution(problem, result, stoper);
            stoper.Reset();
            TreeSearch<City>.CountOfSteps = 0;

            Console.WriteLine(
                "\nRozwiązywanie za pomocą A* (odległość w linii prostej, pokonana odległość, odległość krawędziowa)...");
            var aStarSolution = new PriorityQueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch<City>.TreeSearchMetod(problem, aStarSolution, Method.AStar);
            stoper.Stop();
            DisplaySolution(problem, result, stoper);
            stoper.Reset();
            TreeSearch<City>.CountOfSteps = 0;
        }

        private enum Method
        {
            Stack,
            Queue,
            PriorityQueue,
            AStar
        }
    }
}
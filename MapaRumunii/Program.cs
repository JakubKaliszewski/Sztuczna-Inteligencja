using System;
using System.Collections;
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
                Console.WriteLine("Czas poszukiwania rozwiązania: " + stoper.Elapsed.Milliseconds / 1000.0 + " s"); //zmienna z czasem);
                Console.WriteLine("Liczba kroków do znalezienia rozwiązania: " + TreeSearch<City>.CountOfSteps);
                Console.WriteLine("Liczba kroków dla rozwiązania: " + result.StepsForSolution);
                Console.WriteLine("Droga:");
                result.ShowRoad(problem.ShowState);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        enum Method
        {
            Stack,
            Queue,
            PriorityQueue,
            AStar
        };

        public static void Main()
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
            result = TreeSearch<City>.TreeSearchMetod(problem, stackSolution,Method.Stack);
            stoper.Stop();
            DisplaySolution(problem, result, stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with QueueFringe...");
            QueueFringe<Node<City>> queueSolution = new QueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch<City>.TreeSearchMetod(problem, queueSolution, Method.Queue);
            stoper.Stop();
            DisplaySolution(problem, result,stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with BestFirstSearch...");
            PriorityQueueFringe<Node<City>> queuePrioritySolution =new PriorityQueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch<City>.TreeSearchMetod(problem, queuePrioritySolution, Method.PriorityQueue);
            stoper.Stop();
            DisplaySolution(problem, result,stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with AStarFringe...");
            QueueFringe<Node<City>> aStarSolution = new QueueFringe<Node<City>>();
            stoper.Start();
            result = TreeSearch<City>.TreeSearchMetod(problem, aStarSolution, Method.AStar);
            stoper.Stop();
            DisplaySolution(problem, result,stoper);
            stoper.Reset();
        }

    }
}
using System;
using System.Diagnostics;
using System.Threading;
using nHetmans;

namespace Przesuwanka
{
    internal class Program
    {
        public static void DisplaySolution(Przesuwanka przesuwanka, Node<byte[,]> result, TimeSpan stoper)
        {
            Console.Beep();
            Console.WriteLine();
            showState(przesuwanka.InitialState);

            if (result == null)
            {
                Console.WriteLine("\nBrak rozwiązań!");
            }
            else
            {
                Console.WriteLine("Rozwiązanie:");
                showState(result.StateOfNode);
                Console.WriteLine("Czas poszukiwania rozwiązania: " + stoper); //zmienna z czasem);
                Console.WriteLine("Liczba kroków do znalezienia rozwiązania: " + przesuwanka.CountOfSteps);
                Console.WriteLine("\nLiczba kroków rozwiązania: " + result.StepsForSolution);
                Console.WriteLine("---------------------------------------------------------------------------");
                przesuwanka.CountOfSteps = 0;
            }
        }

        private static void showState(byte[,] table)
        {
            for (var i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (var j = 0; j < table.GetLength(1); j++) Console.Write(table[i, j] + "\t");

                Console.Write(" ]\n");
            }
        }


        private enum Method
        {
            Stack,
            Queue,
            PriorityQueue,
            AStar
        }


        private static void Main()
        {
            Console.WriteLine("Podaj wymiar przesuwanki: ");
            var size = byte.Parse(Console.ReadLine());
            Console.WriteLine("Podaj wymiar ilość przelosowań: ");
            var numberOfMixes = byte.Parse(Console.ReadLine());

            byte[,] initial =
            {
                {1, 8, 2},
                {0, 4, 3},
                {7, 6, 5},
            };

            //var problemPrzesuwanka = new Przesuwanka(size,initial);
            var problemPrzesuwanka = new Przesuwanka(size, numberOfMixes);
            problemPrzesuwanka.showState(problemPrzesuwanka.InitialState);
            Node<byte[,]> result;
            Stopwatch stoper = new Stopwatch();
            TimeSpan elapsed = new TimeSpan();


            Console.WriteLine("\nRozwiązywanie za pomocą BestFirstSearch (ilość płytek na złej pozycji)...");
            var priorityQueueSolution = new PriorityQueueFringe<Node<byte[,]>>();
            stoper.Start();
            result = TreeSearch<byte[,]>.TreeSearchMethod(problemPrzesuwanka, priorityQueueSolution,
                Method.PriorityQueue);
            stoper.Stop();
            elapsed = stoper.Elapsed;
            DisplaySolution(problemPrzesuwanka, result, elapsed);
            stoper.Reset();

            Console.WriteLine(
                "\nRozwiązywanie za pomocą A* (ilość płytek na złej pozycji + odległość Manhattan dla każdej płytki od pozycji, gdzie powinna się znajdować + ilość kroków, im ich mniej tym stan jest bardziej obiecujący)...");
            var AStarSolution = new PriorityQueueFringe<Node<byte[,]>>();
            stoper.Start();
            result = TreeSearch<byte[,]>.TreeSearchMethod(problemPrzesuwanka, AStarSolution, Method.AStar);
            stoper.Stop();
            elapsed = stoper.Elapsed;
            DisplaySolution(problemPrzesuwanka, result, elapsed);
            stoper.Reset();

            Console.WriteLine("\nRozwiązywanie za pomocą Stosu...");
            var stackSolution = new StackFringe<Node<byte[,]>>(); //DFS
            stoper.Start();
            result = TreeSearch<byte[,]>.TreeSearchMethod(problemPrzesuwanka, stackSolution, Method.Stack);
            stoper.Stop();
            elapsed = stoper.Elapsed;
            stoper.Reset();
            DisplaySolution(problemPrzesuwanka, result, elapsed);

            Console.WriteLine("\nRozwiązywanie za pomocą kolejki...");
            var queueSolution = new QueueFringe<Node<byte[,]>>(); //BFS
            stoper.Start();
            result = TreeSearch<byte[,]>.TreeSearchMethod(problemPrzesuwanka, queueSolution, Method.Queue);
            stoper.Stop();
            elapsed = stoper.Elapsed;
            DisplaySolution(problemPrzesuwanka, result, elapsed);
            stoper.Reset();
        }
    }
}
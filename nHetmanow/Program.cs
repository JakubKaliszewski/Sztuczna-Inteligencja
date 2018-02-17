using System;
using System.Diagnostics;
using System.Text;

namespace nHetmans
{
    internal class Program
    {
        public static void DisplaySolution(Hetmans problemHetmans, Node<byte[]> result, Stopwatch stoper)
        {
            problemHetmans.ShowState(problemHetmans.InitialState);

            if (result == null)
            {
                Console.WriteLine("\nBrak Rozwiązań!");
            }
            else
            {
                Console.WriteLine("Rozwiązanie:");
                ShowState(result.StateOfNode);
                Console.WriteLine("Czas poszukiwania rozwiązania: " + stoper.Elapsed.Milliseconds / 1000.0 +
                                  " s"); //zmienna z czasem);
                Console.WriteLine("Liczba kroków do znalezienia rozwiązania: " + TreeSearch<byte[]>.CountOfSteps);
                Console.WriteLine("\nLiczba kroków rozwiązania: " + result.StepsForSolution);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public static void ShowState(byte[] state)
        {
            Console.WriteLine();
            var size = (byte) state.GetLength(0);

            Console.Write("  ");
            for (byte i = 0; i < size; i++) // 0 1 2 3 4 5 6 7 - naglowek 
                Console.Write(i + " ");

            Console.Write("\n"); // przejscie do wlasciwego wyswietlania tresci

            for (byte row = 0; row < size; row++)
            {
                Console.Write(row + " ");
                for (byte column = 0; column < size; column++)
                    if (state[column] == row)
                        Console.Write('\u25A0' + " ");
                    else
                        Console.Write("  ");

                Console.Write("\n");
            }
        }

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Podaj wymiar szachownicy: ");
            var size = byte.Parse(Console.ReadLine());
            var problemHetmans = new Hetmans(size);
            Node<byte[]> result;


            Console.WriteLine("\nRozwiązywanie za pomocą Stosu...");
            ;
            var stackSolution = new StackFringe<Node<byte[]>>();
            var stoper = Stopwatch.StartNew();
            result = TreeSearch<byte[]>.TreeSearchMethod(problemHetmans, stackSolution, Method.Stack);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            stoper.Reset();
            TreeSearch<byte[]>.CountOfSteps = 0;

            Console.WriteLine("\nRozwiązywanie za pomocą kolejki...");
            var queueSolution = new QueueFringe<Node<byte[]>>();
            stoper.Start();
            result = TreeSearch<byte[]>.TreeSearchMethod(problemHetmans, queueSolution, Method.Queue);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            stoper.Reset();
            TreeSearch<byte[]>.CountOfSteps = 0;

            Console.WriteLine("\nRozwiązywanie za pomocą BestFirstSearch (ilość konfliktów)...");
            var priorityQueueSolution = new PriorityQueueFringe<Node<byte[]>>();
            stoper.Start();
            result = TreeSearch<byte[]>.TreeSearchMethod(problemHetmans, priorityQueueSolution, Method.PriorityQueue);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            stoper.Reset();
            TreeSearch<byte[]>.CountOfSteps = 0;

            Console.WriteLine("\nRozwiązywanie za pomocą A* (ilości konfliktów + ilość kroków)...");
            var AStarSolution = new PriorityQueueFringe<Node<byte[]>>();
            stoper.Start();
            result = TreeSearch<byte[]>.TreeSearchMethod(problemHetmans, AStarSolution, Method.AStar);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            TreeSearch<byte[]>.CountOfSteps = 0;
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
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace nHetmans
{
    internal class Program
    {
        public static void DisplaySolution(Hetmans problemHetmans, Node<byte[]> result, Stopwatch stoper)
        {
            problemHetmans.ShowState(problemHetmans.InitialState);

            if (result == null)
            {
                Console.WriteLine("\nNo solutions!");
            }
            else
            {
                Console.WriteLine("Goal State:");
                ShowState(result.StateOfNode);
                Console.WriteLine("\nSteps: " + result.CountOfSteps);
                Console.WriteLine("Time: " + stoper.Elapsed.Milliseconds / 1000.0 + " s"); //zmienna z czasem);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public static void ShowState(byte[] state)
        {
            Console.WriteLine();
            byte size = (byte) state.GetLength(0);

            Console.Write("  ");
            for (byte i = 0; i < size; i++) // 0 1 2 3 4 5 6 7 - naglowek 
            {
                Console.Write(i + " ");
            }

            Console.Write("\n"); // przejscie do wlasciwego wyswietlania tresci

            for (byte row = 0; row < size; row++)
            {
                Console.Write(row + " ");
                for (byte column = 0; column < size; column++)
                {
                    if (state[column] == row)
                    {
                        Console.Write('\u25A0' + " ");
                        //Console.Write('H' + " ");
                    }
                    else Console.Write("  ");
                }

                Console.Write("\n");
            }
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Podaj wymiar szachownicy: ");
            byte size = byte.Parse(Console.ReadLine());
            Hetmans problemHetmans = new Hetmans(size);
            Node<byte[]> result;


            Console.WriteLine("\nSolving with StackFringe...");
            StackFringe<Node<byte[]>> stackSolution = new StackFringe<Node<byte[]>>();
            Stopwatch stoper = Stopwatch.StartNew();
            result = TreeSearch.TreeSearchMetod(problemHetmans, stackSolution);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with QueueFringe...");
            QueueFringe<Node<byte[]>> queueSolution = new QueueFringe<Node<byte[]>>();
            stoper.Start();
            result = TreeSearch.TreeSearchMetod(problemHetmans, queueSolution);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            stoper.Reset();
            
            
            Console.WriteLine("\nSolving with PriorityQueueFringe...");
            QueueFringe<Node<byte[]>> priorityQueueSolution = new QueueFringe<Node<byte[]>>();
            stoper.Start();
            result = TreeSearch.TreeSearchPriorityQueue(problemHetmans, priorityQueueSolution);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);
            stoper.Reset();
            
            Console.WriteLine("\nSolving with AStarFringe...");
            QueueFringe<Node<byte[]>> AStarSolution = new QueueFringe<Node<byte[]>>();
            stoper.Start();
            result = TreeSearch.TreeSearchAStar(problemHetmans, AStarSolution);
            stoper.Stop();
            DisplaySolution(problemHetmans, result, stoper);

        }
    }
}
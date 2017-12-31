using System;
using System.Collections.Generic;

namespace nHetmans
{
    internal class Program
    {
        public static void ShowState(byte[] state)
        {
            byte size = (byte)state.GetLength(0);

            Console.Write("  ");
            for (byte i = 0; i < size; i++)// 0 1 2 3 4 5 6 7 - naglowek 
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

            byte size = 8;
            
            Hetmans problemHetmans = new Hetmans(size);
            problemHetmans.ShowState(problemHetmans.InitialState);
            ///Metoda Szukania - kolejka
            QueueFringe<Node<byte[]>> queueSolution = new QueueFringe<Node<byte[]>>();
            
            
            var result = TreeSearch.TreeSearchWithQueue(problemHetmans, queueSolution);
            if (result == null)
            {
                Console.WriteLine("\nNo solutions!");
            }
            else
            {
                Console.WriteLine("\nGoal State: \n");
                ShowState(result.StateOfNode);
            }
            Console.ReadKey();
        }
    }
}
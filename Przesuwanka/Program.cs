using System;
using System.Diagnostics;

namespace Przesuwanka
{
    internal class Program
    {
        private static byte[,]
            generateState(byte size) //Metoda na wygenerowanie tablicy wielowymiarowej, [0...size*size]
        {
            var tmpTable = new byte[size, size];
            var posibleNumbers = new byte[size * size];

            for (var i = 0; i < size * size; i++) posibleNumbers[i] = (byte) (i + 1);

            posibleNumbers[size * size - 1] = 0;

            var IndexnumberInPosibleNumbers = 0;
            for (var i = 0; i < tmpTable.GetLength(0); i++)
            for (var j = 0; j < tmpTable.GetLength(1); j++)
            {
                tmpTable[i, j] = posibleNumbers[IndexnumberInPosibleNumbers];
                IndexnumberInPosibleNumbers++;
            }

            return tmpTable;
        }

        private static byte[,] mixGeneratedState(byte[,] table, byte numberOfMixes)
        {
            byte n1, m1, n2, m2, tmpValue;
            var rnd = new Random();

            for (byte i = 0; i < numberOfMixes; i++)
            {
                n1 = (byte) rnd.Next(0, table.GetLength(0));
                m1 = (byte) rnd.Next(0, table.GetLength(0));
                n2 = (byte) rnd.Next(0, table.GetLength(0));
                m2 = (byte) rnd.Next(0, table.GetLength(0));

                tmpValue = table[n1, m1];
                table[n1, m1] = table[n2, m2];
                table[n2, m2] = tmpValue;
            }

            return table;
        }

        public static void DisplaySolution(Przesuwanka przesuwanka, Node<byte[,]> result, Stopwatch stoper)
        {
            Console.WriteLine();
            showState(przesuwanka.InitialState);

            if (result == null)
            {
                Console.WriteLine("\nNo solutions!");
            }
            else
            {
                Console.WriteLine("\nGoal State: \n");
                showState(result.StateOfNode);
                Console.WriteLine("\nSteps: " + result.CountOfSteps);
                Console.WriteLine("Czas: " + stoper.Elapsed.Milliseconds / 1000.0 + " s"); //zmienna z czasem);
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

        private static void Main(string[] args)
        {
            Console.WriteLine("Podaj wymiar przesuwanki: ");
            var size = byte.Parse(Console.ReadLine());

            var initial = new byte[size, size];
            byte numberOfMixes = 5; //Parametr ilosci przemieszan
            var goal = new byte[size, size];


/*            byte[,] initial =
            {
                {4, 1, 3},
                {7, 2, 6},
                {5, 8, 0},
            };*/

            initial = mixGeneratedState(generateState(size), numberOfMixes);
            goal = generateState(size);
            showState(initial);


            var problemPrzesuwanka = new Przesuwanka(initial, goal);
            Node<byte[,]> result;

/*            Console.WriteLine("\nSolving with StackFringe...");
            var stackSolution = new StackFringe<Node<byte[,]>>();
            result = TreeSearch.TreeSearchMetod(problemPrzesuwanka, stackSolution);
            DisplaySolution(problemPrzesuwanka, result);
            TreeSearch.countOfSteps = 0;*/
            
            Console.WriteLine("\nSolving with QueueFringe...");
            
            var queueSolution = new QueueFringe<Node<byte[,]>>();
            Stopwatch stoper = System.Diagnostics.Stopwatch.StartNew();
            result = TreeSearch.TreeSearchMetod(problemPrzesuwanka, queueSolution);
            stoper.Stop();
            DisplaySolution(problemPrzesuwanka, result, stoper);
            stoper.Reset();

            Console.WriteLine("\nSolving with PriorityQueueFringe...");
            var priorityQueueSolution = new QueueFringe<Node<byte[,]>>();
            stoper.Start();
            result = TreeSearch.TreeSearchPriorityQueue(problemPrzesuwanka, priorityQueueSolution);
            DisplaySolution(problemPrzesuwanka, result,stoper);
            stoper.Stop();
            stoper.Reset();

            Console.WriteLine("\nSolving with AStarFringe...");
            var AStarSolution = new QueueFringe<Node<byte[,]>>();
            stoper.Start();
            result = TreeSearch.TreeSearchAStar(problemPrzesuwanka, AStarSolution);
            DisplaySolution(problemPrzesuwanka, result,stoper);
            stoper.Stop();
        }
    }
}
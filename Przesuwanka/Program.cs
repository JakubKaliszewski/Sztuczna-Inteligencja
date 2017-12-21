using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    class Program
    {
        static byte[,] generateState(byte size) //Metoda na wygenerowanie tablicy wielowymiarowej, [0...size*size]
        {
            byte[,] tmpTable = new byte[size, size];
            byte[] posibleNumbers = new byte[size * size];

            for (int i = 0; i < size * size; i++)
            {
                posibleNumbers[i] = (byte)(i+1);
            }
            posibleNumbers[size*size - 1] = 0;
            
            int IndexnumberInPosibleNumbers = 0;
            for (int i = 0; i < tmpTable.GetLength(0); i++)
            {
                for (int j = 0; j < tmpTable.GetLength(1); j++)
                {
                    tmpTable[i,j] = posibleNumbers[IndexnumberInPosibleNumbers];
                    IndexnumberInPosibleNumbers++;
                }
            }
            
            return tmpTable;
        }

        static byte[,] mixGeneratedState(byte[,] table, byte numberOfMixes)
        {
            byte n1,m1, n2,m2, tmpValue;
            Random rnd = new Random();

            for (byte i = 0; i < numberOfMixes; i++)
            {
                n1 = (byte)rnd.Next(0, table.GetLength(0));
                m1 = (byte)rnd.Next(0, table.GetLength(0));
                n2 = (byte)rnd.Next(0, table.GetLength(0));
                m2 = (byte)rnd.Next(0, table.GetLength(0));

                tmpValue = table[n1, m1];
                table[n1, m1] = table[n2, m2];
                table[n2, m2] = tmpValue;
            }
            
            return table;
        }

        static void showState(byte[,] table)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i,j]+"\t");
                }
                Console.Write(" ]\n");
            }
        }

        static void Main(string[] args)
        {
            byte size = 3;//Parametr rozmiaru przesuwanki
            byte numberOfMixes = 30;//Parametr ilosci przemieszan

            //byte[,] initial = new byte[size, size];
            byte[,] initial = 
            {
                {4,1,3},
                {7,2,6},
                {5,8,0},
            };
            byte[,] goal = new byte[size, size];


            //initial = mixGeneratedState(generateState(size), numberOfMixes);
            goal = generateState(size);

            Console.WriteLine("Initial State: \n");
            showState(initial);
            Console.WriteLine();

            
            Przesuwanka PrzesuwankaGame = new Przesuwanka(initial, goal);
            QueueFringe<Node<byte[,]>> queue = new QueueFringe<Node<byte[,]>>();
            //StackFringe<Node<byte[,]>> stack = new StackFringe<Node<byte[,]>>();

            var result = TreeSearch.TreeSearchWithQueue(PrzesuwankaGame, queue);
            if (result == null)
            {
                Console.WriteLine("\nBrak rozwiązań przesuwanki dla tego stanu początkowego");
            }
            else
            {
                Console.WriteLine("\nGoal State: \n");
                showState(result.StateOfNode);
            }
            Console.ReadKey();


        }
    }
}
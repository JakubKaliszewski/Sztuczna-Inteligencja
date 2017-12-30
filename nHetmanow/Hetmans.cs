using System;
using System.Collections.Generic;

namespace nHetmans
{
    public class Hetmans : IProblem<byte[]>
    {
        public byte[] InitialState { get; }
        public bool IsGoal(byte[] state)
        {
            throw new System.NotImplementedException();
        }

        public bool Compare(byte[] stateOfNode, byte[] checkingState)
        {
            throw new System.NotImplementedException();
        }

        public IList<byte[]> Expand(byte[] state)
        {
            throw new System.NotImplementedException();
        }

        public void showState(byte[] state)
        {
            byte size = (byte)state.GetLength(0);

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
                    }
                    else Console.Write("  ");
                }
                Console.Write("\n");
            }
        }
    }
}
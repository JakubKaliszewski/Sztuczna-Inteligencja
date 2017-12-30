using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
using System.Runtime.Remoting.Messaging;

namespace nHetmans
{
    public class Hetmans : IProblem<byte[]>
    {
        private byte[] initial;

        public Hetmans(byte[] initialState)
        {
            this.initial = initialState;
        }

        public byte[] InitialState
        {
            get { return this.initial; }
        }

        public bool IsGoal(byte[] state)
        {
            if (CountOfConflicts(state) == 0) return true;
            return false;
        }

        public int CountOfConflicts(byte[] state)
        {
            byte sizeOfState = (byte) state.Length;
            
            int conflicts = 0;
            conflicts += CheckHorizontally(state);
            for (byte column = 0; column < sizeOfState; column++)
            {
                for (int j = 1; j + column < sizeOfState; j++) //skos prawo
                {
                    if (state[column + j] == state[column] + j)
                        conflicts++;
                    if (state[column + j] == state[column] - j)
                        conflicts++;
                }
                for (int j = 1; column - j > 0; j++) //skos w lewo??
                {
                    if (state[column - j] == state[column] + j)
                        conflicts++;
                    if (state[column - j] == state[column] - j)
                        conflicts++;
                }
            }
            return conflicts;
        }

        public int CheckHorizontally(byte[] state)
        {
            int conflicts = 0;
            byte sizeOfState = (byte) state.Length;
            List<byte> visited = new List<byte>();

            for (int column = 0; column < sizeOfState; column++)
            {
                if (visited.Contains(state[column])) conflicts++;
                visited.Add(state[column]);
            }

            return conflicts;
        }

        public bool Compare(byte[] stateOfNode, byte[] checkingState)
        {
            throw new System.NotImplementedException();
        }

        public IList<byte[]> Expand(byte[] state)
        {
            throw new System.NotImplementedException();
        }

        public void ShowState(byte[] state)
        {
            byte size = (byte) state.GetLength(0);

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
                    }
                    else Console.Write("  ");
                }

                Console.Write("\n");
            }
        }
    }
}
using System;
using System.Collections.Generic;

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
            bool isFinal;
            byte sizeOfState = (byte) state.GetLength(0);
            if (CheckHorizontally(state) == false) return false;

            for (byte column = 0; column < sizeOfState; column++)
            {
                isFinal = CheckDiagonals(state, column);
                if (isFinal == false) return isFinal;
            }

            return isFinal;
        }

        public bool CheckDiagonals(byte[] state, byte testedIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckLeftDiagonal(byte[] state, byte testedIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckRightDiagonal(byte[] state, byte testedIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckHorizontally(byte[] state)
        {
            byte sizeOfState = (byte) state.GetLength(0);
            List<byte> visited = new List<byte>();

            for (int column = 0; column < sizeOfState; column++)
            {
                if (visited.Contains(state[column])) return false;
                visited.Add(state[column]);
            }

            return true;
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
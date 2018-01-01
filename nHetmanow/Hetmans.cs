using System;
using System.Collections.Generic;

namespace nHetmans
{
    public class Hetmans : IProblem<byte[]>
    {
        private byte[] initial;

        public Hetmans(byte size)
        {
            initial = GenerateState(size);
        }

        public byte[] InitialState
        {
            get { return initial; }
        }


        public byte[] GenerateState(byte size)
        {
            byte[] state = new byte[size];
            Random random = new Random();
            for (byte i = 0; i < size; i++)
            {
                state[i] = (byte) random.Next(0, size);
            }

            return state;
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
                for (int value = 1; value + column < sizeOfState; value++) //skos prawo
                {
                    if (state[column + value] == state[column] + value)
                        conflicts++;
                    if (state[column + value] == state[column] - value)
                        conflicts++;
                }

                for (int value = 1; column - value > 0; value++) //skos w lewo??
                {
                    if (state[column - value] == state[column] + value)
                        conflicts++;
                    if (state[column - value] == state[column] - value)
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
            byte size = (byte) stateOfNode.Length;
            for (byte column = 0; column < size; column++)
            {
                if (stateOfNode[column] != checkingState[column]) return false;
            }

            return true;
        }

        public void ShowState(byte[] state)
        {
            Console.WriteLine();
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

        public IList<byte[]> Expand(byte[] state)
        {
            List<byte[]> possibleStates = createStatesToExpand(state);
            return possibleStates;
        }

        private List<byte[]> createStatesToExpand(byte[] state)
        {
            byte size = (byte) state.Length;
            List<byte[]> possibleStates = GeneratePosisbleStates(state);

            return possibleStates;
        }

        private List<byte[]> GeneratePosisbleStates(byte[] state)
        {
            byte size = (byte) state.Length;
            List<byte[]> returnedList = new List<byte[]>();
            bool changed = false;
            int initialCountOfConflicts = CountOfConflicts(state);
            int conflictsForColumn, tempCountOfConflicts;
            byte numberOfTheBestRow = 0;

            for (byte column = 0; column < size; column++)
            {
                byte[] actualState = CopyState(state);
                conflictsForColumn = initialCountOfConflicts;
                tempCountOfConflicts = 0;

                for (byte row = 0; row < size; row++)
                {
                    actualState[column] = row;
                    tempCountOfConflicts = CountOfConflicts(actualState);
                    if (tempCountOfConflicts < conflictsForColumn)
                    {
                        conflictsForColumn = tempCountOfConflicts;
                        numberOfTheBestRow = row;
                        changed = true;
                    }
                }

                if (changed)
                {
                    actualState[column] = numberOfTheBestRow;
                    returnedList.Add(actualState);
                }
            }

            if (!changed)
            {
                returnedList.Add(GenerateState(size));
            }

            return returnedList;
        }

        private byte[] CopyState(byte[] state)
        {
            byte size = (byte) state.Length;
            byte[] returnedArrayBytes = new byte[size];

            for (int column = 0; column < size; column++)
            {
                returnedArrayBytes[column] = state[column];
            }

            return returnedArrayBytes;
        }
    }
}
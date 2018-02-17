using System;
using System.Collections.Generic;

namespace nHetmans
{
    public class Hetmans : IProblem<byte[]>
    {
        public Hetmans(byte size)
        {
            InitialState = GenerateState(size);
        }

        public byte[] InitialState { get; }

        public bool IsGoal(byte[] state)
        {
            if (CountOfConflicts(state) == 0) return true;
            return false;
        }

        public int CountOfConflicts(byte[] state)
        {
            var sizeOfState = (byte) state.Length;

            var conflicts = 0;
            conflicts += CheckHorizontally(state);
            for (byte column = 0; column < sizeOfState; column++)
            {
                for (var value = 1; value + column < sizeOfState; value++) //skos prawo
                {
                    if (state[column + value] == state[column] + value)
                        conflicts++;
                    if (state[column + value] == state[column] - value)
                        conflicts++;
                }

                for (var value = 1; column - value > 0; value++) //skos w lewo
                {
                    if (state[column - value] == state[column] + value)
                        conflicts++;
                    if (state[column - value] == state[column] - value)
                        conflicts++;
                }
            }

            return conflicts;
        }

        public bool Compare(byte[] stateOfNode, byte[] checkingState)
        {
            var size = (byte) stateOfNode.Length;
            for (byte column = 0; column < size; column++)
                if (stateOfNode[column] != checkingState[column])
                    return false;

            return true;
        }

        public IList<byte[]> Expand(byte[] state)
        {
            var possibleStates = GeneratePosisbleStates(state);
            return possibleStates;
        }

        private byte[] GenerateState(byte size)
        {
            var state = new byte[size];
            var random = new Random();
            for (byte i = 0; i < size; i++) state[i] = (byte) random.Next(0, size);

            return state;
        }


        private int CheckHorizontally(byte[] state)
        {
            var conflicts = 0;
            var sizeOfState = (byte) state.Length;
            var visited = new List<byte>();

            for (var column = 0; column < sizeOfState; column++)
            {
                if (visited.Contains(state[column])) conflicts++;
                visited.Add(state[column]);
            }

            return conflicts;
        }

        public void ShowState(byte[] state)
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

        private List<byte[]> GeneratePosisbleStates(byte[] state)
        {
            var size = (byte) state.Length;
            var returnedList = new List<byte[]>();
            var changed = false;
            var initialCountOfConflicts = CountOfConflicts(state);
            int conflictsForColumn, tempCountOfConflicts;
            byte numberOfTheBestRow = 0;

            for (byte column = 0; column < size; column++)
            {
                var actualState = CopyState(state);
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

            if (!changed) returnedList.Add(GenerateState(size));

            return returnedList;
        }

        private byte[] CopyState(byte[] state)
        {
            var size = (byte) state.Length;
            var returnedArrayBytes = new byte[size];

            for (var column = 0; column < size; column++) returnedArrayBytes[column] = state[column];

            return returnedArrayBytes;
        }
    }
}
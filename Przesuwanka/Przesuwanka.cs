using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
{
    class Przesuwanka : IProblem<byte[,]>
    {
        private byte[,] initial;
        private byte[,] goal;


        public Przesuwanka()
        {
        }

        public Przesuwanka(byte[,] initial, byte[,] goal)
        {
            this.initial = initial;
            this.goal = goal;
        }

        public byte[,] InitialState
        {
            get { return this.initial; }
        }

        public IList<byte[,]> Expand(byte[,] state)
        {
            List<byte[,]> possibleStates = createStatesToExpand(state);
            return possibleStates;
        }

        public IList<byte[,]> ExpandPriorityQueue(byte[,] state)
        {
            List<byte[,]> possibleStates = createStatesToExpand(state);
            possibleStates = sortStates(possibleStates);
            return possibleStates;
        }

        private List<byte[,]> sortStates(List<byte[,]> possibleStates)
        {
            List<byte[,]> returnedList = new List<byte[,]>();
            int sizeOfPossibleStates = possibleStates.Count;
            List<Tuple<int, byte[,]>> statesAndConflicts = new List<Tuple<int, byte[,]>>(); //index to kolumna
            List<int> listOfConflicts = new List<int>();

            for (int index = 0; index < sizeOfPossibleStates; index++)
            {
                int count = CountOfConflicts(possibleStates[index]);
                statesAndConflicts.Add(new Tuple<int, byte[,]>(count, possibleStates[index]));
                listOfConflicts.Add(count);
            }

            listOfConflicts.Sort();

            for (int column = 0; column < sizeOfPossibleStates; column++)
            {
                if (listOfConflicts[column] == statesAndConflicts[column].Item1)
                {
                    returnedList.Add(statesAndConflicts[column].Item2);
                }
            }

            return returnedList;
        }

        private int CountOfConflicts(byte[,] possibleState)
        {
            int conflicts = 0;
            int size = possibleState.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (goal[i, j] != possibleState[i, j])
                    {
                        conflicts++;
                    }
                }
            }

            return conflicts;
        }

        static public byte[,] CopyState(byte[,] state)
        {
            byte[,] returnedArrayBytes = new byte[state.GetLength(0), state.GetLength(1)];

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    returnedArrayBytes[i, j] = state[i, j];
                }
            }

            return returnedArrayBytes;
        }

        private List<byte[,]> createStatesToExpand(byte[,] state)
        {
            int size = state.GetLength(0);
            List<byte[,]> possibleStates = new List<byte[,]>(4);
            byte[,] possibleState = new byte[size, size];
            byte tmp;
            byte[] coordinatesOfZero = findCoordinatesOfElement(0, state);

            if (coordinatesOfZero[1] - 1 >= 0)
            {
                possibleState = CopyState(state);
                tmp = state[coordinatesOfZero[0], coordinatesOfZero[1] - 1]; //wartosc ktora zamieniamy z zerem

                possibleState[coordinatesOfZero[0], coordinatesOfZero[1] - 1] = 0; //
                possibleState[coordinatesOfZero[0], coordinatesOfZero[1]] = tmp;
                possibleStates.Add(possibleState);
            }

            if (coordinatesOfZero[0] - 1 >= 0)
            {
                possibleState = CopyState(state);
                tmp = state[coordinatesOfZero[0] - 1, coordinatesOfZero[1]]; //wartosc ktora zamieniamy z zerem

                possibleState[coordinatesOfZero[0] - 1, coordinatesOfZero[1]] = 0; //
                possibleState[coordinatesOfZero[0], coordinatesOfZero[1]] = tmp;
                possibleStates.Add(possibleState);
            }

            if (coordinatesOfZero[0] + 1 <= size - 1)
            {
                possibleState = CopyState(state);
                tmp = state[coordinatesOfZero[0] + 1, coordinatesOfZero[1]]; //wartosc ktora zamieniamy z zerem

                possibleState[coordinatesOfZero[0] + 1, coordinatesOfZero[1]] = 0; //
                possibleState[coordinatesOfZero[0], coordinatesOfZero[1]] = tmp;
                possibleStates.Add(possibleState);
            }

            if (coordinatesOfZero[1] + 1 <= size - 1)
            {
                possibleState = CopyState(state);
                tmp = state[coordinatesOfZero[0], coordinatesOfZero[1] + 1]; //wartosc ktora zamieniamy z zerem

                possibleState[coordinatesOfZero[0], coordinatesOfZero[1] + 1] = 0; //
                possibleState[coordinatesOfZero[0], coordinatesOfZero[1]] = tmp;
                possibleStates.Add(possibleState);
            }


            return possibleStates;
        }


        private byte[] findCoordinatesOfElement(byte element, byte[,] state)
        {
            for (byte i = 0; i < state.GetLength(0); i++)
            {
                for (byte j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == element)
                    {
                        return new byte[2] {i, j};
                    }
                }
            }

            throw new Exception("Zero not found");
        }

        public bool IsGoal(byte[,] state)
        {
            for (int i = 0; i < goal.GetLength(0); i++)
            {
                for (int j = 0; j < goal.GetLength(1); j++)
                {
                    if (goal[i, j] != state[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool Compare(byte[,] stateOfNode, byte[,] checkingState)
        {
            for (int i = 0; i < stateOfNode.GetLength(0); i++)
            {
                for (int j = 0; j < stateOfNode.GetLength(1); j++)
                {
                    if (stateOfNode[i, j] != checkingState[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void showState(byte[,] table)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i, j] + "\t");
                }

                Console.Write(" ]\n");
            }
        }
    }
}
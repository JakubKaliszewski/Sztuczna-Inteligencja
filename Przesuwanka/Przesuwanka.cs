using System;
using System.Collections.Generic;

namespace Przesuwanka
{
    internal class Przesuwanka : IProblem<byte[,]>
    {
        private readonly byte[,] goal;


        public Przesuwanka()
        {
        }

        public Przesuwanka(byte[,] initial, byte[,] goal)
        {
            InitialState = initial;
            this.goal = goal;
        }

        public byte[,] InitialState { get; }

        public IList<byte[,]> Expand(byte[,] state)
        {
            var possibleStates = createStatesToExpand(state);
            return possibleStates;
        }

        public IList<byte[,]> ExpandPriorityQueue(byte[,] state)
        {
            var possibleStates = createStatesToExpand(state);
            possibleStates = sortStates(possibleStates);
            return possibleStates;
        }


        public IList<byte[,]> ExpandAStar(byte[,] state)
        {
            var possibleStates = createStatesToExpand(state);
            possibleStates = sortStatesAStar(possibleStates);
            return possibleStates;
        }

        public bool IsGoal(byte[,] state)
        {
            for (var i = 0; i < goal.GetLength(0); i++)
            for (var j = 0; j < goal.GetLength(1); j++)
                if (goal[i, j] != state[i, j])
                    return false;

            return true;
        }

        public bool Compare(byte[,] stateOfNode, byte[,] checkingState)
        {
            for (var i = 0; i < stateOfNode.GetLength(0); i++)
            for (var j = 0; j < stateOfNode.GetLength(1); j++)
                if (stateOfNode[i, j] != checkingState[i, j])
                    return false;

            return true;
        }

        public void showState(byte[,] table)
        {
            for (var i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("[ ");
                for (var j = 0; j < table.GetLength(1); j++) Console.Write(table[i, j] + "\t");

                Console.Write(" ]\n");
            }
        }

        private List<byte[,]> sortStatesAStar(List<byte[,]> possibleStates)
        {
            var returnedList = new List<byte[,]>();
            var sizeOfPossibleStates = possibleStates.Count;
            var statesAndConflicts = new List<Tuple<int, byte[,]>>(); //index to kolumna
            var listOfConflicts = new List<int>();

            for (var index = 0; index < sizeOfPossibleStates; index++)
            {
                var count = CountOfConflicts(possibleStates[index]) + CountDistancesToGoal(possibleStates[index]);
                statesAndConflicts.Add(new Tuple<int, byte[,]>(count, possibleStates[index]));
                listOfConflicts.Add(count);
            }

            listOfConflicts.Sort();

            for (var column = 0; column < sizeOfPossibleStates; column++)
                if (listOfConflicts[column] == statesAndConflicts[column].Item1)
                    returnedList.Add(statesAndConflicts[column].Item2);

            return returnedList;
        }

        private List<byte[,]> sortStates(List<byte[,]> possibleStates)
        {
            var returnedList = new List<byte[,]>();
            var sizeOfPossibleStates = possibleStates.Count;
            var statesAndConflicts = new List<Tuple<int, byte[,]>>(); //index to kolumna
            var listOfConflicts = new List<int>();

            for (var index = 0; index < sizeOfPossibleStates; index++)
            {
                var count = CountOfConflicts(possibleStates[index]);
                statesAndConflicts.Add(new Tuple<int, byte[,]>(count, possibleStates[index]));
                listOfConflicts.Add(count);
            }

            listOfConflicts.Sort();

            for (var column = 0; column < sizeOfPossibleStates; column++)
                if (listOfConflicts[column] == statesAndConflicts[column].Item1)
                    returnedList.Add(statesAndConflicts[column].Item2);

            return returnedList;
        }

        private int CountOfConflicts(byte[,] possibleState)
        {
            var conflicts = 0;
            var size = possibleState.GetLength(0);

            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                if (goal[i, j] != possibleState[i, j])
                    conflicts++;

            return conflicts;
        }


        private int
            CountDistancesToGoal(
                byte[,] state) //Zliczyć do jednego wora liczbę pól każdej płytki względem pozycji co powinna mieć
        {
            var sumOfDistance = 0;
            var singleDistance = 0;
            var size = (byte) state.GetLength(0);

            for (byte column = 0; column < size; column++)
            for (byte row = 0; row < size; row++)
                sumOfDistance += GetDistanceToTheTargetPosition(state, column, row);

            return sumOfDistance;
        }

        private int GetDistanceToTheTargetPosition(byte[,] state, byte column, byte row)
        {
            var size = (byte) state.GetLength(0);
            var checkedNumber = state[column, row];

            //16%n,16div n
            var goodColumn = (byte) (checkedNumber % size);
            var goodRow = (byte) (checkedNumber / size);

            return Math.Abs(column - row) + Math.Abs(goodColumn - goodRow); //metryka Manhattan
        }

        public static byte[,] CopyState(byte[,] state)
        {
            var returnedArrayBytes = new byte[state.GetLength(0), state.GetLength(1)];

            for (var i = 0; i < state.GetLength(0); i++)
            for (var j = 0; j < state.GetLength(1); j++)
                returnedArrayBytes[i, j] = state[i, j];

            return returnedArrayBytes;
        }

        private List<byte[,]> createStatesToExpand(byte[,] state)
        {
            var size = state.GetLength(0);
            var possibleStates = new List<byte[,]>(4);
            var possibleState = new byte[size, size];
            byte tmp;
            var coordinatesOfZero = findCoordinatesOfElement(0, state);

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
            for (byte j = 0; j < state.GetLength(1); j++)
                if (state[i, j] == element)
                    return new byte[2] {i, j};

            throw new Exception("Zero not found");
        }
    }
}
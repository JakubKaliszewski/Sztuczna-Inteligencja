using System;
using System.Collections.Generic;

namespace Przesuwanka
{
    internal class Przesuwanka : IProblem<byte[,]>
    {
        private readonly byte[,] goal;


        public Przesuwanka(byte size, byte numberOfMixes)
        {
            goal = generateState(size);
            InitialState = mixGeneratedState(generateState(size), numberOfMixes);
        }       
        
        public Przesuwanka(byte size, byte[,] initialState)
        {
            goal = generateState(size);
            InitialState = initialState;
        }


        public byte[,] InitialState { get; set; }
        public double CountOfSteps { get; set;}
        
        private byte[,] generateState(byte size) //Metoda na wygenerowanie tablicy wielowymiarowej, [0...size*size]
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

        private byte[,] mixGeneratedState(byte[,] table, byte numberOfMixes)
        {
            var copyOfTable = CopyState(table);
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

            if (StateIsSolvable(table))
                return table;
            return mixGeneratedState(copyOfTable, numberOfMixes);
        }

        private bool StateIsSolvable(byte[,] state)
        {
          /*
          1. rozwiązywalny gdy size jest nieparzysty oraz liczba inwersji jest parzysta
          2. Jeżeli size jest parzysty to state jest rozwiązywalny gdy:
             - gdy zero znajduje się w parzystym wierszu licząc od dołu(drugi od dołu itp), oraz liczba inwersji jest nieparzysta
             - gdy zero znajduje się w nieparzystym wierszu licząc od dołu, oraz liczba inwersji jest parzysta
          3. Dla reszty nie ma rozwiązań
          */
            int size = state.GetLength(0);
            int countOfInversions = GetCountOfInversions(state);
            if (size % 2 != 0 && countOfInversions % 2 == 0)
                return true;
            
            var coordinatesOfZero = findCoordinatesOfElement(0, state);//1 zmienna to wiersz
            var indexOfZeroFromBottom = size - coordinatesOfZero[0];

            if (indexOfZeroFromBottom % 2 == 0 && countOfInversions % 2 != 0)
                return true;
            
            if (indexOfZeroFromBottom % 2 != 0 && countOfInversions % 2 == 0)
                return true;

            return false;
        }

        private int GetCountOfInversions(byte[,] state)
        {
            int countOfInversions = 0;
            int sizeOfState = state.GetLength(0);
            List<byte> stateInList = stateToList(state);

            for (int toCompareIndex = 0; toCompareIndex < sizeOfState - 1; toCompareIndex++)
            {
                if(stateInList[toCompareIndex] == 0) break;
                
                for (int index = toCompareIndex + 1; index < sizeOfState - 1; index++)
                {
                    if(stateInList[index] == 0) break;
              
                    if (stateInList[index] > stateInList[toCompareIndex])
                        countOfInversions++;
                }
            }
            
            return countOfInversions;
        }

        private List<byte> stateToList(byte[,] state)
        {
            int size = state.GetLength(0);
            List<byte> list = new List<byte>();
            for (int row = 0; row < size - 1; row++)
            {
                for (int column = 0; column < size - 1; column++)
                {
                    list.Add(state[row,column]);
                }
            }

            return list;
        }

        public IList<byte[,]> Expand(byte[,] state)
        {
            return createStatesToExpand(state);
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

        public int CountOfConflicts(byte[,] possibleState)
        {
            var conflicts = 0;
            var size = possibleState.GetLength(0);

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (goal[i, j] != possibleState[i, j])
                        conflicts++;
                }
            }
                
            return conflicts;
        }


        public int CountDistancesToGoal(byte[,] state) //Zliczyć do jednego wora liczbę pól każdej płytki względem pozycji co powinna mieć
        {
            var sumOfDistance = 0;
            var size = (byte) state.GetLength(0);
            sumOfDistance += CountOfConflicts(state);
            
            for (byte column = 0; column < size; column++)
            {
                for (byte row = 0; row < size; row++)
                    sumOfDistance += GetDistanceToTheTargetPosition(state, column, row);
            }
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
            var possibleStates = new List<byte[,]>();
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
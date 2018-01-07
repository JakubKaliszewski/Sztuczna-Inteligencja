using System;

namespace MapaRumunii
{
    internal class Program
    {
        private static void DisplaySolution(MapOfRomania problem, Node<City> result)
        {
            if (result == null)
            {
                Console.WriteLine("\nNo solutions!");
            }
            else
            {
                Console.WriteLine("\nRoad: \nSteps: " + TreeSearch.countOfSteps);
                result.ShowRoad(problem.ShowState);
                
            }
        }

        public static void Main(string[] args)
        {
            Map initMap = new Map();
            Console.WriteLine("Podaj miasto startowe: ");
            string startCity = Console.ReadLine();
            Console.WriteLine("Podaj miasto docelowe: ");
            string destinyCity = Console.ReadLine();
            Console.WriteLine("Wybierz metodę rozwiązania:\n1. wgłąb,\n2. wszerz.");
            int choose = int.Parse(Console.ReadLine());
            MapOfRomania problem = new MapOfRomania(initMap, startCity, destinyCity);
            Node<City> result;

            switch (choose)
            {
                case 1:
                {
                    StackFringe<Node<City>> stackSolution = new StackFringe<Node<City>>();
                    result = TreeSearch.TreeSearchMetod(problem, stackSolution);
                    DisplaySolution(problem, result);
                    break;
                }

                case 2:
                {
                    QueueFringe<Node<City>> queueSolution = new QueueFringe<Node<City>>();
                    result = TreeSearch.TreeSearchMetod(problem, queueSolution);
                    DisplaySolution(problem, result);
                    break;
                }
            }
        }
    }
}
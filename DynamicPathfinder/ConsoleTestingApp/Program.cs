using DynamicPathfinder;
using System;
using System.Linq;
using System.Timers;

namespace ConsoleTestingApp
{
    class Program
    {
        static GeneticAlgorithm geneticAlgorithm;
        static bool lastCoordOnly = true;

        static void Main()
        {
            Coordinate dest = GetDestFromUser();
            lastCoordOnly = GetPathPrefFromUser();

            int iterations = dest.X + dest.Y + dest.Z;
            iterations += iterations / 3;
            geneticAlgorithm = new GeneticAlgorithm(20, iterations, new Coordinate(0, 0, 0), dest, 1);
            Timer timer = new Timer(10) { AutoReset = true, Enabled = true };
            timer.Elapsed += TimerCallback;
            timer.Start();
            Console.ReadKey();
            timer.Stop();
        }

        private static bool GetPathPrefFromUser()
        {
            Console.WriteLine("Last coordinate only? (Y/N):");
            return "y".Equals(Console.ReadLine(), StringComparison.OrdinalIgnoreCase);
        }

        private static Coordinate GetDestFromUser()
        {
            Console.WriteLine("Enter Destination X:");
            var x = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Destination Y:");
            var y = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Destination Z:");
            var z = int.Parse(Console.ReadLine());
            Coordinate dest = new Coordinate(x, y, z);
            return dest;
        }

        private static void TimerCallback(object source, ElapsedEventArgs e)
        {
            geneticAlgorithm.Update();
            if (geneticAlgorithm.Iteration == geneticAlgorithm.IterationsPerGeneration)
            {
                Genome bestGenome = geneticAlgorithm.GetFittestGenome();
                string pathStr = "";
                if (lastCoordOnly)
                {
                    Coordinate lastCoord = bestGenome.Path.Last();
                    pathStr += $"Last coordinate = ({lastCoord.X},{lastCoord.Y},{lastCoord.Z})";
                }
                else
                {
                    pathStr += "Current Path";
                    foreach (var coord in bestGenome.Path)
                    {
                        pathStr += $"({coord.X},{coord.Y},{coord.Z}) ";
                    }
                }
                pathStr += $", fitness = {geneticAlgorithm.GetFitness(bestGenome)}";
                Console.WriteLine(pathStr);
            }
        }
    }
}

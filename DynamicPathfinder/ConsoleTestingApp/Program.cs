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

        static void Main(string[] args)
        {
            Coordinate dest = new Coordinate(15, 15, 15);
            int iterations = dest.X + dest.Y + dest.Z;
            iterations += iterations / 3;
            geneticAlgorithm = new GeneticAlgorithm(20, iterations, new Coordinate(0, 0, 0), dest);
            Timer timer = new Timer(50) { AutoReset = true, Enabled = true };
            timer.Elapsed += TimerCallback;
            timer.Start();
            Console.ReadKey();
            timer.Stop();
        }
        
        private static void TimerCallback(object source, ElapsedEventArgs e)
        {
            geneticAlgorithm.Update();
            if (geneticAlgorithm.Iteration == geneticAlgorithm.IterationsPerGeneration)
            {
                Genome firstGenome = geneticAlgorithm.GetFirstGenome();
                string pathStr = "";
                if (lastCoordOnly)
                {
                    Coordinate lastCoord = firstGenome.Path.Last();
                    pathStr += $"Last coordinate = ({lastCoord.X},{lastCoord.Y},{lastCoord.Z})";
                }
                else
                {
                    pathStr += "Current Path";
                    foreach (var coord in firstGenome.Path)
                    {
                        pathStr += $"({coord.X},{coord.Y},{coord.Z}) ";
                    }
                }
                pathStr += $", fitness = {geneticAlgorithm.GetFitness(firstGenome)}";
                Console.WriteLine(pathStr);
            }
        }
    }
}

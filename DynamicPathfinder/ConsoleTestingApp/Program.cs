﻿using DynamicPathfinder;
using System;
using System.Linq;
using System.Threading;
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
            int noOfGenomes = GetIntFromUser("Enter number of genomes:");
            float mutationStrength = GetMutationStrengthFromUser();
            lastCoordOnly = GetPathPrefFromUser();

            Console.WriteLine("Running - Press any key to end");

            int iterations = dest.X + dest.Y + dest.Z;
            iterations += iterations / 3;
            geneticAlgorithm = new GeneticAlgorithm(noOfGenomes, iterations, new Coordinate(0, 0, 0), dest, mutationStrength, CrossOver.CrossOverType.ONE_POINT);
            while (!Console.KeyAvailable)
            {
                RunAlgorithm();
            }
            Console.ReadLine();
        }

        private static Coordinate GetDestFromUser()
        {
            var x = GetIntFromUser("Enter Destination X:");
            var y = GetIntFromUser("Enter Destination Y:");
            var z = GetIntFromUser("Enter Destination Z:");
            Coordinate dest = new Coordinate(x, y, z);
            return dest;
        }

        private static int GetIntFromUser(string instruction)
        {
            Console.WriteLine(instruction);
            return int.Parse(Console.ReadLine());
        }

        private static float GetMutationStrengthFromUser()
        {
            Console.WriteLine("Enter Mutation Strength (0.0 - 100):");
            return float.Parse(Console.ReadLine());
        }

        private static bool GetPathPrefFromUser()
        {
            Console.WriteLine("Last coordinate only? (Y/N):");
            return "y".Equals(Console.ReadLine(), StringComparison.OrdinalIgnoreCase);
        }

        private static void RunAlgorithm()
        {
            geneticAlgorithm.Update();
            if (geneticAlgorithm.Iteration == geneticAlgorithm.IterationsPerGeneration)
            {
                PrintLastGeneration();
                Thread.Sleep(500);
            }
        }

        private static void PrintLastGeneration()
        {
            Genome bestGenome = geneticAlgorithm.GetFittestGenome();
            Coordinate dest = geneticAlgorithm.DestinationPosition;
            string pathStr = $"Gen {geneticAlgorithm.Generation + 1}, ";
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
            pathStr += $", destination = ({dest.X},{dest.Y},{dest.Z})";
            Console.WriteLine(pathStr);
        }
    }
}

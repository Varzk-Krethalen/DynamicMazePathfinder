using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicPathfinder
{
    public class Population
    {
        public List<Genome> Genomes { get; set; } = new List<Genome>();
        private int NumberOfGenomes { get; }
        public CrossOver CrossOver { get; }
        public Coordinate OriginPosition { get; }
        private int mutationStrength = 1;

        public Population(int numberOfGenomes, CrossOver crossOver, Coordinate originPosition)
        {
            NumberOfGenomes = numberOfGenomes;
            CrossOver = crossOver;
            OriginPosition = originPosition;
            AddNewGenomes(NumberOfGenomes);
        }

        public void CreateNewGeneration() //when mutate? before, after, new percentage set?
        {
            int topTenPercent = (int)(((double)NumberOfGenomes / 100) * 25);
            List<Genome> newGenomes = Genomes.OrderBy(g => GetFitness(g))
                .Take(topTenPercent)
                .ToList();

            Genomes = new List<Genome>(newGenomes);

            int remainingGenes = NumberOfGenomes - topTenPercent;
            AddNewGenomes(remainingGenes);
        }

        /// <summary>
        /// Add a number of new genome instances to the population
        /// </summary>
        /// <param name="noOfGenomes"></param>
        private void AddNewGenomes(int noOfGenomes)
        {
            for (int i = 0; i < noOfGenomes && Genomes.Count < NumberOfGenomes; i++)
            {
                Genomes.Add(new Genome(OriginPosition));
            }
        }

        /// <summary>
        /// Get a fitness score for the genome (euclidean distance)
        /// </summary>
        /// <param name="genome"></param>
        /// <returns></returns>
        private float GetFitness(Genome genome)
        {
            return OriginPosition.GetDistance(genome.CurrentPosition);
        }

        /// <summary>
        /// Iterate the next move of each genome
        /// </summary>
        public void RunIteration()
        {
            foreach (Genome genome in Genomes)
            {
                genome.Move();
            }
        }

        /// <summary>
        /// Foreach genome, based on mutation percentage chance, mutate up to that percentage of genes
        /// </summary>
        private void MutateGenomes()
        {
            foreach (Genome genome in Genomes)
            {
                if (StaticUtils.Random.Next(1, 100) <= mutationStrength) //consider NextDouble
                {
                    genome.Mutate(StaticUtils.Random.Next(0, mutationStrength));
                }
            }
        }
    }
}

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
        public Coordinate DestinationPosition { get; }

        private int MutationStrength { get; set; }

        /// <summary>
        /// Create a population of a set number of genes, with a crossover type, and the ideal path ends
        /// </summary>
        /// <param name="numberOfGenomes"></param>
        /// <param name="crossOver"></param>
        /// <param name="originPosition"></param>
        /// <param name="destinationPosition"></param>
        public Population(int numberOfGenomes, CrossOver crossOver, Coordinate originPosition, Coordinate destinationPosition, float mutationStrength)
        {
            NumberOfGenomes = numberOfGenomes;
            CrossOver = crossOver;
            OriginPosition = originPosition;
            DestinationPosition = destinationPosition;
            MutationStrength = (int)(mutationStrength * 100);
            for (int i = 0; i < NumberOfGenomes; i++)
            {
                Genomes.Add(new Genome(OriginPosition));
            }
        }

        /// <summary>
        /// Create a new generation of genomes based on the previous
        /// </summary>
        public void CreateNewGeneration() //when mutate? before, after, new percentage set?
        {
            int topTenPercent = (int)(((double)NumberOfGenomes / 100) * 25);
            IOrderedEnumerable<Genome> genomesByFitness = Genomes.OrderBy(g => GetFitness(g));
            int requiredRemaining = NumberOfGenomes - topTenPercent;

            Genomes = new List<Genome>(genomesByFitness
                .Take(topTenPercent)
                .ToList());

            List<Genome> offspring = GetCrossOverGenomes(Genomes, requiredRemaining);
            Genomes.AddRange(offspring);
            MutateGenomes(Genomes);
        }
        

        /// <summary>
        /// Crossover all pairs of genomes
        /// </summary>
        /// <param name="parentGenomes"></param>
        /// <returns></returns>
        private List<Genome> GetCrossOverGenomes(List<Genome> parentGenomes, int requiredChildren)
        {
            List<Genome> offspring = new List<Genome>();
            while (offspring.Count < requiredChildren)
            {
                Genome parent1 = parentGenomes[StaticUtils.Random.Next(parentGenomes.Count - 1)];
                Genome parent2 = parentGenomes[StaticUtils.Random.Next(parentGenomes.Count - 1)];
                offspring.Add(CrossOver.CrossOverGenomes(parent1, parent2, OriginPosition));
            }
            return offspring;
        }

        /// <summary>
        /// Get a fitness score for the genome (euclidean distance)
        /// </summary>
        /// <param name="genome"></param>
        /// <returns></returns>
        public float GetFitness(Genome genome)
        {
            return DestinationPosition.GetDistance(genome.CurrentPosition);
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
        private void MutateGenomes(List<Genome> genomesToEvolve)
        {
            foreach (Genome genome in genomesToEvolve)
            {
                if (StaticUtils.Random.Next(1, 10000) <= MutationStrength) //consider NextDouble
                {
                    genome.Mutate(StaticUtils.Random.Next(1, StaticUtils.GenomeLength / 100));
                }
            }
        }
    }
}

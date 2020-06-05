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
        public Population(int numberOfGenomes, CrossOver crossOver, Coordinate originPosition, Coordinate destinationPosition, int mutationStrength)
        {
            NumberOfGenomes = numberOfGenomes;
            CrossOver = crossOver;
            OriginPosition = originPosition;
            DestinationPosition = destinationPosition;
            this.MutationStrength = mutationStrength;
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

            Genomes = new List<Genome>(genomesByFitness
                .Take(topTenPercent)
                .ToList());
            Genomes.AddRange(GetCrossOverGenomes(genomesByFitness.Skip(topTenPercent).ToList()));
        }//Should crossovers be from the fittest?
        

        /// <summary>
        /// Crossover all pairs of genomes
        /// </summary>
        /// <param name="parentGenomes"></param>
        /// <returns></returns>
        private List<Genome> GetCrossOverGenomes(List<Genome> parentGenomes)
        {
            //select randomly from top ten for parents?
            List<(Genome, Genome)> parentPairs = PairGenomes(parentGenomes, out Genome oddGenome);
            List<Genome> offspring = new List<Genome>();
            parentPairs.ForEach(p =>
            {
                CrossOver.CrossOverGenomes(p.Item1.Genes, p.Item2.Genes, out Genome offspring1, out Genome offspring2, OriginPosition);
                offspring.Add(offspring1);
                offspring.Add(offspring2);
            });
            offspring.Add(oddGenome);
            return offspring;
        }

        /// <summary>
        /// Pair genomes
        /// </summary>
        /// <param name="genomes"></param>
        /// <returns></returns>
        private List<(Genome, Genome)> PairGenomes(List<Genome> genomes, out Genome oddGenome)
        {
            List<(Genome, Genome)> pairs = new List<(Genome, Genome)>();
            while (genomes.Count > 1)
            {
                (Genome, Genome) pair = (genomes.First(), genomes.Skip(1).First());
                genomes.Remove(pair.Item1);
                genomes.Remove(pair.Item2);
                pairs.Add(pair);
            }
            oddGenome = genomes.FirstOrDefault();
            return pairs;
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
        private void MutateGenomes()
        {
            foreach (Genome genome in Genomes)
            {
                if (StaticUtils.Random.Next(1, 100) <= MutationStrength) //consider NextDouble
                {
                    genome.Mutate(StaticUtils.Random.Next(0, MutationStrength));
                }
            }
        }
    }
}

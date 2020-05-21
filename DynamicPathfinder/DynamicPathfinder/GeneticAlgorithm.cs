using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicPathfinder
{
    public class GeneticAlgorithm
    {
        public Coordinate OriginPosition { get; set; }
        public Coordinate DestinationPosition { get; set; }
        public int NumberOfGenomes { get; set; }
        public int IterationsPerGeneration { get; set; }
        public int Generation { get; private set; }
        private Population Population { get; set; }
        private CrossOver CrossOver { get; set; }
        private int Iteration { get; set; }

        /// <summary>
        /// Create new algorithm instance and initial population
        /// </summary>
        /// <param name="numberOfGenomes"></param>
        /// <param name="iterationsPerGeneration"></param>
        /// <param name="originPosition"></param>
        /// <param name="destinationPosition"></param>
        public GeneticAlgorithm(int numberOfGenomes, int iterationsPerGeneration, Coordinate originPosition, Coordinate destinationPosition) //fitness threshold? percentage of fit individuals?
        {
            NumberOfGenomes = numberOfGenomes;
            OriginPosition = originPosition;
            DestinationPosition = destinationPosition;
            IterationsPerGeneration = iterationsPerGeneration;
            CrossOver = CrossOver.GetCrossOver(CrossOver.CrossOverType.ONE_POINT);
            BeginNewPopulation();
            //add mutation strength?
        }

        /// <summary>
        /// Create a new population set
        /// </summary>
        public void BeginNewPopulation()
        {
            Population = new Population(NumberOfGenomes, CrossOver, OriginPosition);
        }

        /// <summary>
        /// Update population status
        /// </summary>
        public void Update()
        {
            if (Iteration > IterationsPerGeneration)
            {
                Population.CreateNewGeneration();
            }
            else
            {
                Population.RunIteration();
                UpdateDestinationPosition();
            }
        }

        /// <summary>
        /// 5% chance for the destination position to change
        /// </summary>
        private void UpdateDestinationPosition()
        {
            if (StaticUtils.Random.Next(0, 100) > 95)
            {
                DestinationPosition.MoveDirection(GeneDirection.GetRandomDirection());
            }
        }

        /// <summary>
        /// Gets a copy of the path coordinates for the first genome of the current generation
        /// </summary>
        /// <returns></returns>
        public List<Coordinate> GetFirstGenomePath()
        {
            return new List<Coordinate>(Population.Genomes.FirstOrDefault().Path);
        }
    }
}

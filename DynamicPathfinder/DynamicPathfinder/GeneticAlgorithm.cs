using System.Linq;

namespace DynamicPathfinder
{
    public class GeneticAlgorithm
    {
        public Coordinate OriginPosition { get; set; }
        public Coordinate InitialDestinationPosition { get; set; }
        public Coordinate DestinationPosition { get; set; }
        public float MutationStrength { get; }
        public int NumberOfGenomes { get; set; }
        public int IterationsPerGeneration { get; set; }
        private Population Population { get; set; }
        private CrossOver CrossOver { get; set; }
        public int Iteration { get; private set; } = 0;
        public int Generation { get; private set; } = 0;

        /// <summary>
        /// Create new algorithm instance and initial population
        /// </summary>
        /// <param name="numberOfGenomes"></param>
        /// <param name="iterationsPerGeneration"></param>
        /// <param name="originPosition"></param>
        /// <param name="destinationPosition"></param>
        public GeneticAlgorithm(int numberOfGenomes, int iterationsPerGeneration, Coordinate originPosition, Coordinate destinationPosition, float mutationStrength, CrossOver.CrossOverType crossOverType) //fitness threshold? percentage of fit individuals?
        {
            NumberOfGenomes = numberOfGenomes;
            OriginPosition = originPosition;
            InitialDestinationPosition = new Coordinate(destinationPosition);
            DestinationPosition = destinationPosition;
            MutationStrength = mutationStrength;
            IterationsPerGeneration = iterationsPerGeneration;
            CrossOver = CrossOver.GetCrossOver(crossOverType);
            BeginNewPopulation();
            //add mutation strength?
        }

        /// <summary>
        /// Create a new population set
        /// </summary>
        public void BeginNewPopulation()
        {//shouldn't have to pass the destination position
            Population = new Population(NumberOfGenomes, CrossOver, OriginPosition, DestinationPosition, MutationStrength);
        }

        /// <summary>
        /// Update population status
        /// </summary>
        public void Update()
        {
            if (Iteration > IterationsPerGeneration)
            {
                Population.CreateNewGeneration();
                DestinationPosition = new Coordinate(InitialDestinationPosition);
                Generation++;
                Iteration = 0;
            }
            else
            {
                Population.RunIteration();
                UpdateDestinationPosition();
                Iteration++;
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
        /// Gets the first genome of the current generation
        /// </summary>
        /// <returns></returns>
        public Genome GetFirstGenome()
        {
            return Population.Genomes.FirstOrDefault();
        }

        /// <summary>
        /// Gets the fittest genome of the current generation
        /// </summary>
        /// <returns></returns>
        public Genome GetFittestGenome()
        {
            return Population.Genomes.OrderBy(g => Population.GetFitness(g)).FirstOrDefault();
        }

        public float GetFitness(Genome genome)
        {
            return Population.GetFitness(genome);
        }
    }
}

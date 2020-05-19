using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicPathfinder
{
    public class GeneticAlgorithm
    {
        private Population Population { get; set; }
        private CrossOver CrossOver { get; set; }
        public Coordinate OriginPosition { get; set; }
        public Coordinate DestinationPosition { get; set; }
        public int NumberOfGenomes { get; set; }
        public int IterationsPerGeneration { get; }
        public int Generation { get; private set; }
        private int Iteration { get; set; }

        public GeneticAlgorithm(int numberOfGenomes, int iterationsPerGeneration, Coordinate originPosition, Coordinate destinationPosition) //fitness threshold? percentage of fit individuals?
        {
            NumberOfGenomes = numberOfGenomes;
            OriginPosition = originPosition;
            DestinationPosition = destinationPosition;
            IterationsPerGeneration = iterationsPerGeneration;
            BeginNewPopulation(); //remove and assume the function will be called?
            CrossOver = CrossOver.GetCrossOver(CrossOver.CrossOverType.SINGLE);
            //add mutation strength
        }

        public void BeginNewPopulation()
        {
            Population = new Population(NumberOfGenomes, CrossOver, OriginPosition);
        }

        public void Update() //add to path, 
        {//how get number of iterations per gen?
            if (Iteration > IterationsPerGeneration)
            {
                Population.CreateNewGeneration();
            }
            else
            {
                Population.RunIteration();
                //Consider updating destination position - random number out of 100, if >95 then move?
            }
        }

        public List<PathSegment> GetGenomePath() //for a selected genome...
        {
            throw new NotImplementedException();
        }
    }
}

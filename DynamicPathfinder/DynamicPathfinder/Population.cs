using System.Collections.Generic;

namespace DynamicPathfinder
{
    public class Population
    {
        public List<Genome> Genomes { get; set; } = new List<Genome>();
        public CrossOver CrossOver { get; }
        public Coordinate OriginPosition { get; }

        public Population(int numberOfGenomes, CrossOver crossOver, Coordinate originPosition)
        {
            for (int i = 0; i < numberOfGenomes; i++)
            {
                Genomes.Add(new Genome(originPosition));
            }
            CrossOver = crossOver;
            OriginPosition = originPosition;
        }

        public void CreateNewGeneration()
        {

        }//this is where we run a fitness class thingy
        //basic fitness - just use displacement distance of coords.
        //is A* needed at all?

        public void RunIteration()
        {

        }

        private void MutateGenomes()
        {
            //based on a mutation strength, call Mutate on a random set of genes.
        }
    }
}

using System;
using System.Collections.Generic;

namespace DynamicPathfinder
{
    public class Genome //chromosome? what names!?
    {
        public List<PathSegment> Path { get; set; } = new List<PathSegment>();
        public Gene[] Genes { get; set; }
        private Coordinate CurrentPosition { get; set; }
        private static Array values = Enum.GetValues(typeof(Gene));
        private static Random random = new Random();

        public Genome(Coordinate initialPosition)
        {
            //create new random genome - mutate in new constructor?
            CurrentPosition = initialPosition;
            Genes = new Gene[100];

            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = GetRandomGene();
            }
        }

        private static Gene GetRandomGene()
        {
            return (Gene)values.GetValue(random.Next(values.Length));
        }

        public void Move()
        {
            //update path with direction from genes
            Gene gene = Genes[12]; //should be random
            Coordinate newPosition = CurrentPosition;
            switch(gene)
            {
                case Gene.UP:
                    newPosition.Y++;
                    break;
                case Gene.DOWN:
                    newPosition.Y--;
                    break;
                case Gene.NORTH:
                    newPosition.Z++;
                    break;
                case Gene.SOUTH:
                    newPosition.Z--;
                    break;
                case Gene.EAST:
                    newPosition.X++;
                    break;
                default:
                    newPosition.X--;
                    break;
            }
            //UpdatePath()
        }

        /// <summary>
        /// Mutate a number of genes
        /// </summary>
        /// <param name="genesToMutate"></param>
        public void Mutate(int genesToMutate)
        {
            for (int i = 0; i < genesToMutate; i++)
            {
                int index = random.Next(Genes.Length);
                Genes[index] = GetRandomGene();
            }
        }
    }
}

using System.Collections.Generic;
using static DynamicPathfinder.GeneDirection;

namespace DynamicPathfinder
{
    public class Genome //chromosome? naming.
    {
        public List<Coordinate> Path { get; set; } = new List<Coordinate>();
        private Gene[] Genes { get; set; }
        public Coordinate CurrentPosition { get; private set; }

        /// <summary>
        /// Create genome starting at a given coordinate, possibly with specified genes
        /// </summary>
        /// <param name="initialPosition"></param>
        public Genome(Coordinate initialPosition, Gene[] genes = null)
        {
            CurrentPosition = initialPosition;
            Path.Add(CurrentPosition);
            if (genes == null)
            {
                Genes = new Gene[100]; //need to somewhere define genome length

                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = new Gene();
                }
            }
            else
            {
                Genes = genes;
            }
        }

        /// <summary>
        /// Move in a gene's direction, and reflect that in the path
        /// </summary>
        public void Move()
        {
            Direction direction = Genes[StaticUtils.Random.Next(0,99)].Direction;
            Coordinate newPosition = new Coordinate(CurrentPosition);

            newPosition.MoveDirection(direction);
            Path.Add(newPosition);
            CurrentPosition = newPosition;
        }

        /// <summary>
        /// Mutate a number of genes
        /// </summary>
        /// <param name="genesToMutate"></param>
        public void Mutate(int genesToMutate)
        {
            for (int i = 0; i < genesToMutate; i++)
            {
                int index = StaticUtils.Random.Next(Genes.Length);
                Genes[index] = new Gene();
            }
        }
    }
}

namespace DynamicPathfinder
{
    public abstract class CrossOver
    {
        /// <summary>
        /// Available CrossOver implementations
        /// </summary>
        public enum CrossOverType { UNIFORM, SEGMENT, ONE_POINT }

        /// <summary>
        /// Return one of the implemented crossover types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CrossOver GetCrossOver(CrossOverType type)
        {
            switch(type)
            {
                case CrossOverType.UNIFORM:
                    return new UniformCrossover();
                case CrossOverType.SEGMENT:
                    return new SegmentCrossover();
                default:
                    return new OnePointCrossover();
            }
        }

        /// <summary>
        /// Get two offspring from a pair of parent genomes
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <param name="offspring1"></param>
        /// <param name="offspring2"></param>
        /// <param name="initialPosition"></param>
        public abstract void CrossOverGenomes(Gene[] parent1, Gene[] parent2, out Genome offspring1, out Genome offspring2, Coordinate initialPosition);
    }
}

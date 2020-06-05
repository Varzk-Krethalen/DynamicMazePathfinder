namespace DynamicPathfinder
{
    public abstract class CrossOver
    {
        /// <summary>
        /// Available CrossOver implementations
        /// </summary>
        public enum CrossOverType { UNIFORM, TWO_POINT, ONE_POINT }

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
                case CrossOverType.TWO_POINT:
                    return new TwoPointCrossover();
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
        public abstract Genome CrossOverGenomes(Genome parent1, Genome parent2, Coordinate initialPosition);
    }
}

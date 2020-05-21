using static DynamicPathfinder.GeneDirection;

namespace DynamicPathfinder
{
    public class Gene
    {
        public Direction Direction { get; set; }

        /// <summary>
        /// Create a new gene with a random direction
        /// </summary>
        public Gene()
        {
            RandomiseDirection();
        }

        /// <summary>
        /// Set the gene's direction to a random direction
        /// </summary>
        public void RandomiseDirection()
        {
            Direction = GetRandomDirection();
        }
    }
}

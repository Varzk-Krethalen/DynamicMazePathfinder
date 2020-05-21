using System;

namespace DynamicPathfinder
{
    public static class GeneDirection
    {
        private static Array values = Enum.GetValues(typeof(Direction));

        /// <summary>
        /// 3D orthogonal directions
        /// </summary>
        public enum Direction
        {
            UP,
            DOWN,
            NORTH,
            SOUTH,
            EAST,
            WEST
        }

        /// <summary>
        /// Get a random direction value
        /// </summary>
        /// <returns></returns>
        public static Direction GetRandomDirection()
        {
            return (Direction)values.GetValue(StaticUtils.Random.Next(values.Length));
        }
    }
}

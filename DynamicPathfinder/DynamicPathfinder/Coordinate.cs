using System;
using static DynamicPathfinder.GeneDirection;

namespace DynamicPathfinder
{
    public class Coordinate
    {
        /// <summary>
        /// Create a 3D coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Create a copy of a coordinate
        /// </summary>
        /// <param name="coordToCopy"></param>
        public Coordinate(Coordinate coordToCopy)
        {

            X = coordToCopy.X;
            Y = coordToCopy.Y;
            Z = coordToCopy.Z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        /// <summary>
        /// Move the coordinate in the specificed direction
        /// </summary>
        /// <param name="direction"></param>
        public void MoveDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    Y++;
                    break;
                case Direction.DOWN:
                    Y--;
                    break;
                case Direction.NORTH:
                    Z++;
                    break;
                case Direction.SOUTH:
                    Z--;
                    break;
                case Direction.EAST:
                    X++;
                    break;
                default:
                    X--;
                    break;
            }
        }

        /// <summary>
        /// Get euclidean distance between two coordinates
        /// </summary>
        /// <param name="secondPoint"></param>
        /// <returns></returns>
        public float GetDistance(Coordinate secondPoint)
        {
            float deltaX = X - secondPoint.X;
            float deltaY = Y - secondPoint.Y;
            float deltaZ = Z - secondPoint.Z;

            return (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY) + (deltaZ * deltaZ));
        }
    }
}

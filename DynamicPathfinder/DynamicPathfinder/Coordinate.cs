using System;
using static DynamicPathfinder.GeneDirection;

namespace DynamicPathfinder
{
    public class Coordinate
    {
        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Coordinate(Coordinate coordToCopy)
        {

            X = coordToCopy.X;
            Y = coordToCopy.Y;
            Z = coordToCopy.Z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

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

        public float GetDistance(Coordinate secondPoint)
        {
            float deltaX = X - secondPoint.X;
            float deltaY = Y - secondPoint.Y;
            float deltaZ = Z - secondPoint.Z;

            return (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY) + (deltaZ * deltaZ));
        }
    }
}

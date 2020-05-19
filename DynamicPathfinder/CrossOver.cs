using System;

namespace DynamicPathfinder
{
    public abstract class CrossOver //segment, individual, split on index...
    {
        public enum CrossOverType { SINGLE, SEGMENT, ONE_POINT}
        public CrossOver GetCrossOver(CrossOverType type)
        {
            throw new NotImplementedException();
        }
    }
}

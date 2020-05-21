using System;

namespace DynamicPathfinder
{
    public static class StaticUtils
    {
        public static Random Random { get; set; } = new Random();
        public static int GenomeLength { get; set; } = 1000;
        public static int RandomGeneIndex()
        {
            return Random.Next(GenomeLength - 1);
        }
    }
}

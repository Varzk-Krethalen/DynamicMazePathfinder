namespace DynamicPathfinder
{
    internal class OnePointCrossover : CrossOver
    {
        public override Genome CrossOverGenomes(Gene[] parent1, Gene[] parent2, Coordinate initialPosition)
        {
            int crossPoint = StaticUtils.Random.Next(0, parent1.Length);
            return new Genome(initialPosition, CrossGenes(parent1, parent2, crossPoint));
        }

        private Gene[] CrossGenes(Gene[] parentGenes1, Gene[] parentGenes2, int crossPoint)
        {
            Gene[] newGenes = new Gene[StaticUtils.GenomeLength];
            for (int i = 0; i < crossPoint; i++)
            {
                newGenes[i] = parentGenes1[i];
            }
            for (int i = crossPoint; i < newGenes.Length; i++)
            {
                newGenes[i] = parentGenes2[i];
            }
            return newGenes;
        }
    }
}
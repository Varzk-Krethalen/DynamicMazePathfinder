namespace DynamicPathfinder
{
    internal class OnePointCrossover : CrossOver
    {
        public override void CrossOverGenomes(Gene[] parent1, Gene[] parent2, out Genome offspring1, out Genome offspring2, Coordinate initialPosition)
        {
            int crossPoint = StaticUtils.Random.Next(0, parent1.Length);
            offspring1 = new Genome(initialPosition, CrossGenes(parent1, parent2, crossPoint));
            offspring2 = new Genome(initialPosition, CrossGenes(parent2, parent1, crossPoint));
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
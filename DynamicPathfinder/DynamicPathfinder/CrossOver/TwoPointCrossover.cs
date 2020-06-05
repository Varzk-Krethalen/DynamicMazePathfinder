namespace DynamicPathfinder
{
    internal class TwoPointCrossover : CrossOver
    {
        public override Genome CrossOverGenomes(Genome parent1, Genome parent2, Coordinate initialPosition)
        {
            int crossPoint1 = StaticUtils.RandomGeneIndex();
            int crossPoint2 = StaticUtils.RandomGeneIndex();
            return new Genome(initialPosition, CrossGenes(parent1.Genes, parent2.Genes, crossPoint1, crossPoint2));
        }

        private Gene[] CrossGenes(Gene[] parentGenes1, Gene[] parentGenes2, int crossPoint1, int crossPoint2)
        {
            Gene[] newGenes = new Gene[StaticUtils.GenomeLength];
            for (int i = 0; i < newGenes.Length; i++)
            {
                newGenes[i] = parentGenes1[i];
            }
            for (int i = crossPoint1; i < crossPoint2; i++)
            {
                newGenes[i] = parentGenes2[i];
            }
            return newGenes;
        }
    }
}
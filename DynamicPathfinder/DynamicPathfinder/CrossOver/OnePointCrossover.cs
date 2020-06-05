namespace DynamicPathfinder
{
    internal class OnePointCrossover : CrossOver
    {
        public override Genome CrossOverGenomes(Genome parent1, Genome parent2, Coordinate initialPosition)
        {
            int crossPoint = StaticUtils.RandomGeneIndex();
            return new Genome(initialPosition, CrossGenes(parent1.Genes, parent2.Genes, crossPoint));
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
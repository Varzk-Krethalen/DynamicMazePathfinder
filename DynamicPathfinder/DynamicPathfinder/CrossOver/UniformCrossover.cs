namespace DynamicPathfinder
{
    internal class UniformCrossover : CrossOver
    {
        public override Genome CrossOverGenomes(Genome parent1, Genome parent2, Coordinate initialPosition)
        {
            Gene[] newGenes = new Gene[StaticUtils.GenomeLength];
            for (int i = 0; i < newGenes.Length; i++)
            {
                if (StaticUtils.Random.Next(0, 1) == 0)
                {
                    newGenes[i] = parent1.Genes[i];
                }
                else
                {
                    newGenes[i] = parent2.Genes[i];
                }
            }
            return new Genome(initialPosition, newGenes);
        }
    }
}
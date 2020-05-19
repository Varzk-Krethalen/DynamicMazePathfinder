namespace DynamicPathfinder
{
    //either each gene is a direction with probability, or
    //we have say 100 genes, which are each a direction, and we take a random one - mutate on distribution of genes!
    public class Gene
    {
        public Direction Direction { get; set; }
        public void Mutate()
        {

        }
    }
}

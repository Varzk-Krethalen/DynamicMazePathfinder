using DynamicPathfinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(20, 20, new Coordinate(0, 0, 0), new Coordinate(5, 5, 5));
            geneticAlgorithm.Update();

        }   
    }
}

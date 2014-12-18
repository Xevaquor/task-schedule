using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Darwin;

namespace DarwinManualTest
{
    class Program
    {
        static double fitness(Individual ind)
        {
            int[] array = new int[1];
            ind.Chromosome.CopyTo(array, 0);
            var x = array[0];
            return x;
        }

        static void Main(string[] args)
        {
            Population p = Population.GetRandomPopulation(6);

            Console.WriteLine(p.ToString(fitness));
            Console.WriteLine("Population fitness sum: {0}",
                p.GetPopulationFitnessSum(fitness));
            var np = new Population { Individuals = new Roulette(fitness, p).SpinTheWheel(6) };

            Console.WriteLine(Roulette.ProbabilityToString(p, fitness));

            Console.WriteLine("Selected for reproducing");
            Console.WriteLine(np.ToString(fitness));

            var h = new Harem(fitness, np);
            var newGeneration = h.Reproduce();

            Console.WriteLine("After reproduction");
            Console.WriteLine(newGeneration.ToString(fitness));


            Console.ReadLine();
        }
    }
}

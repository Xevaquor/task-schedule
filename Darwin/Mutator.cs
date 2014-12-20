using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin
{
    public class Mutator
    {
        public static readonly double MUTATION_RATE = 0.05;

        public void Mutate(ref Population population)
        {
#if PARALLEL_MUTATE
            Parallel.ForEach(population.Individuals, individual =>
            {
#else
            foreach (var individual in population.Individuals)
            {
#endif
                for (int i = 0; i < individual.Chromosome.Length; i++)
                {
                    if (Individual.RANDOM.NextDouble() < MUTATION_RATE)
                    {
                        individual.Chromosome[i] = !individual.Chromosome[1];
                    }
                }
#if PARALLEL_MUTATE
            //});
#else
            }
#endif

        }
    }
}

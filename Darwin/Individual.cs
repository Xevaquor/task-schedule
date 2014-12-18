using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin
{
    public delegate double FitnessFunc(Individual individual);
    public class Individual
    {

        public static readonly int BITS_PER_CHROMOSOME = 5;
        public static readonly Random RANDOM = new Random(42);

        public BitArray Chromosome { get; set; }

        public double GetFitnessLevel(FitnessFunc fitnessFunc)
        {
            return fitnessFunc(this);
        }

        public Individual()
        {
            Chromosome = new BitArray(BITS_PER_CHROMOSOME);
        }

        public static Individual GetRandomIndividual()
        {
            var ind = new Individual();
            for (int i = 0; i < BITS_PER_CHROMOSOME; i++)
            {
                ind.Chromosome.Set(i, RANDOM.NextDouble() < 0.5);
            }
            return ind;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < BITS_PER_CHROMOSOME; i++)
            {
                sb.Append(Chromosome[i] ? '1' : '0');
            }
            return sb.ToString();
        }

        public Individual Crossover(Individual partner, int pivot)
        {
            var geneA = new BitArray(this.Chromosome);
            var geneB = new BitArray(partner.Chromosome);
            var ones = Enumerable.Repeat(true, pivot).ToList();
            var zeros = Enumerable.Repeat(false, BITS_PER_CHROMOSOME - pivot).ToList();
            ones.AddRange(zeros);
            var maskA = new BitArray(ones.ToArray());
            var maskB = new BitArray(maskA).Not();

            var finalChromo = new BitArray(BITS_PER_CHROMOSOME);
            finalChromo.Or(geneA.And(maskA));
            finalChromo.Or(geneB.And(maskB));
            return new Individual { Chromosome = finalChromo };
        }
        public Individual Crossover(Individual partner)
        {
            return Crossover(partner, RANDOM.Next(1, Chromosome.Length));
        }

        public Individual[] CrossoverPair(Individual partner)
        {
            var pivot = RANDOM.Next(1, Chromosome.Length);

            //var dd = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("CROSSING OVER {0} with {1} in {2}", this, partner, pivot);
            //Console.ForegroundColor = dd;

            return new[] {Crossover(partner, pivot), partner.Crossover(this,pivot)};
        }
    }
}

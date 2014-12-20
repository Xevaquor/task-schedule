using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Darwin;

namespace DarwinManualTest
{
    class GeneticAlgorithm
    {
        private int step = 1;
        private readonly FitnessFunc fitness;
        private readonly int populationSize;
        private StreamWriter file;

        public GeneticAlgorithm(FitnessFunc fitnessFunc, int populationSize)
        {
            this.fitness = fitnessFunc;
            this.populationSize = populationSize;
            file = new StreamWriter("results");
        }

        public void Evolve(int steps)
        {
            var generation = Population.GetRandomPopulation(populationSize);
            for (int i = 0; i < steps; i++)
            {
                generation = Step(generation);
            }
            Console.WriteLine(generation.ToString(fitness));
        }

        Population Step(Population generation)
        {
            //Extensions.WriteColorLine(ConsoleColor.Green, "======={0} Step======", step++);

            //Console.WriteLine(generation.ToString(fitness));
           // Console.WriteLine("Population fitness sum: {0}",
            //    generation.GetPopulationFitnessSum(fitness));
            var np = new Population { Individuals = new Roulette(fitness, generation).SpinTheWheel(populationSize) };

           // Console.WriteLine(Roulette.ProbabilityToString(generation, fitness));

         //   Console.WriteLine("Selected for reproducing");
//Console.WriteLine(np.ToString(fitness));

            var h = new Harem(fitness, np);
            var newGeneration = h.Reproduce();

           // Console.WriteLine("After reproduction");
          //  Console.WriteLine(newGeneration.ToString(fitness));

         //   Console.WriteLine("After mutation");
            new Mutator().Mutate(ref newGeneration);
         //   Console.WriteLine(newGeneration.ToString(fitness));

            ++step;

            file.WriteLine("{2} {0} {1}", newGeneration.GetPopulationFitnessSum(fitness)/
                                      newGeneration.Individuals.Count,
                newGeneration.Individuals.Max(x => x.GetFitnessLevel(fitness)), step);
            file.Flush();

            return newGeneration;
        }

    }
}

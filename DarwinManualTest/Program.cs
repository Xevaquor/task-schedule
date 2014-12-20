using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Darwin;
using TaskSchedule.Algo;

namespace DarwinManualTest
{
    class Program
    {
        static double fitness(Individual ind)
        {
            int[] array = new int[1];
            ind.Chromosome.CopyTo(array, 0);
            var x = array[0];
            return (x - 17)*(x - 17) + 500;
        }



        static void Main(string[] args)
        {
            var time = Benchmark.MeasureExecutionTime(() =>
            {
                var ga = new GeneticAlgorithm(fitness, 100);
                ga.Evolve(5000);
            });

            Console.WriteLine(time);
            Console.ReadLine();
        }
    }
}

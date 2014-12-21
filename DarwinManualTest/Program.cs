using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Darwin;
using TaskSchedule.Algo;

namespace DarwinManualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MACHINES = 10;
            const int STEPS_TO_PERFORM = 2000;
            const int POPULATION_SIZE = 100;
            int[] jobs = File.ReadAllText("m20.txt").Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            PrintTestCaseSummary(jobs.Length, MACHINES, STEPS_TO_PERFORM, POPULATION_SIZE, jobs);

            var darwin = new GeneticAlgorithm(jobs, MACHINES, STEPS_TO_PERFORM, POPULATION_SIZE);
            var greedy = new ListScheduler();
            
            int darwinResult = -1;
            var darwinTime = Benchmark.MeasureExecutionTime(() =>
            {
                darwinResult = darwin.Evolve();
            });

            var greedyResult = new SchedulingResult();
            var greedyTime = Benchmark.MeasureExecutionTime(() =>
            {
                greedyResult  = greedy.Schedule(jobs, Enumerable.Range(0, MACHINES).Select(x => new Processor(x.ToString())).ToList());
            });

            Console.WriteLine("Greedy: {0} in {1}", greedyResult.ProcessingTime, greedyTime);
            Console.WriteLine("Darwin: {0} in {1}", darwinResult, darwinTime);

            Console.ReadLine();
        }

        private static void PrintTestCaseSummary(int jobs, int machines, int stepsToPerform, int populationSize, int[] jobCosts)
        {
            Console.WriteLine("{0} jobs on {1} machines, {2} genetic steps on population of {3}\nSum of all tasks {4}   ", jobs, machines, stepsToPerform, populationSize, jobCosts.Sum());
        }
    }
}

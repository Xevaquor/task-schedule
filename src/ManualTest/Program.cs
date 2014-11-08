using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.Algo;

namespace ManualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //             0  1  2  3  4
            int[] jobs = { 5, 4, 3, 2, 5 };
            int cpuCount = 6;

            var algo = new BruteForceScheduler();

            var result = algo.Schedule(jobs, cpuCount);

            Console.WriteLine(result.ProcessingTime);
            PrintSchedule(result.Schedule, jobs);

            Console.ReadLine();
        }

        static void PrintSchedule(Dictionary<char, List<int>> schedule, int[] jobs)
        {
            foreach (var cpu in schedule.Keys.OrderBy(x => x))
            {
                Console.Write("\nCPU: {0}: ", cpu);
                Console.Write(string.Join(" ", schedule[cpu]));
            }
            Console.WriteLine("\nGRAPHHHHHH");

            foreach (var cpu in schedule.Keys.OrderBy(x => x))
            {
                Console.Write("\nCPU: {0}: ", cpu);
                for (int i = 0; i < schedule[cpu].Count; i++)
                {
                    int val = jobs[schedule[cpu][i]];
                    char mark = i % 2 == 0 ? '@' : '#';
                    Console.Write(new string( Enumerable.Repeat(mark, val).ToArray()));
                    
                }
            }
        }
    }
}

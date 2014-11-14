using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using TaskSchedule.Algo;

namespace ManualTest
{
    public class Program
    {
        private static readonly IEnumerable<Processor> ProcessorSource = Processor.CreateFromNames("Aries", "Taurus",
            "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio",
            "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Ophiuchus");


        static void Main(string[] args)
        {
            Benchmark b = new Benchmark();
            var jobs = new[] { 1,2,3,5,7,11,13,17,19,40 }.ToArray();
            var cpus = ProcessorSource.Take(3).ToList();

            var bfs = new BruteForceScheduler();
            var ls = new ListScheduler();

            SchedulingResult result = null;
            var x = b.MeasureExecutionTime(() =>
            {
                result = bfs.Schedule(jobs, cpus);
            });

            Console.WriteLine("N-tuples to check {0}", (int)Math.Pow(cpus.Count, jobs.Count()));
            PrintSchedule(result, jobs);
            Console.WriteLine(x.Humanize());

            Console.WriteLine("============================");
            x = b.MeasureExecutionTime(() =>
            {
                result = ls.Schedule(jobs, cpus);
            });
            PrintSchedule(result, jobs);
            Console.WriteLine(x.Humanize(culture: new CultureInfo("pl-PL")));
            Console.ReadLine();
        }

        public static void PrintArray(int[] arr)
        {
            Console.WriteLine(string.Join(" ", arr));
        }

        static void PrintSchedule(SchedulingResult sr, int[] jobCosts)
        {
            Console.WriteLine("Total processing time: {0}", sr.ProcessingTime);

            foreach (var cpu in sr.Schedule.Keys)
            {
                Console.Write("{0}: ", cpu.Name.PadLeft(10));
                int counter = 0;
                foreach (var job in sr.Schedule[cpu].Jobs)
                {
                    if (counter++ % 2 == 0)
                        Console.Write(new string('#', jobCosts[job]));
                    else
                        Console.Write(new string('@', jobCosts[job]));
                }

                Console.WriteLine();
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TaskSchedule.Algo
{
    public interface IScheduler
    {
        SchedulingResult Schedule(int[] jobs, int processorCount);
    }

    public class BruteForceScheduler : IScheduler
    {
        BruteForceGenerator bfg = new BruteForceGenerator();
        private int[] costs;
        public SchedulingResult Schedule(int[] jobs, int processorCount)
        {
            costs = jobs;
            var schedulings = bfg.Generate(jobs.Count(), processorCount);

            var best = schedulings.OrderByDescending(x => ComputeProcessingTime(x).Item2).Last();
            
            return new SchedulingResult
            {
                ProcessingTime = ComputeProcessingTime(best).Item2,
                Schedule = BuildSchedule(best)
            };


        }

        private Dictionary<char, List<int>> BuildSchedule(string s)
        {
            var result = new Dictionary<char, List<int>>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (!result.ContainsKey(c))
                {
                    result.Add(c, new List<int>(){i});
                }
                else
                {
                    result[c].Add(i);
                }
            }
            return result;
        }

        public Tuple<string, int> ComputeProcessingTime(string s)
        {
            var times = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (!times.ContainsKey(c))
                {
                    times.Add(c, costs[i]);
                }
                else
                {
                    times[c] += costs[i];
                }
            }
            Debug.WriteLine(times.Max(x => x.Value));
            return new Tuple<string, int>(s, times.Max(x => x.Value));
        }
    }
}
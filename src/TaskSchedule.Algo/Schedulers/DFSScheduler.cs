#define DFS_PARALLEL_COMPUTE_SCHEDULE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Node = System.Collections.Generic.List<int>;

namespace TaskSchedule.Algo.Schedulers
{
    public class DfsScheduler : IScheduler
    {
        private int upperBoundValue = int.MaxValue;
        private int bestSoFarValue = int.MaxValue;
        private int[] bestSoFarTuple = null;
        private int[] jobs = null;
        private int machinesCount = -1;

        public SchedulingResult Schedule(int[] jobs, List<Processor> processors)
        {
            this.jobs = jobs;
            machinesCount = processors.Count;
            //Seed();

            for (int i = 0; i < jobs.Length; i++)
            {
                buffer.Add((int)Math.Ceiling(jobs.Skip(i).Sum() * 1.0 / machinesCount * 0.9));
            }

            var root = new Node();
            Traverse(root);


            return new SchedulingResult
            {
                ProcessingTime = bestSoFarValue,
                Schedule = GetSchedule(bestSoFarTuple, bestSoFarValue, processors)
            };
        }

        private void Traverse(Node root)
        {
            if (root.Count == 4)
            {
                Console.WriteLine("Visitng " + string.Join(" ", root));
            }

            if (root.Count == jobs.Length)
            {
                // mamy liść
                // Console.WriteLine("Got leaf: " + string.Join(" ", root));
                var leafValue = ComputeSchedule(root);
                if (leafValue < bestSoFarValue)
                {
                    bestSoFarValue = leafValue;
                    bestSoFarTuple = root.ToArray();
                    Extensions.WriteColorLine(ConsoleColor.Green, string.Format("{0} New bound at {1}", bestSoFarValue, string.Join(" ", root)));
                }

                return;
            }
            var estimate = ComputeEstimation(root.Count);
            // odwiedź
            //Console.WriteLine("Visiting " + string.Join(" ", root));
            // jeśli gorzej niż to co mamy to nie ma sensu grzebać dalej
            var nodeValue = ComputeSchedule(root);
            if (nodeValue + estimate > bestSoFarValue)
            {
                return;
            }

            foreach (var descendatn in GetDescendatns(root))
            {
                Traverse(descendatn);
            }



        }

        List<int> buffer = new Node();
        private int ComputeEstimation(int used)
        {
            return buffer[used];
            //return (int)Math.Ceiling(jobs.Skip(used).Sum() * 1.0 / machinesCount);
            //return jobs.Skip(used).Max();
        }

        private void Seed()
        {
            var r = new Random(17);
            var initialGuess = new int[jobs.Length];
            for (int i = 0; i < jobs.Length; i++)
            {
                initialGuess[i] = (r.Next(machinesCount));
            }
            bestSoFarValue = ComputeSchedule(initialGuess.ToList());
            bestSoFarTuple = initialGuess;
        }

        [DebuggerStepThrough]
        public IEnumerable<Node> GetDescendatns(Node node)
        {
            for (int i = 0; i < machinesCount; i++)
            {
                var desc = new Node(node) { i };
                yield return desc;
            }
        }

        public string GetDescription(int machinesCount, int taskCount)
        {
            return "DFS algo";
        }
        private Dictionary<Processor, SingleProcessorSchedule> GetSchedule(int[] tuple, int cost, List<Processor> processors)
        {
            var res = processors.ToDictionary(cpu => cpu, cpu => new SingleProcessorSchedule { ProcessingTime = cost, Jobs = new List<int>() });

            for (var i = 0; i < tuple.Length; i++)
            {
                var t = tuple[i];
                res[processors[t]].Jobs.Add(i);
            }
            return res;
        }

        // [DebuggerStepThrough]
        private int ComputeSchedule(List<int> tuple)
        {
            var costs = new int[machinesCount];

            for (int i = 0; i < tuple.Count; i++)
            {
                var cost = jobs[i];
                costs[tuple[i]] += cost;
            }
            return costs.Max();
        }
    }
}

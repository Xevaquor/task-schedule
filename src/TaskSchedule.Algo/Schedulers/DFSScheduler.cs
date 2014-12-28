using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
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
            if (root.Count == jobs.Length)
            {
                // mamy liść
               // Console.WriteLine("Got leaf: " + string.Join(" ", root));
                var leafValue = ComputeSchedule(root.ToArray());
                if (leafValue < bestSoFarValue)
                {
                    bestSoFarValue = leafValue;
                    bestSoFarTuple = root.ToArray();
                    Extensions.WriteColorLine(ConsoleColor.Green,  string.Format("{0} New bound at {1}", bestSoFarValue ,string.Join(" ", root)));
                }

                return;
            }
            // odwiedź
            //Console.WriteLine("Visiting " + string.Join(" ", root));
            // jeśli gorzej niż to co mamy to nie ma sensu grzebać dalej
            var nodeValue = ComputeSchedule(root.ToArray());
            if (nodeValue >= bestSoFarValue)
            {
                var deep = jobs.Length - root.Count;
                var profit = BigInteger.Pow(machinesCount, deep);
                if (profit > BigInteger.Parse("0"))
                {
                    Extensions.WriteColorLine(ConsoleColor.Red, "CUTTING OFF " + profit);
                    Extensions.WriteColorLine(ConsoleColor.Red, "CUTTING OFF " + string.Join(" ", root));
                }
                return;

            }




            foreach (var descendatn in GetDescendatns(root))
            {
                Traverse(descendatn);
            }



    }

        private void Seed()
        {
            var r = new Random(17);
            var initialGuess = new int[jobs.Length];
            for (int i = 0; i < jobs.Length; i++)
            {
                initialGuess[i] = (r.Next(machinesCount));
            }
            bestSoFarValue = ComputeSchedule(initialGuess);
            bestSoFarTuple = initialGuess;
        }

        [DebuggerStepThrough]
        public IEnumerable<Node> GetDescendatns(Node node)
        {
            for (int i = 0; i < machinesCount; i++)
            {
                var desc = new Node(node) {i};
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

        [DebuggerStepThrough]
        private int ComputeSchedule(int[] tuple)
        {
            var costs = new int[machinesCount];

            Parallel.For(0, tuple.Length, i =>
            {
                var cost = jobs[i];
                costs[tuple[i]] += cost;
            });

            return costs.Max();
        }
    }
}

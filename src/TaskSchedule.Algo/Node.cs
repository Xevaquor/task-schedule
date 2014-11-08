using System.Collections.Generic;
using System.Diagnostics;

namespace TaskSchedule.Algo
{
    public class Node
    {
        public int Processor { get; set; }

        List<Node> Children { get; set; }

        [DebuggerStepThrough]
        public Node()
        {
            Children = new List<Node>();
            Processor = -1;
        }

        public List<Node> AppendChildren(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Children.Add(new Node { Processor = i });
            }
            return Children;
        }

        public override string ToString()
        {
            return string.Format("CPU: {0}, Children: {1}", Processor, Children.Count);
        }
    }
}
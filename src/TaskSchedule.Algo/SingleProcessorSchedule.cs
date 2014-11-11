using System.Collections.Generic;

namespace TaskSchedule.Algo
{
    [Equals]
    public class SingleProcessorSchedule
    {
        public int ProcessingTime { get; set; }
        public List<int> Jobs { get; set; }

        public SingleProcessorSchedule()
        {
            Jobs = new List<int>();
        }
    }
}

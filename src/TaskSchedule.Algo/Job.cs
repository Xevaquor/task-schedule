namespace TaskSchedule.Algo
{
    [Equals]
    public class Job
    {
        public uint ExecutionTime { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Job: {0}, {1}", Name, ExecutionTime);
        }
    }
}
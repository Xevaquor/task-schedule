using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

namespace TaskSchedule.Algo
{
    public class JobFactory
    {
        public IEnumerable<Job> CreateCollection(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Job { Name = (i + 1).ToString(CultureInfo.InvariantCulture), ExecutionTime = 1 };
            }
        }
    }
}
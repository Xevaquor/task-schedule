using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TaskSchedule.Algo
{
    public class Benchmark
    {
        public TimeSpan MeasureExecutionTime(Action func)
        {
            GC.Collect();
            var sw = Stopwatch.StartNew();
            func.Invoke();
            sw.Stop();
            return sw.Elapsed;
        }
    }
}

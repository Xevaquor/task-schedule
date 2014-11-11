using System.Collections.Generic;
using System.Linq;

namespace TaskSchedule.Algo
{
    [Equals]
    public class Processor
    {
        public string Name { get; set; }

        public Processor(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<Processor> CreateFromNames(params string[] names)
        {
            return names.Select(name => new Processor(name));
        }
    }
}

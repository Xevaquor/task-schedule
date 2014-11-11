using System;
using System.Collections.Generic;

namespace TaskSchedule.Algo
{
    public class NTupleGenerator
    {
        public IEnumerable<int[]> GenerateNTuples(int digits, int @base)
        {
            var iterations = (int)Math.Pow(@base, digits);
            
            var tuple = new int[digits];
            yield return tuple;
            
            for (int i = 0; i < iterations - 1; i++)
            {
                tuple = Next(tuple, 0, @base);
                yield return tuple;
            }
        }

        private int[] Next(int[] prev, int offset, int maxVal)
        {
            if (offset >= prev.Length)
            {
                throw new OverflowException();
            }

            //dodaj na ostatniej pozycji
            prev[offset]++;

            //przeniesienie
            if (prev[offset] >= maxVal)
            {
                prev[offset] = 0;
                return Next(prev, offset + 1, maxVal);
            }

            return prev;
        }
    }
}

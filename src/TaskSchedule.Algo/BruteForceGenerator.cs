using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace TaskSchedule.Algo
{
    public class BruteForceGenerator
    {
        public IEnumerable<string> Generate(int jobs, int cpus)
        {
            const int lowerBound = 0;
            int upperBound = (int) Math.Pow(cpus, jobs);

            for (int i = lowerBound; i < upperBound; i++)
            {
                yield return IntToString(i, GetBaseChars(cpus), jobs);
            }
        }

        private char[] GetBaseChars(int cpus)
        {
            return "ABCDEFGHIJKLNMOPRSTQUWYZ".Take(cpus).ToArray();
        }

        public string IntToString(int value, char[] baseChars, int digits)
        {
            string result = string.Empty;
            int targetBase = baseChars.Length;

            do
            {
                result = baseChars[value % targetBase] + result;
                value = value / targetBase;
            }
            while (value > 0);
            
            return result.PadLeft(digits, baseChars[0]);
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;
using Xunit.Should;

namespace TaskSchedule.Algo.Test
{
    public class BruteForceGeneratorTests
    {
        private BruteForceGenerator bf = new BruteForceGenerator();

        [Theory]
        [InlineData(1, 1, new[] { "A" })]
        [InlineData(2, 1, new[] { "AA" })]
        [InlineData(2, 2, new[] { "AA", "BB", "AB", "BA" })]
        public void basic_generates_all_sequences(int jobCount, int cpuCount, IEnumerable<string> expected)
        {
            var actual = bf.Generate(jobCount, cpuCount);
            
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
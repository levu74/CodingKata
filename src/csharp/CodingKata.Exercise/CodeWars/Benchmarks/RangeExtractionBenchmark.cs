using BenchmarkDotNet.Attributes;
using CodingKata.Exercise.Benchmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class RangeExtractionBenchmark : IBestVoteComparisonBenchmark
    {
        [Params(new[] { -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20 },
            new[] { -3, -2, -1, 2, 10, 15, 16, 18, 19, 20 },
            new[] { 1, 2 })]
        public int[] Numbers { get; set; }

        [Benchmark(Baseline = true)]
        public void MySolution()
        {
            new RangeExtraction.Kata().Extract(Numbers);
        }

        [Benchmark]
        public void BestVote()
        {
            new RangeExtraction.KataBestVote().Extract(Numbers);
        }
    }
}

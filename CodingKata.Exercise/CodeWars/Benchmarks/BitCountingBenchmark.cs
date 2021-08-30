using BenchmarkDotNet.Attributes;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class BitCountingBenchmark
    {

        [Params(1, 2, 3, 5, 8, 13, 21, 34, 77231418)]
        public int Number { get; set; }

        [Benchmark]
        public void MySolution()
        {
            new BitCounting.Kata().CountBits(Number);
        }


        [Benchmark]
        public void BestVote()
        {
            new BitCounting.KataBestVote().CountBits(Number);
        }
    }
}

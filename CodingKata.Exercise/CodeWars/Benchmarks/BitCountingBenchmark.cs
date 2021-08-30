using BenchmarkDotNet.Attributes;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class BitCountingBenchmark
    {
        private const int Number = 500;

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

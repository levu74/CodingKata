using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class MorseCodeDecoderBenchmark
    {
        private const string MorseCode = ".... . -.--   .--- ..- -.. .";

        [Benchmark]
        public void MySolution()
        {
            new MorseCodeDecoder.Kata().Decode(MorseCode);
        }


        [Benchmark]
        public void BestVote()
        {
            new MorseCodeDecoder.KataBestVote().Decode(MorseCode);
        }
    }
}

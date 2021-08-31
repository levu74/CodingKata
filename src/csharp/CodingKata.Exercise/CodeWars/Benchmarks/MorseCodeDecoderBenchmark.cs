using BenchmarkDotNet.Attributes;
using CodingKata.Exercise.Benchmark;
using CodingKata.Exercise.CodeWars.MorseCodeDecoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class MorseCodeDecoderBenchmark : IBestVoteComparisonBenchmark
    {
        [Params(".... . -.--   .--- ..- -.. .")]
        public string MorseCode { get; set; }

        [Benchmark]
        public void MySolution()
        {
            new Kata().Decode(MorseCode);
        }


        [Benchmark]
        public void BestVote()
        {
            new KataBestVote().Decode(MorseCode);
        }
    }
}

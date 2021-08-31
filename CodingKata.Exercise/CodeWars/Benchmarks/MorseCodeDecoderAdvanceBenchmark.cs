using BenchmarkDotNet.Attributes;
using CodingKata.Exercise.Benchmark;
using CodingKata.Exercise.CodeWars.MorseCodeDecoderAdvance;
using CodingKata.Exercise.CodeWars.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class MorseCodeDecoderAdvanceBenchmark : IBestVoteComparisonBenchmark
    {
        [Params(MorseCodeBitLibrary.I_1, MorseCodeBitLibrary.THE_QUICK_BROWN_FOX_JUMPS_OVER_THE_LAZY_DOG_dot, MorseCodeBitLibrary.HEY_JUDE_2)]
        public string Bits { get; set; }
      
        [Benchmark]
        public void MySolution()
        {
            new Kata().DecodeBits(Bits);
        }

        [Benchmark]
        public void BestVote()
        {
            new KataBestVote().DecodeBits(Bits);
        }
    }
}

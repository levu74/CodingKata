using BenchmarkDotNet.Attributes;
using CodingKata.Exercise.Benchmark;
using CodingKata.Exercise.CodeWars.MorseCodeDecoderRealLife;
using CodingKata.Exercise.CodeWars.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class MorseCodeDecoderRealLifeBenchmark : IMySolutionBenchmark
    {
        [Params(MorseCodeBitLibrary.THE_QUICK_BROWN_FOX_JUMPS_OVER_THE_LAZY_DOG_RL, MorseCodeBitLibrary.HEY_JUDE_1_3)]
        public string Bits { get; set; }
      
        [Benchmark(Baseline = true)]
        public void MySolution()
        {
            new Kata().DecodeBitsAdvanced(Bits);
        }

        [Benchmark]
        public void StandardDeviation()
        {
            new KataStandardDeviation().DecodeBitsAdvanced(Bits);
        }
    }
}

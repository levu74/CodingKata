﻿using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using CodingKata.Exercise.CodeWars.Benchmarks;
using System;

namespace CodingKata.Exercise.Array
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<BitCountingBenchmark>(DefaultConfig());
            BenchmarkDotNet.Running.BenchmarkRunner.Run<MorseCodeDecoderBenchmark>(DefaultConfig());
            BenchmarkDotNet.Running.BenchmarkRunner.Run<MorseCodeDecoderAdvanceBenchmark>(DefaultConfig());
        }

        private static ManualConfig DefaultConfig()
        {
            return BenchmarkDotNet.Configs.DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser());
        }

        private static MemoryDiagnoser MemoryDiagnoser()
        {
            return new MemoryDiagnoser(new MemoryDiagnoserConfig());
        }
    }
}

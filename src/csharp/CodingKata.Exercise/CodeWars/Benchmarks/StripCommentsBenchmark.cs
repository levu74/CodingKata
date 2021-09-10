using BenchmarkDotNet.Attributes;
using CodingKata.Exercise.Benchmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKata.Exercise.CodeWars.Benchmarks
{
    public class StripCommentsBenchmark : IBestVoteComparisonBenchmark
    {
        [Params("apples, pears # and bananas\ngrapes\nbananas !apples", "a #b\nc\nd $e f g")]
       
        public string Input { get; set; }
        [Params(new [] { "#", "!" }, new [] { "#", "$" })]
        public string[] CommentSymbols { get; set; }

        [Benchmark(Baseline = true)]
        public void MySolution()
        {
            new StripComments.Kata().StripComments(Input, CommentSymbols);
        }

        [Benchmark]
        public void BestVote()
        {
            new StripComments.KataBestVote().StripComments(Input, CommentSymbols);
        }
    }
}

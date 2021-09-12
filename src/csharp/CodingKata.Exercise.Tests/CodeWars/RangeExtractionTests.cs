using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingKata.Exercise.CodeWars.RangeExtraction;
using FluentAssertions;
using Xunit;

namespace CodingKata.Exercise.Tests.CodeWars
{
    public class RangeExtractionTests
    {
        private Kata _sut;

        public RangeExtractionTests()
        {
            _sut = new Kata();
        }

        [Theory]
        [InlineData(new[] { 1, 2 }, "1,2")]
        [InlineData(new[] { 1, 2, 3 }, "1-3")]
        [InlineData(new[] { -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20 }, "-6,-3-1,3-5,7-11,14,15,17-20")]
        [InlineData(new[] { -3, -2, -1, 2, 10, 15, 16, 18, 19, 20 }, "-3--1,2,10,15,16,18-20")]
        [InlineData(new[] { 47, 49, 50, 52, 53, 55 }, "47,49,50,52,53,55")]
        [InlineData(new[] { 83, 85, 87, 88, 89, 91, 92 }, "83,85,87-89,91,92")]
        public void SimpleTests(int[] numbers, string expected)
        {
           _sut.Extract(numbers).Should().Be(expected);
        }

       
        
        [Theory]
        [InlineData(new[] { -56, -54,-53,-52, -50 }, "-56,-54--52,-50")]
        public void RandomTests(int[] numbers, string expected)
        {
            _sut.Extract(numbers).Should().Be(expected);
        }
    }
}

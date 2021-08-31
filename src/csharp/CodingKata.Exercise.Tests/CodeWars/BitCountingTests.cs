using CodingKata.Exercise.CodeWars.BitCounting;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingKata.Exercise.Tests.CodeWars
{
    
    public class BitCountingTests
    {
        private Kata _sut;

        public BitCountingTests()
        {
            _sut = new Kata();
        }
        [Theory]
        [InlineData(10, 2)]
        [InlineData(9, 2)]
        [InlineData(7, 3)]
        [InlineData(77231418, 14)]
        public void CountBitsTest(int decimalNumber, int bitCount)
        {
            var bitCountResult = _sut.CountBits(decimalNumber);
            bitCountResult.Should().Be(bitCount);
        }
    }
}

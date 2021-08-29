using CodingKata.Exercise.CodeWars.MorseCodeDecoder;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingKata.Exercise.Tests.CodeWars
{
    public class MorseCodeDecoderTests
    {
        private Kata _sut;

        public MorseCodeDecoderTests()
        {
            _sut = new Kata();
        }
        [Theory]
       
        [InlineData(".... . -.--   .--- ..- -.. .", "HEY JUDE")]
        public void DecodeTest(string morseCode, string expected)
        {
            var decode = _sut.Decode(morseCode);

            decode.Should().Be(expected);
        }
    }
}

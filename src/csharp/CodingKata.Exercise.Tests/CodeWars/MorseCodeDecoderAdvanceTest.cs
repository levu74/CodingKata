using CodingKata.Exercise.CodeWars.MorseCodeDecoderAdvance;
using CodingKata.Exercise.CodeWars.Utils;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingKata.Exercise.Tests.CodeWars
{
    public class MorseCodeDecoderAdvanceTest
    {
        private Kata _sut;

        public MorseCodeDecoderAdvanceTest()
        {
            _sut = new Kata();
        }
        [Theory]
        [InlineData(MorseCodeBitLibrary.DE_2, "DE")]
        [InlineData(MorseCodeBitLibrary.HEY_JUDE_2, "HEY JUDE")]
        [InlineData(MorseCodeBitLibrary.E_1, "E")]
        [InlineData(MorseCodeBitLibrary.THE_QUICK_BROWN_FOX_JUMPS_OVER_THE_LAZY_DOG_dot, "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG.")]
        [InlineData(MorseCodeBitLibrary.E_3, "E")]
        [InlineData(MorseCodeBitLibrary.I_1, "I")]
        public void DecodeMorseFromBits(string bits, string expected)
        {
            var decode = _sut.DecodeMorse(_sut.DecodeBits(bits));
            decode.Should().Be(expected);
        }

        [Theory]
        [InlineData(MorseCodeBitLibrary.HEY_JUDE_2, ".... . -.--   .--- ..- -.. .")]
        [InlineData("1",".")]
        [InlineData("111",".")]
        public void DecodeBits(string bits, string expectedMorseCode)
        {
            var decode = _sut.DecodeBits(bits);
            decode.Should().Be(expectedMorseCode);
        }

        [Theory]
        [InlineData(".... . -.--   .--- ..- -.. .", "HEY JUDE")]
        public void DecodeMorse(string morseCode, string expected)
        {
            var decode = _sut.DecodeMorse(morseCode);
            decode.Should().Be(expected);
        }


        [Theory]
        [InlineData("111111111111111000000000000000111110000011111000001111100000", 5)]
        [InlineData("111", 3)]
        [InlineData("1", 1)]
        [InlineData(MorseCodeBitLibrary.DE_2, 2)]
        [InlineData("101", 1)]
        public void TestCaclulateTransmissionRate(string bits, int expected)
        {
            int transmissionRate = _sut.CaclulateTransmissionRate(bits);
            transmissionRate.Should().Be(expected);
        }
    }
}

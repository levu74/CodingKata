using CodingKata.Exercise.CodeWars.MorseCodeDecoderRealLife;
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
    public class MorseCodeDecoderRealLifeTest
    {
        private IMorseCodeDecoderRealLife _sut;

        public MorseCodeDecoderRealLifeTest()
        {
            _sut = new Kata();
        }

        [Theory]
        [InlineData(MorseCodeBitLibrary.HEY_JUDE_1_3, "HEY JUDE")]
        [InlineData("110011001100110000001100000011111100110011111100111111", "HEY")]
        [InlineData(MorseCodeBitLibrary.THE_QUICK_BROWN_FOX_JUMPS_OVER_THE_LAZY_DOG_RL, "THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG")]
        public void TestBasicDecodeMorse(string bits, string expected)
        {
            _sut.DecodeMorse(_sut.DecodeBitsAdvanced(bits)).Should().Be(expected);
        }

        [Theory]
        [InlineData("0000", "")]
        [InlineData("0", "")]
        public void TestWholeZeroReturnEmpty(string bits, string expected)
        {
            _sut.DecodeMorse(_sut.DecodeBitsAdvanced(bits)).Should().Be(expected);
        }

        [Theory]
        [InlineData("111", "E")]
        [InlineData("1", "E")]
        [InlineData("11", "E")]
        public void TestWholeOneBitsReturnDot(string bits, string expected)
        {
            _sut.DecodeMorse(_sut.DecodeBitsAdvanced(bits)).Should().Be(expected);
        }


        [Theory]
        [InlineData("110011", "I")]
        [InlineData("101", "I")]
        public void TestStableCase(string bits, string expected)
        {
            _sut.DecodeMorse(_sut.DecodeBitsAdvanced(bits)).Should().Be(expected);
        }


        [Theory]
        [InlineData("1001", "EE", Skip = "Need more information")]
        [InlineData("10001", "EE")]
        [InlineData("10000001", "E E")]
        [InlineData("100001", "EE")]
        public void TestMissingItem(string bits, string expected)
        {
            _sut.DecodeMorse(_sut.DecodeBitsAdvanced(bits)).Should().Be(expected);
        }

    }
}

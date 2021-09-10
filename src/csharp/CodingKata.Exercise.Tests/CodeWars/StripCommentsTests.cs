using CodingKata.Exercise.CodeWars.StripComments;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingKata.Exercise.Tests.CodeWars
{
    public class StripCommentsTests
    {
        private Kata _sut;

        public StripCommentsTests()
        {
            _sut = new Kata();
        }

        [Theory]
        [InlineData("apples, pears # and bananas\ngrapes\nbananas !apples", new string[] { "#", "!" }, "apples, pears\ngrapes\nbananas")]
        [InlineData("a #b\nc\nd $e f g", new string[] { "#", "$" }, "a\nc\nd")]
        [InlineData("a \n b \nc ", new string[] { "#", "$" }, "a\n b\nc")]
        [InlineData("\n\nFD\n\nDE\n\n\n\n\nB", new string[] { "#", "$" }, "\n\nFD\n\nDE\n\n\n\n\nB")]
        [InlineData("\nC\n\n\nE\n\nC\n\nE\n\nF\n\nE\n\nEB\n\nDF\n\nE\n\nCE\n\nD\n\nC\n\n\n\nD\n\n\n\n\n\nB\n\nEE\n\n\n\n\n", new string[] { "#", "$" }, "\nC\n\n\nE\n\nC\n\nE\n\nF\n\nE\n\nEB\n\nDF\n\nE\n\nCE\n\nD\n\nC\n\n\n\nD\n\n\n\n\n\nB\n\nEE\n\n\n\n\n")]
        public void TaskBasicCase(string input, string[] commentSymbols, string expected)
        {
            _sut.StripComments(input, commentSymbols).Should().Be(expected);
        }
    }
}

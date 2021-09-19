using CodingKata.Exercise.CodeWars.SudokuSolutionValidator;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingKata.Exercise.Tests.CodeWars
{
    public class SudokuSolutionValidatorTests
    {
        public SudokuSolutionValidatorTests()
        {
            _sut = new CodingKata.Exercise.CodeWars.SudokuSolutionValidator.Kata();
        }

        private Kata _sut;

        [Fact]
        public void Test_IsValid()
        {
            bool result = _sut.ValidateSolution(new int[][]
            {
                new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
            });

            result.Should().BeTrue();
        }

        [Fact]
        public void Test_IsInvalid()
        {
            bool result = _sut.ValidateSolution(new int[][]
            {
                new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new int[] {1, 9, 8, 3, 0, 2, 5, 6, 7},
                new int[] {8, 5, 0, 7, 6, 1, 4, 2, 3},
                new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new int[] {7, 0, 3, 9, 2, 4, 8, 5, 6},
                new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new int[] {3, 0, 0, 2, 8, 6, 1, 7, 9},
            });

            result.Should().BeFalse();
        }
    }
}

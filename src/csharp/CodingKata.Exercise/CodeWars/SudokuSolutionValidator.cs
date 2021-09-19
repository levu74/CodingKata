using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingKata.Exercise.CodeWars.SudokuSolutionValidator
{
    public interface ISudokuSolutionValidator
    {
        bool ValidateSolution(int[][] board);
    }

    public class Kata : ISudokuSolutionValidator
    {
        public bool ValidateSolution(int[][] board)
        {
            int numColumns = board[0].Length;
            int numRows = board.Length;
            int numBlockColumns = numColumns / 3;
            int numBlockRows = numRows / 3;
            var posibleNumbers = Enumerable.Range(1, 9).ToList();

            // Columns
            for (int colIndex = 0; colIndex < numColumns; colIndex++)
            {
                HashSet<int> numbers = new HashSet<int>(posibleNumbers);
                for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
                {
                    int cellNumber = board[rowIndex][colIndex];
                    if (numbers.Contains(cellNumber))
                    {
                        numbers.Remove(cellNumber);
                    }
                }
                if (numbers.Count > 0)
                {
                    return false;
                }
            }
            // Rows
            for (int rowIndex = 0; rowIndex < numRows; rowIndex++)
            {
                HashSet<int> numbers = new HashSet<int>(posibleNumbers);
                for (int colIndex = 0; colIndex < numColumns; colIndex++)
                {
                    int cellNumber = board[rowIndex][colIndex];
                    if (numbers.Contains(cellNumber))
                    {
                        numbers.Remove(cellNumber);
                    }
                }
                if (numbers.Count > 0)
                {
                    return false;
                }
            }
            // 3x3 sub-grids
            for (int blockIndex = 0; blockIndex < numBlockColumns * numBlockRows; blockIndex++)
            {
                int colDelta = (blockIndex % numBlockColumns) * 3;
                int rowDelta = (blockIndex / numBlockColumns) * 3;
                HashSet<int> numbers = new HashSet<int>(posibleNumbers);
                for (int cellIndex = 0; cellIndex < 9; cellIndex++)
                {
                    int cellNumber = board[rowDelta + cellIndex / 3][colDelta + cellIndex % 3];
                    if (numbers.Contains(cellNumber))
                    {
                        numbers.Remove(cellNumber);
                    }
                }
                if (numbers.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

      
    }

    public class KataBestVote : ISudokuSolutionValidator
    {
        private static int[] nineNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public bool ValidateSolution(int[][] board)
        {
            for (int i = 0; i < 9; ++i)
            {
                var row = new List<int>();
                for (int j = 0; j < 9; ++j) row.Add(board[i][j]);
                if (!ValidateNine(row)) return false;

                var col = new List<int>();
                for (int j = 0; j < 9; ++j) col.Add(board[j][i]);
                if (!ValidateNine(col)) return false;

                var block = new List<int>();
                int br = (i / 3) * 3;
                int bc = (i % 3) * 3;
                for (int j = 0; j < 9; ++j) block.Add(board[br + j / 3][bc + j % 3]);
                if (!ValidateNine(block)) return false;
            }

            return true;
        }

        private bool ValidateNine(IList<int> nine)
        {
            return nineNumbers.All(nine.Contains);
        }
    }
}
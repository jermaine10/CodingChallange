using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[TestFixture]
public class Psuedo_Random_Tests
{
    private static Random rnd = new Random();

    private static object[] testCases = new object[]
    {
      new object[]
      {
        true,
        new int[][]
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
        },
      },
      new object[]
      {
        false,
        new int[][]
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
        },
      },
      new object[]
      {
        false,
        new int[][]
        {
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
        },
      },
      new object[]
      {
        true,
        new int[][]
        {
          new int[] {8, 2, 6, 3, 4, 7, 5, 9, 1},
          new int[] {7, 3, 5, 8, 1, 9, 6, 4, 2},
          new int[] {1, 9, 4, 2, 6, 5, 8, 7, 3},
          new int[] {3, 1, 7, 5, 8, 4, 2, 6, 9},
          new int[] {6, 5, 9, 1, 7, 2, 4, 3, 8},
          new int[] {4, 8, 2, 9, 3, 6, 7, 1, 5},
          new int[] {9, 4, 8, 7, 5, 1, 3, 2, 6},
          new int[] {5, 6, 1, 4, 2, 3, 9, 8, 7},
          new int[] {2, 7, 3, 6, 9, 8, 1, 5, 4},
        },
      },
      new object[]
      {
        false,
        new int[][]
        {
          new int[] {1, 2, 6, 3, 4, 7, 5, 9, 8},
          new int[] {7, 3, 5, 8, 1, 9, 6, 4, 2},
          new int[] {1, 9, 4, 2, 6, 5, 8, 7, 3},
          new int[] {3, 1, 7, 5, 8, 4, 2, 6, 9},
          new int[] {6, 5, 9, 1, 7, 2, 4, 3, 8},
          new int[] {4, 8, 2, 9, 3, 6, 7, 1, 5},
          new int[] {9, 4, 8, 7, 5, 1, 3, 2, 6},
          new int[] {5, 6, 1, 4, 2, 3, 9, 8, 7},
          new int[] {2, 7, 3, 6, 9, 8, 1, 5, 4},
        },
      },
      new object[]
      {
        false,
        new int[][]
        {
          new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
          new int[] {2, 3, 1, 5, 6, 4, 8, 9, 7},
          new int[] {3, 1, 2, 6, 4, 5, 9, 7, 8},
          new int[] {4, 5, 6, 7, 8, 9, 1, 2, 3},
          new int[] {5, 6, 4, 8, 9, 7, 2, 3, 1},
          new int[] {6, 4, 5, 9, 7, 8, 3, 1, 2},
          new int[] {7, 8, 9, 1, 2, 3, 4, 5, 6},
          new int[] {8, 9, 7, 2, 3, 1, 5, 6, 4},
          new int[] {9, 7, 8, 3, 1, 2, 6, 4, 5},
        },
      },
    }.OrderBy(_ => rnd.Next()).ToArray();

    private static string stringify(int[][] board)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < board.Length; ++i)
        {
            result.Append("\n{")
                  .Append(String.Join(", ", board[i]))
                  .Append("}");
        }

        return result.ToString().Trim();
    }

    [Test, TestCaseSource("testCases")]
    public void Test(bool expected, int[][] board)
    {
        Assert.AreEqual(expected, SudokuValidation.ValidateSolution(board), "Test:\n" + stringify(board));
    }
}
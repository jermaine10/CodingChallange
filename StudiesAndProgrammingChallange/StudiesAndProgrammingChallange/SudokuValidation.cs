using System.Collections.Generic;
using System.Linq;
public class SudokuValidation
{
    public static bool ValidateSolution(int[][] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int[] x = GetSection(i, j, board);
                if (x.Distinct().Count() != 9)
                    return false;
            }
        }
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i].Distinct().Count() != 9 || board[i].Contains(0))
                return false;
            var columnArr = new int[9];
            for (int j = 0; j < board.Length; j++)
            {
                columnArr[j] = board[j][i];
            }
            if (columnArr.Distinct().Count() != 9 || columnArr.Contains(0))
            {
                return false;
            }
        }

        return true;
    }

    private static int[] GetSection(int x, int y, int[][] v)
    {
        List<int> result = new List<int>();
        for (int i = 3 * x; i < 3 * (x + 1); i++)
        {
            for (int j = 3 * y; j < 3 * (y + 1); j++)
            {
                result.Add(v[i][j]);
            }
        }
        return result.ToArray();
    }
}
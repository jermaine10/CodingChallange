using System;
using System.Linq;

public class ExchangeSort
{
    public int Sort(int[] sequence)
    {
        var x = sequence.OrderBy(d => d).ToList();
        var result = sequence;
        int count = 0;
        var values = sequence.Distinct().OrderBy(d => d).ToList();
        int[][] valueLength = new int[values.Count][];
        for (int i = 0; i < values.Count; i++)
        {
            valueLength[i] = new int[] { x.IndexOf(values[i]), x.Where(d => d == values[i]).Count() };
        }

        for (int i = 0; i < values.Count; i++)
        {
            for (int j = valueLength[i][0]; j < valueLength[i][0] + valueLength[i][1]; j++)
            {
                if (result[j] != values[i])
                {
                    var indexOfVal = values.IndexOf(sequence[j]);
                    bool f = false;
                    for (int k = valueLength[indexOfVal][0]; k < valueLength[indexOfVal][1] + valueLength[indexOfVal][0]; k++)
                    {
                        if (sequence[k] == values[i])
                        {
                            result = Swap(k, j, result);
                            count++;
                            f = true;
                            break;

                        }


                    }
                    if (!f)
                    {
                        result = Swap(j, Array.LastIndexOf(result, values[i]), result);
                        count++;
                    }

                    continue;
                }
            }
        }
        return count;
    }
    private static int[] Swap(int i, int j, int[] data)
    {
        int temp = data[i];
        data[i] = data[j];
        data[j] = temp;
        return data;
    }
}
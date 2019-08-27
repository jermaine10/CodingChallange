using System;
using System.Collections.Generic;
using System.Linq;
public class ClosestText
{
    private IEnumerable<string> words;

    public ClosestText(IEnumerable<string> words)
    {
        this.words = words;
    }

    public string FindMostSimilar(string term)
    {
        if (words.Contains(term))
            return term;
        else
        {
            return GetDifferences(term, words);
        }
    }
    private string GetDifferences(string term, IEnumerable<string> words)
    {
        var min = int.MaxValue;
        string result = "";
        foreach (var x in words)
        {
            int lengthDifference = Math.Abs(term.Length - x.Length);


            var s = x.Length >= term.Length ? GetTextDifference(term, x) : GetTextDifference(x, term);
            if (s < min)
            {
                min = s;
                result = x;
            }
        }
        return result;
    }


    private int GetTextDifference(string smallerString, string bigString)
    {
        int currentPos = -1;
        int d = 0;
        var lengthDif = bigString.Length - smallerString.Length;

        for (int i = 0; i < smallerString.Length; i++)
        {
            int charPos = bigString.IndexOf(smallerString[i]);

            if (charPos >= 0 && currentPos == -1)
            {
                if (Math.Abs(charPos - i) > lengthDif)
                {
                    continue;
                }
                d += charPos > smallerString.IndexOf(smallerString[i]) ? charPos : smallerString.IndexOf(smallerString[i]);
                currentPos = charPos + 1;
                continue;
            }
            if (bigString.Length <= currentPos)
                d++;
            else if (currentPos != -1)
            {
                if (!bigString[currentPos].Equals(smallerString[i]))
                    d++;
                currentPos++;
            }
        }
        if (currentPos == -1)
            return bigString.Length;
        if (currentPos < bigString.Length)
            return d + bigString.Length - (currentPos);
        return d;
    }
}

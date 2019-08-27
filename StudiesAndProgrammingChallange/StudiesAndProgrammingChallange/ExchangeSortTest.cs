using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
[TestFixture]
public class CsharpTest
{
    public int An(int[] l)
    {
        int a = l.Count(x => x == 7), b = l.Count(x => x == 9);
        if (b > 0) return a - l.Take(a).Count(x => x == 7) + Math.Max(l.Skip(a).Take(l.Length - a - b).Count(x => x == 9), l.Skip(l.Length - b).Count(x => x == 8));
        var t = l.OrderBy(x => x).ToArray();
        var r = 0;
        for (var i = 0; i < t.Length; i++) if (t[i] > l[i]) r++;
        return r;
    }
}
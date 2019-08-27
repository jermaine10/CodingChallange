using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class ClosestTextTest
{
    private Random random = new Random();

    [Test]
    public void TestDictionary1()
    {
        ClosestText kata = new ClosestText(new List<string> { "cherry", "pineapple", "melon", "strawberry", "raspberry", "apple", "coconut", "banana" }.OrderBy(o => random.Next()));
        Assert.AreEqual("strawberry", kata.FindMostSimilar("strawbery"));
        Assert.AreEqual("cherry", kata.FindMostSimilar("berry"));
        Assert.AreEqual("apple", kata.FindMostSimilar("aple"));
    }

    [Test]
    public void TestDictionary2()
    {
        ClosestText kata = new ClosestText(new List<string> { "stars", "mars", "wars", "codec", "code", "codewars" }.OrderBy(o => random.Next()));
        Assert.AreEqual("codewars", kata.FindMostSimilar("coddwars"));
    }

    [Test]
    public void TestDictionary3()
    {
        ClosestText kata = new ClosestText(new List<string> { "javascript", "java", "ruby", "php", "python", "coffeescript", "c", "cpp", "brainfuck" }.OrderBy(o => random.Next()));
        Assert.AreEqual("java", kata.FindMostSimilar("heaven"));
        Assert.AreEqual("javascript", kata.FindMostSimilar("javascript"));
    }


    [Test]
    public void TestDictionary4Random()
    {
        ClosestText kata = new ClosestText(new List<string> { "psaysnhfrrqgxwik", "pdyjrkaylryr", "lnjhrzfrosinb", "afirbipbmkamjzw", "loogviwcojxgvi", "iqkyztorburjgiudi", "cwhyyzaorpvtnlfr", "iroezmixmberfr", "jhjyasikwyufr", "tklquxrnhfiggb", "cpnqknjyviusknmte", "hwzsemiqxjwfk", "ntwmwwmicnjvhtt", "emvquxrvvlvwvsi", "sefsknopiffajor", "znystgvifufptxr", "xuwahveztwoor", "hrwuhmtxxvmygb", "karpscdigdvucfr", "xrgdgqfrldwk", "nnsoamjkrzgldi", "ljxzjjorwgb", "cfvruditwcxr", "eglanhfredaykxr", "fxjskybblljqr", "qifwqgdsijibor", "xikoctmrhpvi", "ajacizfrgxfumzpvi", "mhmkakybpczjbb", "vkholxrvjwisrk", "npyrgrpbdfqhhncdi", "pxyousorusjxxbt", "jcocndjkyb", "fxpvfhfrujjaifr", "hkldhadcxrjbmkmcdi", "hirldidcuzbyb", "ggcvrtxrtnafw", "tdvibqccxr", "osbednerciaai", "qojfrlhufr", "kqijoorfkejdcxr", "zqdrhpviqslik", "clxmqmiycvidiyr", "xffrkbdyjveb", "dyhxgviphoptak", "dihhiczkdwiofpr", "riyhpvimgaliuxr", "fgtrjakzlnaebxr", "ppctybxgtleipb", "ucxmdeudiycokfnb" }.OrderBy(o => random.Next()));
        Assert.AreEqual("zqdrhpviqslik", kata.FindMostSimilar("rkacypviuburk"));
    }
}

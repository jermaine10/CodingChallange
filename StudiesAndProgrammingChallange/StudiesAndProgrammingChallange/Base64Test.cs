using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

[TestFixture]
public class Base64Test
{
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    string[] knownValues = new string[] {
        "this is a string!!",
        "this is a test!",
        "now is the time for all good men to come to the aid of their country.",
        "1234567890  ",
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ ",
        "the quick brown fox jumps over the white fence. ",
        "dGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIHRoZSB3aGl0ZSBmZW5jZS4",
        "VFZSSmVrNUVWVEpPZW1jMVRVTkJaeUFna",
        "TVRJek5EVTJOemc1TUNBZyAg",
        "padding, sir?",
        "padding ",
        "",
    };

    [Test]
    public void KnownValuesEncodeTest()
    {
        foreach (var knownValue in knownValues)
        {
            Assert.AreEqual(GetBase64Encoded(knownValue), Base64.ToBase64(knownValue));
        }
    }

    [Test]
    public void RandomValuesEncodeTest()
    {
        for (int i = 0; i < 10; i++)
        {
            var s = GetRandomString(i);
            Assert.AreEqual(GetBase64Encoded(s), Base64.ToBase64(s));
        }
    }

    [Test]
    public void KnownValuesDecodeTest()
    {
        foreach (var knownValue in knownValues)
        {
            var encoded = GetBase64Encoded(knownValue);
            Assert.AreEqual(GetFromBase64Encoded(encoded), Base64.FromBase64(encoded));
        }
    }

    [Test]
    public void RandomValuesDecodeTest()
    {
        for (int i = 0; i < 10; i++)
        {
            var s = GetRandomString(i);
            var encoded = GetBase64Encoded(s);
            Assert.AreEqual(GetFromBase64Encoded(encoded), Base64.FromBase64(encoded));
        }
    }

    private string GetBase64Encoded(string s)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
    }

    private string GetFromBase64Encoded(string s)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(s));
    }

    private string GetRandomString(int i)
    {
        Random random = new Random(i);
        var length = random.Next(1, alphabet.Length - 1);
        return new string(Enumerable.Repeat(alphabet, length).Select(s => s[random.Next(s.Length)]).ToArray());

    }
}

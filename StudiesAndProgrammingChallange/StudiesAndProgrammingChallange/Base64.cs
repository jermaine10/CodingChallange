using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public static class Base64
{
    public static string ToBase64(string s)
    {
        // Happy coding!
        var bytes = Encoding.ASCII.GetBytes(s);
        string stringTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        var bits = string.Empty;
        for (var i = 0; i < bytes.Length; i++)
        {
            bits += Convert.ToString(bytes[i], 2).PadLeft(8, '0');
        }
        const byte threeOct = 8 * 3;
        int octsTaken = 0;
        const byte sixBits = 6;
        var base64 = string.Empty;
        while (octsTaken < bits.Length)
        {
            var currentOctets = bits.Skip(octsTaken).Take(threeOct).ToList();
            var hexTaken = 0;
            while (hexTaken < currentOctets.Count())
            {
                var tempChunk = currentOctets.Skip(hexTaken).Take(sixBits).ToList();
                hexTaken += sixBits;
                var bitString = tempChunk.Aggregate(string.Empty, (current, currentBits) => current + currentBits);
                if (bitString.Length < 6)
                {
                    bitString = bitString.PadRight(6, '0');
                }
                var singleInt = Convert.ToInt32(bitString, 2);

                base64 += stringTable[singleInt];
            }
            octsTaken += threeOct;
        }
        for (var i = 0; i < (bits.Length % 3); i++)
        {
            base64 += "=";
        }
        return base64;
    }

    public static string FromBase64(string s)
    {
        string stringTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        var IndexNo = string.Join("", s.Select(d => stringTable.IndexOf(d)).Where(d => d != -1).ToList().Select(d => Convert.ToString(d, 2).PadLeft(6, '0')).ToList());

        const byte fourOct = 6 * 4;
        int octsTaken = 0;
        const byte eightBit = 8;
        var realString = string.Empty;
        while (octsTaken < IndexNo.Length)
        {
            var currentOctets = IndexNo.Skip(octsTaken).Take(fourOct).ToList();
            var hexTaken = 0;
            while (hexTaken < currentOctets.Count())
            {
                var tempChunk = currentOctets.Skip(hexTaken).Take(eightBit).ToList();
                hexTaken += eightBit;
                var bitString = tempChunk.Aggregate(string.Empty, (current, currentBits) => current + currentBits);
                if (bitString.Length < 8)
                {
                    continue;
                }
                var SingleInt = Convert.ToInt32(bitString, 2).ToString("X");
                realString += Convert.ToChar(Convert.ToUInt32(SingleInt, 16));
            }
            octsTaken += fourOct;

        }
        return realString;
    }
}

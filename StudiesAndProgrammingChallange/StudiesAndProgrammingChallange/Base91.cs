using System.Collections.Generic;
using System.Linq;

class Base91
{
    public static string Decode(string input)
    {

        string stringTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&()*+,./:;<=>?@[]^_`{|}~\"";
        Dictionary<byte, int> stringDecode = new Dictionary<byte, int>();
        for (int i = 0; i < stringTable.Length; i++)
        {
            stringDecode[(byte)stringTable[i]] = i;
        }
        string output = "";
        int c = 0;
        int v = -1;
        int b = 0;
        int n = 0;
        for (int i = 0; i < input.Length; i++)
        {
            c = stringDecode[(byte)input[i]];
            if (c == -1) continue;
            if (v < 0)
            {
                v = c;
            }
            else
            {
                v += c * 91;
                b |= v << n;
                n += (v & 8191) > 88 ? 13 : 14;
                do
                {
                    output += (char)(b & 255);
                    b >>= 8;
                    n -= 8;
                } while (n > 7);
                v = -1;
            }
        }
        if (v + 1 != 0)
        {
            output += (char)((b | v << n) & 255);
        }
        return output;
    }
    public static string Encode(string s)
    {
        string output = "";
        int b = 0;
        int n = 0;
        int v = 0;
        string stringTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&()*+,./:;<=>?@[]^_`{|}~\"'";
        for (int i = 0; i < s.Length; i++)
        {
            b |= (byte)s[i] << n;
            n += 8;
            if (n > 13)
            {
                v = b & 8191;
                if (v > 88)
                {
                    b >>= 13;
                    n -= 13;
                }
                else
                {
                    v = b & 16383;
                    b >>= 14;
                    n -= 14;
                }
                output += (char)stringTable[v % 91];
                output += (char)stringTable[v / 91];
            }
        }
        if (n != 0)
        {
            output += (char)stringTable[b % 91];
            if (n > 7 || b > 90)
                output += (char)stringTable[b / 91];
        }
        return output;
    }
}

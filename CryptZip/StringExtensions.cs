using System;

namespace CryptZip
{
    public static class StringExtensions
    {
        public static byte[] ToBytes(this string s)
        {
            var bytes = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
                bytes[i] = Convert.ToByte(s[i]);
            return bytes;
        }
    }
}
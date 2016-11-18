using CryptZip.Encryption.Padding;
using System;

namespace CryptZip.Encryption
{
    public static class KeyExtender
    {
        public static byte[] Extend(byte[] key, IPadding padding)
        {
            if (key.Length == 16 ||
                key.Length == 24 ||
                key.Length == 32)
                return key;

            int length = 0;
            if (key.Length < 16)
                length = 16;
            else if (key.Length < 24)
                length = 24;
            else if (key.Length < 32)
                length = 32;

            var extended = new byte[length];
            Array.Copy(key, extended, key.Length);
            padding.Add(extended, key.Length - 1);
            return extended;
        }
    }
}
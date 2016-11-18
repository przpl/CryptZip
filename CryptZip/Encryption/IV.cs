using System;

namespace CryptZip.Encryption
{
    public static class IV
    {
        public static byte[] GetRandom(int length)
        {
            var iv = new byte[length];
            var random = new Random(Guid.NewGuid().GetHashCode());
            random.NextBytes(iv);
            return iv;
        }
    }
}
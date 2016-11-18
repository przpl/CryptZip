using System;

namespace CryptZip.Encryption.SerpentAlgorithms
{
    public class SerpentKey
    {
        public Uint128[] K { get; private set; }

        private const uint PHI = 0x9E3779B9;

        public SerpentKey(byte[] key)
        {
            if (key.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(key), "Maximum key length is 256 bits.");

            if (key.Length % 4 != 0)
                throw new ArgumentException("Key length has to be divisible by 4.", nameof(key));

            var words = new uint[132];

            ReadWords(key, words);
            AppendOneToMSB(key, words);
            CalculateWords(words);
            CalculateK(words);
        }

        private void ReadWords(byte[] key, uint[] words)
        {
            for (int i = 0; i < key.Length / 4; i++)
                words[i] = Word32Bits.ToUint(key.SubArray(i * 4, i * 4 + 4));
        }

        private static void AppendOneToMSB(byte[] key, uint[] words)
        {
            if (key.Length < 32)
                words[key.Length / 4] = 1;
        }

        private void CalculateWords(uint[] words)
        {
            for (int i = 8; i < 16; i++)
                words[i] = Word32Bits.RotateLeft((uint)(words[i - 8] ^ words[i - 5] ^ words[i - 3] ^ words[i - 1] ^ PHI ^ (i - 8)), 11);

            Array.Copy(words, 8, words, 0, 8);

            for (int i = 8; i < 132; i++)
                words[i] = Word32Bits.RotateLeft((uint)(words[i - 8] ^ words[i - 5] ^ words[i - 3] ^ words[i - 1] ^ PHI ^ i), 11);
        }

        private void CalculateK(uint[] words)
        {
            K = new Uint128[33];
            var sBox = new SerpentSbox();
            int[] sBoxIndexes = { 3, 2, 1, 0, 7, 6, 5, 4 };

            for (int i = 0; i < 33; i++)
                K[i] = new Uint128(sBox.Substitute(words[4 * i], words[4 * i + 1], words[4 * i + 2], words[4 * i + 3], sBoxIndexes[i % 8]));
        }
    }

    public class Uint128
    {
        public uint[] Words { get; }
        public uint W0 => Words[0];
        public uint W1 => Words[1];
        public uint W2 => Words[2];
        public uint W3 => Words[3];

        public Uint128(uint[] words)
        {
            Words = words;
        }
    }
}
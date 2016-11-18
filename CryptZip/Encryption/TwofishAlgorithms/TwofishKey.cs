using System;

namespace CryptZip.Encryption.TwofishAlgorithms
{
    public class TwofishKey
    {
        public uint[] K { get; }
        public uint[] SBox { get; }

        private readonly byte[] _key;

        public TwofishKey(byte[] key)
        {
            if (key.Length > 32)
                throw new ArgumentOutOfRangeException("_key", "Maximum key length is 256 bits.");

            _key = AddPadding(key);

            uint[] M = SplitInto32BitWords(_key);
            uint[] Me = GetEvenWords(M);
            uint[] Mo = GetOddWords(M);
            K = GetK(Me, Mo);
            SBox = GetSBoxes(Me, Mo);
        }

        private byte[] AddPadding(byte[] key)
        {
            if (key.Length == 16 ||
                key.Length == 24 ||
                key.Length == 32)
                return key;

            if (key.Length < 16)
                return AddPadding(key, 16);
            if (key.Length < 24)
                return AddPadding(key, 24);
            if (key.Length < 32)
                return AddPadding(key, 32);

            throw new ArgumentOutOfRangeException("key", "Invalid key length.");
        }

        private byte[] AddPadding(byte[] key, int zeroesCount)
        {
            var newKey = new byte[zeroesCount];
            Array.Copy(key, newKey, key.Length);

            for (int i = key.Length; i < newKey.Length; i++)
                newKey[i] = 0;

            return newKey;
        }

        private uint[] SplitInto32BitWords(byte[] m)
        {
            int BYTES_PER_WORD = 4;
            var M = new uint[m.Length / BYTES_PER_WORD];

            for (int i = 0; i < M.Length; i++)
                M[i] = Word32Bits.ToUint(m.SubArray(i * 4, i * 4 + 4));

            return M;
        }

        private uint[] GetEvenWords(uint[] M)
        {
            return GetWords(M, 0);
        }

        private uint[] GetOddWords(uint[] M)
        {
            return GetWords(M, 1);
        }

        private uint[] GetWords(uint[] words, int offset)
        {
            var result = new uint[words.Length / 2];

            for (int i = 0; i < result.Length; i++)
                result[i] = words[2*i + offset];

            return result;
        }

        private uint[] GetK(uint[] Me, uint[] Mo)
        {
            var k = new uint[40];
            uint p = 16843009;

            var mds = new TwofishMDS();

            for (int i = 0; i < 20; i++)
            {
                uint A = TwofishFunction.h(mds, (uint)(2*i*p), Me);
                uint B = Word32Bits.RotateLeft(TwofishFunction.h(mds, (uint)((2*i + 1)*p), Mo), 8);
                k[2*i] = A + B;
                k[2*i + 1] = Word32Bits.RotateLeft(A + 2*B, 9);
            }

            return k;
        }

        private uint[] GetSBoxes(uint[] Me, uint[] Mo)
        {
            int k = _key.Length / 8;
            var sBox = new uint[k];
            for (int i = 0; i < k; i++)
                sBox[k - 1 - i] = GetSBox(Me[i], Mo[i]);

            return sBox;
        }

       private uint GetSBox(uint m0, uint m1)
       {
            for (int i = 0; i < 4; i++)
                m1 = RS_rem(m1);
            m1 ^= m0;
            for (int i = 0; i < 4; i++)
                m1 = RS_rem(m1);

            return m1;
        }

        private uint RS_rem(uint x)
        {
            uint b = x >> 24;
            uint g2 = (uint)((b << 1) ^ ((b & 0x80) != 0 ? 0x14D : 0));
            uint g3 = (uint)((b >> 1) ^ ((b & 0x01) != 0 ? 0x14D >> 1 : 0) ^ g2);
            x = (x << 8) ^ (g3 << 24) ^ (g2 << 16) ^ (g3 << 8) ^ b;
            return x;
        }
    }
}

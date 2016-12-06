using CryptZip.Encryption.TwofishAlgorithms;

namespace CryptZip.Encryption
{
    public class Twofish : Cipher
    {
        private readonly ITwofishKey _key;
        private readonly ITwofishMDS _mds;

        private ByteWriter _byteWriter;

        public Twofish(byte[] key) : base(key)
        {
            _key = new TwofishKey(key);
            _mds = new TwofishMDS();
        }

        public Twofish(ITwofishKey key, ITwofishMDS mds) : base(key.RawBytes)
        {
            _key = key;
            _mds = mds;
        }

        public override byte[] Encrypt(byte[] block)
        {
            base.Encrypt(block);

            var byteReader = new ByteReader(block, BITS_PER_WORD, BlockSize);
            _byteWriter = new ByteWriter(16);

            uint[] K = byteReader.ReadBlock();
            K = InputWhitening(K);
            K = Rounds(_mds, K);
            K = OutputWhitening(K);
            WriteBlock(K);

            return _byteWriter.Bytes;
        }

        private uint[] InputWhitening(uint[] K)
        {
            for (int i = 0; i < 4; i++)
                K[i] ^= _key.K[i];
            return K;
        }

        private uint[] Rounds(ITwofishMDS mds, uint[] K)
        {
            for (int round = 0; round < 16; round++)
            {
                K = Round(mds, K, round);
                K = SwapWords(K);
            }
            return K;
        }

        private uint[] Round(ITwofishMDS mds, uint[] K, int round)
        {
            uint F0 = TwofishFunction.h(mds, K[0], _key.SBox);
            uint F1 = TwofishFunction.h(mds, Word32Bits.RotateLeft(K[1], 8), _key.SBox);
            K[2] ^= F0 + F1 + _key.K[2*round + 8];
            K[2] = Word32Bits.RotateRight(K[2], 1);
            K[3] = Word32Bits.RotateLeft(K[3], 1) ^ (F0 + 2 * F1 + _key.K[2*round + 9]);
            return K;
        }

        private uint[] SwapWords(uint[] K)
        {
            uint first = K[0];
            K[0] = K[2];
            K[2] = first;
            first = K[1];
            K[1] = K[3];
            K[3] = first;
            return K;
        }

        private uint[] OutputWhitening(uint[] K)
        {
            K[2] ^= _key.K[4];
            K[3] ^= _key.K[5];
            K[0] ^= _key.K[6];
            K[1] ^= _key.K[7];
            return K;
        }

        private void WriteBlock(uint[] K)
        {
            _byteWriter.WriteBytes(Word32Bits.ToBytes(K[2]));
            _byteWriter.WriteBytes(Word32Bits.ToBytes(K[3]));
            _byteWriter.WriteBytes(Word32Bits.ToBytes(K[0]));
            _byteWriter.WriteBytes(Word32Bits.ToBytes(K[1]));
        }

        public override byte[] Decrypt(byte[] block)
        {
            base.Decrypt(block);

            var byteReader = new ByteReader(block, BITS_PER_WORD, BlockSize);
            _byteWriter = new ByteWriter(16);

            uint[] K = byteReader.ReadBlock();
            K = SwapWords(K);
            K = OutputWhitening(K);
            K = ReverseRounds(_mds, K);
            K = InputWhitening(K);
            K = SwapWords(K);
            WriteBlock(K);

            return _byteWriter.Bytes;
        }

        private uint[] ReverseRounds(ITwofishMDS mds, uint[] K)
        {
            for (int round = 0; round < 16; round++)
            {
                K = ReverseRound(mds, K, round);
                K = SwapWords(K);
            }
            return K;
        }

        private uint[] ReverseRound(ITwofishMDS mds, uint[] K, int round)
        {
            uint F0 = TwofishFunction.h(mds, K[2], _key.SBox);
            uint F1 = TwofishFunction.h(mds, Word32Bits.RotateLeft(K[3], 8), _key.SBox);
            K[1] ^= F0 + 2*F1 + _key.K[39 - 2*round];
            K[1] = Word32Bits.RotateRight(K[1], 1);
            K[0] = Word32Bits.RotateLeft(K[0], 1) ^ (F0 + F1 + _key.K[38 - 2*round]);
            return K;
        }
    }
}

using CryptZip.Encryption.SerpentAlgorithms;

namespace CryptZip.Encryption
{
    public class Serpent : Cipher
    {
        private readonly ISerpentKey _key;
        private readonly ISerpentSbox _sBox;

        public Serpent(byte[] key) : base(key)
        {
            _key = new SerpentKey(key);
            _sBox = new SerpentSbox();
        }

        public Serpent(ISerpentKey key, ISerpentSbox sBox) : base(key.RawBytes)
        {
            _key = key;
            _sBox = sBox;
        }

        public override byte[] Encrypt(byte[] block)
        {
            base.Encrypt(block);

            var byteReader = new ByteReader(block, BITS_PER_WORD, BlockSize); // poprawić, na co to a komu to potrzebne

            uint[] words = byteReader.ReadBlock();
            words = Rounds(words);
            words = LastRound(words);

            var byteWriter = new ByteWriter(16);

            byteWriter.WriteWords(words);

            return byteWriter.Bytes;
        }

        private uint[] Rounds(uint[] words)
        {
            for (int i = 0; i < 31; i++)
            {
                words = words.XOR(_key.K[i].Words);
                words = _sBox.Substitute(words[0], words[1], words[2], words[3], i%8);
                words = LinearTransformator.Transform(words);
            }

            return words;
        }

        private uint[] LastRound(uint[] words)
        {
            words = words.XOR(_key.K[31].Words);
            words = _sBox.Substitute(words[0], words[1], words[2], words[3], 7);
            words = words.XOR(_key.K[32].Words);
            return words;
        }

        public override byte[] Decrypt(byte[] block)
        {
            base.Decrypt(block);

            var byteReader = new ByteReader(block, BITS_PER_WORD, BlockSize); // poprawić
            uint[] words = byteReader.ReadBlock();
            words = InverseLastRound(words);
            words = InverseRounds(words);

            var byteWriter = new ByteWriter(16);
            byteWriter.WriteWords(words);

            return byteWriter.Bytes;
        }

        private uint[] InverseRounds(uint[] words)
        {
            int sBoxIndex = 6;

            for (int i = 31; i >= 1; i--)
            {
                words = LinearTransformator.Inverse(words.XOR(_key.K[i].Words));
                words = _sBox.Inverse(words[0], words[1], words[2], words[3], sBoxIndex--);
                if (sBoxIndex < 0)
                    sBoxIndex = 7;
            }

            words = words.XOR(_key.K[0].Words);
            return words;
        }

        private uint[] InverseLastRound(uint[] words)
        {
            words = words.XOR(_key.K[32].Words);
            words = _sBox.Inverse(words[0], words[1], words[2], words[3], 7);
            return words;
        }
    }
}


namespace CryptZip.Encryption
{
    public class ByteReader
    {
        private readonly byte[] _data;
        private int _dataIndex;
        private readonly int _bytesPerWord, _wordsPerBlock;

        public ByteReader(byte[] data, int bitsPerWord, int blockSize)
        {
            _data = data;
            _bytesPerWord = bitsPerWord / 8;
            _wordsPerBlock = blockSize / _bytesPerWord;
        }

        public bool HasEnded()
        {
            return _dataIndex >= _data.Length;
        }

        public uint[] ReadBlock()
        {
            var words = new uint[_wordsPerBlock];
            for (int i = 0; i < _wordsPerBlock; i++)
                words[i] = ReadWord();
            return words;
        }

        private uint ReadWord()
        {
            var bytes = new byte[_bytesPerWord];
            for (int i = 0; i < _bytesPerWord; i++)
                bytes[i] = ReadByte();
            return Word32Bits.ToUint(bytes);
        }

        public byte ReadByte()
        {
            return _data[_dataIndex++];
        }
    }
}

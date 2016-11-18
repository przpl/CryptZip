using System.Collections.Generic;
using System.IO;

namespace CryptZip.Compression
{
    /// <summary>
    /// Writes single bits into stream.
    /// </summary>
    public class BitWriter
    {
        private int _bitsCount;
        private readonly Stream _stream;
        private byte _current;

        public BitWriter(Stream stream)
        {
            _stream = stream;
        }

        public void Write(IEnumerable<bool> bits)
        {
            foreach (var bit in bits)
                Write(bit);
        }

        public void Write(bool bit)
        {
            _current *= 2;
            if (bit)
                _current++;
            _bitsCount++;

            if (_bitsCount == 8)
            {
                _stream.WriteByte(_current);
                _current = 0;
                _bitsCount = 0;
            }
        }

        public void Flush()
        {
            if (_bitsCount == 0)
                return;

            while (_bitsCount < 8)
            {
                _current *= 2;
                _bitsCount++;
            }

            _stream.WriteByte(_current);
        }
    }
}

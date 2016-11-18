using System;
using System.IO;

namespace CryptZip.Compression
{
    /// <summary>
    /// Reads single bits from stream.
    /// </summary>
    public class BitReader
    {
        public long BytesLeft => _stream.Length - _stream.Position;

        private readonly Stream _stream;
        private byte _currentByte, _power;

        public BitReader(Stream stream)
        {
            _stream = stream;
        }

        public bool Read()
        {
            if (ByteAlreadyRead())
                ReadNextByte();

            bool bitValue = false;

            if (_currentByte >= _power)
            {
                bitValue = true;
                _currentByte -= _power;
            }

            _power /= 2;

            return bitValue;
        }

        public bool[] Read(int count)
        {
            var bits = new bool[count];

            for (int i = 0; i < count; i++)
                bits[i] = Read();

            return bits;
        }

        private bool ByteAlreadyRead()
        {
            return _power == 0;
        }

        private void ReadNextByte()
        {
            if (_stream.Position == _stream.Length)
                throw new IndexOutOfRangeException("No more bytes to read.");
            
            _currentByte = Convert.ToByte(_stream.ReadByte());
            _power = 128;
        }
    }
}

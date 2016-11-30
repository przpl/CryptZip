using System;
using System.IO;

namespace CryptZip.Compression
{
    public class LZ77 : ICompressor
    {
        private SlidingWindow _window;
        private BitWriter _bitWriter;
        private byte _bitsPerOffset, _bitsPerLength;
        private readonly int _searchBufferLength, _lookAheadLength;
        private Stream _input, _output;

        public LZ77()
        {
            _searchBufferLength = 32768;
            _lookAheadLength = 256;
        }

        public LZ77(int searchBufferLength, int lookAheadLength)
        {
            _searchBufferLength = searchBufferLength;
            _lookAheadLength = lookAheadLength;
        }

        public void Compress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            WriteHeader();
            WriteData();
        }

        private void WriteHeader()
        {
            CalculateTokenLength();
            _output.WriteByte(_bitsPerOffset);
            _output.WriteByte(_bitsPerLength);
        }

        private void CalculateTokenLength()
        {
            _bitsPerOffset = BitConverter.MinimalNumberOfBits(_searchBufferLength);
            _bitsPerLength = BitConverter.MinimalNumberOfBits(_lookAheadLength);
        }

        private void WriteData()
        {
            _window = new SlidingWindow(_input, _searchBufferLength, _lookAheadLength);
            _bitWriter = new BitWriter(_output);

            while (!_window.LookAheadEmpty)
                WriteToken(_window.NextToken());

            _bitWriter.Flush();
        }

        private void WriteToken(Token token)
        {
            _bitWriter.Write(BitConverter.ToBits(token.Offset, _bitsPerOffset));
            _bitWriter.Write(BitConverter.ToBits(token.Length, _bitsPerLength));
            _bitWriter.Write(BitConverter.ToBits(token.Byte, 8));
        }

        public void Decompress(Stream input, Stream output)
        {
            _input = input;
            _output = output;

            ReadHeader();
            ReadData();
        }

        private void ReadHeader()
        {
            _bitsPerOffset = Convert.ToByte(_input.ReadByte());
            _bitsPerLength = Convert.ToByte(_input.ReadByte());
        }

        private void ReadData()
        {
            var buffer = new SlidingBuffer(_searchBufferLength);
            var bitReader = new BitReader(_input);

            int bytesPerToken = GetBytesPerToken();

            while (bitReader.BytesLeft >= bytesPerToken - 1)
            {
                int offset = BitConverter.ToInt(bitReader.Read(_bitsPerOffset));
                int length = BitConverter.ToInt(bitReader.Read(_bitsPerLength));
                byte value = BitConverter.ToByte(bitReader.Read(8));

                for (int i = 0; i < length; i++)
                {
                    byte b = buffer.Bytes[buffer.Bytes.Length - offset];
                    buffer.Add(b);
                    _output.WriteByte(b);
                }
                buffer.Add(value);
                _output.WriteByte(value);
            }
        }

        private int GetBytesPerToken()
        {
            int bitsPerToken = _bitsPerOffset + _bitsPerLength + 8;

            if (bitsPerToken%8 == 0)
                return bitsPerToken/8;

            return bitsPerToken/8 + 1;
        }
    }
}

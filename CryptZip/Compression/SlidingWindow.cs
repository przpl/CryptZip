using System;
using System.IO;

namespace CryptZip.Compression
{
    public class SlidingWindow
    {
        public byte[] Bytes { get; }

        public bool LookAheadEmpty => _end <= _border;

        private int _start, _end;
        private readonly int _border;
        private readonly Stream _stream;
        private long _bytesRead;

        public SlidingWindow(Stream stream, int searchBufferLength, int lookAheadLength)
        {
            _stream = stream;
            Bytes = new byte[searchBufferLength + lookAheadLength];
            _border = searchBufferLength;
            _end = _border;
            _start = searchBufferLength;
            FillLookAheadBuffer();
        }

        private void FillLookAheadBuffer()
        {
            for (int i = _border; i < Bytes.Length; i++)
            {
                Bytes[i] = Convert.ToByte(_stream.ReadByte());
                _bytesRead++;
                _end++;
                if (_bytesRead == _stream.Length)
                    return;
            }
        }

        public void Slide(int count)
        {
            for (int i = 0; i < count; i++)
                Slide();
        }

        public void Slide()
        {
            if (_start > 0)
                _start--;

            if (_bytesRead > _stream.Length || (_end - _border) == 0)
                return;

            for (int i = _start; i < _end - 1; i++)
                Bytes[i] = Bytes[i + 1];

            if (_bytesRead < _stream.Length)
            {
                Bytes[_end - 1] = Convert.ToByte(_stream.ReadByte());
                _bytesRead++;
            }
            else
                _end--;
        }

        public Token NextToken()
        {
            if (IsLastByte())
                return new Token {Byte = Bytes[_border]};

            byte currentByte = Bytes[_border];

            int index = LastIndex(currentByte, _border);

            int lastOffset = 0, lastLength = 0;

            if (index != -1)
            {
                lastOffset = _border - index;
                lastLength = MatchLength(index);
            }

            int lastIndex = index;

            while (index != -1)
            {
                index = LastIndex(currentByte, index);

                if (index != -1)
                {
                    int currentMatchLength = MatchLength(index);
                    if (currentMatchLength >= lastLength)
                    {
                        lastOffset = _border - index;
                        lastLength = currentMatchLength;
                        lastIndex = index;
                    }
                }
            }

            Slide(lastLength + 1);

            return lastIndex == -1 ? new Token { Empty = true, Byte = Bytes[_border - 1]} 
                                   : new Token { Offset = lastOffset, Length = lastLength, Byte = Bytes[_border - 1]};
        }

        private bool IsLastByte()
        {
            return _border == _stream.Length - 1;
        }

        private int LastIndex(byte searchItem, int limitExclusive)
        {
            for (int i = limitExclusive - 1; i >= _start; i--)
                if (Bytes[i] == searchItem)
                    return i;
            return -1;
        }

        private int MatchLength(int startIndex)
        {
            int length = 1;
            int index = startIndex + 1;

            while (IsPtrInsideLookAhead(length) && Bytes[index] == Bytes[_border + length])
            {
                length++;
                index++;

                if (index == _border)
                    index = startIndex;
            }

            return length;

        }

        private bool IsPtrInsideLookAhead(int offset)
        {
            return _border + offset < _end - 1;
        }
    }
}
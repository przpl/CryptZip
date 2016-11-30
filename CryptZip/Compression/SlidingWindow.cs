using System;
using System.IO;

namespace CryptZip.Compression
{
    public class SlidingWindow
    {
        public byte[] Bytes { get; }

        public bool LookAheadEmpty => _end <= _border;

        private int _start, _border, _end; // example: length of Search = 3 and Look = 4, then _start = 0, _border = 3, _end = 7 (_end is out of range)
        private readonly Stream _stream;

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
            for (int i = _border; i < Bytes.Length && _stream.Position < _stream.Length; i++)
            {
                Bytes[i] = Convert.ToByte(_stream.ReadByte());
                _end++;
            }
        }

        public void Slide(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (_start > 0)
                    _start--;

                if (_stream.Position > _stream.Length || _end == _border)
                    return;

                for (int j = _start; j < _end - 1; j++)
                    Bytes[j] = Bytes[j + 1];

                if (_stream.Position < _stream.Length)
                    Bytes[_end - 1] = Convert.ToByte(_stream.ReadByte());
                else
                    _end--;
            }
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

                if (index == -1)
                    break;

                int currentMatchLength = MatchLength(index);

                if (currentMatchLength < lastLength)
                    continue;

                lastOffset = _border - index;
                lastLength = currentMatchLength;
                lastIndex = index;
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
            int offset = 1;

            while (IsPtrInsideLookAhead(offset) && Bytes[startIndex + offset] == Bytes[_border + offset])
                offset++;

            return offset;
        }

        private bool IsPtrInsideLookAhead(int offset)
        {
            return _border + offset < _end - 1; // _end - 1 because we can't match last element of look-ahead buffer, it is needed for token
        }
    }
}
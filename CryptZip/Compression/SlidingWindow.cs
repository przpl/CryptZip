using System;
using System.Diagnostics;
using System.IO;

namespace CryptZip.Compression
{
    public class SlidingWindow
    {
        public byte[] Bytes { get; }

        public bool LookAheadEmpty => _border == _end;

        private int _start, _border, _end; // example: length of Search = 3 and Look = 4, then _start = 0, _border = 3, _end = 7 (_end is out of range)
        private int _searchBufferLength;
        private readonly Stream _stream;

        public SlidingWindow(Stream stream, int searchBufferLength, int lookAheadLength)
        {
            _stream = stream;
            Bytes = new byte[searchBufferLength + lookAheadLength];
            FillLookAheadBuffer(lookAheadLength);
            _searchBufferLength = searchBufferLength;
        }

        private void FillLookAheadBuffer(int lookAheadLength)
        {
            for (int i = 0; i < lookAheadLength && _stream.Position < _stream.Length; i++)
            {
                Bytes[i] = Convert.ToByte(_stream.ReadByte());
                _end++;
            }
        }

        public void Slide(int count) // usunąć
        {
            for (int i = 0; i < count; i++)
                Slide();
        }

        private void Slide()
        {
            SlideStart();
            SlideBorder();
            if (_stream.Position < _stream.Length)
                SlideEnd();
        }

        private void SlideStart()
        {
            if (_searchBufferLength != -1)
                _searchBufferLength--;

            if (_searchBufferLength == -1)
            {
                _start++;
                _start %= Bytes.Length;
            }
        }

        private void SlideBorder()
        {
            _border++;
            _border %= Bytes.Length;
        }

        private void SlideEnd()
        {
            Bytes[_end++] = Convert.ToByte(_stream.ReadByte()); // test prędkości rzutowania
            _end %= Bytes.Length;
        }

        public Token NextToken()
        {
            if (IsLastByte())
            {
                Token token = new Token { Byte = Bytes[_border] };
                SlideBorder();
                return token;
            }

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

            int border = _border;

            if (border == 0)
                border = Bytes.Length;

            if (lastOffset == 0 && lastLength == 1)
                Debug.WriteLine("");

            return lastIndex == -1 ? new Token { Empty = true, Byte = Bytes[border - 1]}
                                   : new Token { Offset = lastOffset, Length = lastLength, Byte = Bytes[border - 1]};
        }

        private bool IsLastByte()
        {
            if (_border == _end - 1)
                return true;
            if (_border < _end)
                return false;

            return _border == _end + Bytes.Length - 1;
        }

        private int LastIndex(byte searchItem, int limitExclusive)
        {
            if (limitExclusive < _start)
                limitExclusive += Bytes.Length;

            for (int i = limitExclusive - 1; i >= _start; i--)
                if (Bytes[i % Bytes.Length] == searchItem)
                    return i;

            return -1;
        }

        private int MatchLength(int startIndex)
        {
            int offset = 1;

            while (IsPtrInsideLookAhead(offset) && Bytes[(startIndex + offset) % Bytes.Length] == Bytes[(_border + offset) % Bytes.Length])
                offset++;

            return offset;
        }

        private bool IsPtrInsideLookAhead(int offset)
        {
            int end = _end;

            if (_border > _end)
                end += Bytes.Length;

            // przełącznik ? :

            return _border + offset < end - 1; // end - 1 because we can't match last element of look-ahead buffer, it is needed for token
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;

namespace CryptZip.Compression
{
    public class SlidingWindow
    {
        public int[] Bytes { get; }

        public bool LookAheadEmpty => _border == _end;

        private int _start, _border, _end; // example: length of Search = 3 and Look = 4, then _start = 0, _border = 3, _end = 7 (_end is out of range)
        private int _searchBufferLength;
        private readonly Stream _stream;
        private readonly Token[] tokens;
        private readonly int _threadsCount;
        
        public SlidingWindow(Stream stream, int searchBufferLength, int lookAheadLength, int threadsCount)
        {
            _stream = stream;
            Bytes = new int[searchBufferLength + lookAheadLength];
            FillLookAheadBuffer(lookAheadLength);
            _searchBufferLength = searchBufferLength;
            tokens = new Token[threadsCount];
            _threadsCount = threadsCount;
        }

        private void FillLookAheadBuffer(int lookAheadLength)
        {
            for (int i = 0; i < lookAheadLength && _stream.Position < _stream.Length; i++)
            {
                Bytes[i] = _stream.ReadByte(); //Convert.ToByte(_stream.ReadByte());
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
            Bytes[_end++] = _stream.ReadByte(); //Convert.ToByte(_stream.ReadByte()); // test prędkości rzutowania
            _end %= Bytes.Length;
        }

        public Token NextToken()
        {
            if (IsLastByte())
            {
                var token = Token.Empty(Bytes[_border]);
                SlideBorder();
                return token;
            }

            ExecuteThreads();
            Token max = FindLargest();
            Slide(max.Length + 1);

            return max;
        }

        private int[] GetSliceIndexes()
        {
            int searchBufferLength = GetSearchBufferLength();

            int sliceSize = (int)Math.Ceiling(searchBufferLength / (float)_threadsCount);

            int[] sliceSizes = new int[_threadsCount];

            for (int i = 0; i < sliceSizes.Length; i++)
                sliceSizes[i] = sliceSize;

            int totalLength = sliceSize * _threadsCount;
            int excess = totalLength - searchBufferLength;

            int j = 0;
            while (excess > 0)
            {
                sliceSizes[j++]--;
                j %= sliceSizes.Length;
                excess--;
            }

            int[] sliceIndexes = new int[_threadsCount];
            int index = 0;

            for (int i = 0; i < sliceSizes.Length; i++)
            {
                sliceIndexes[i] = _start + index;
                index += sliceSizes[i];
            }

            return sliceIndexes;
        }

        private void ExecuteThreads()
        {
            int[] sliceIndexes = GetSliceIndexes();

            var tasks = new Task[_threadsCount];

            for (int i = 0; i < _threadsCount; i++)
            {
                int border = i != sliceIndexes.Length - 1 ? sliceIndexes[i + 1] : _border;
                tasks[i] = FindToken(i, sliceIndexes[i], border);
            }

            foreach (var t in tasks)
                t.Wait();
        }

        private Token FindLargest()
        {
            Token max = tokens[0];

            for (int i = 1; i < tokens.Length; i++)
                if (tokens[i].Length > max.Length)
                    max = tokens[i];

            return max;
        }

        private int GetSearchBufferLength()
        {
            int border = _border;
            if (border < _start)
                border += Bytes.Length;
            return border - _start;
        }

        public Task FindToken(int threadIndex, int start, int border)
        {
            int firstInLookAhead = Bytes[_border];

            return Task.Run(() =>
            {
                int index = FindLast(firstInLookAhead, start, border);

                if (index == -1)
                {
                    tokens[threadIndex] = Token.Empty(Bytes[_border % Bytes.Length]);
                    return;
                }
                
                int lastLength = MatchLength(index);
                int lastIndex = index;

                do
                {
                    index = FindLast(firstInLookAhead, start, index);

                    if (index == -1)
                        break;

                    int currentMatchLength = MatchLength(index);

                    if (currentMatchLength < lastLength)
                        continue;

                    lastLength = currentMatchLength;
                    lastIndex = index;
                } while (index != -1);

                int lastOffset = _border - lastIndex;
                if (_border < lastIndex)
                    lastOffset += Bytes.Length;

                int tokenByte = Bytes[(_border + lastLength) % Bytes.Length];

                tokens[threadIndex] = new Token { Offset = lastOffset, Length = lastLength, Byte = tokenByte };
            });
        }

        private bool IsLastByte()
        {
            if (_border == _end - 1)
                return true;
            if (_border < _end)
                return false;

            return _border == _end + Bytes.Length - 1;
        }

        private int FindLast(int searchItem, int start, int end)
        {
            if (end < start)
                end += Bytes.Length;

            for (int i = end - 1; i >= start; i--)
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

            return _border + offset < end - 1; // end - 1 because we can't match last element of look-ahead buffer, it is needed for token
        }
    }
}
using System;
using System.IO;
using System.Threading.Tasks;

namespace CryptZip.Compression
{
    public class SlidingWindow // test wydajności z Bytes.Length vs jakaś zmienna
    {
        public byte[] Bytes { get; }

        public bool LookAheadEmpty => _border == _end;

        private int _start, _border, _end; // example: length of Search = 3 and Look = 4, then _start = 0, _border = 3, _end = 7 (_end is out of range)
        private int _searchBufferLength;
        private readonly Stream _stream;
        private Token[] tokens;
        private int _threadsCount;
        private Task[] tasks;

        public SlidingWindow(Stream stream, int searchBufferLength, int lookAheadLength, int threadsCount = 8)
        {
            _stream = stream;
            Bytes = new byte[searchBufferLength + lookAheadLength];
            FillLookAheadBuffer(lookAheadLength);
            _searchBufferLength = searchBufferLength;
            tokens = new Token[threadsCount];
            tasks = new Task[threadsCount];
            _threadsCount = threadsCount;
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
                var token = new Token { Byte = Bytes[_border] };
                SlideBorder();
                return token;
            }

            int searchBufferLength = GetSearchBufferLength();

            if (searchBufferLength < _threadsCount) // only one thread, not needed ???
            {
                var task = FindToken(0, _start, _border); // przeładowana wersja?
                task.Wait();
                Slide(tokens[0].Length + 1);
                return tokens[0];
            }

            float temp = searchBufferLength/(float)_threadsCount;

            int bytesPerThread = (int)Math.Ceiling(temp);

            int[] bytes = new int[_threadsCount]; // liczba bajtów na wątek do przetworzenia

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = bytesPerThread;

            int actualValue = bytesPerThread* _threadsCount;
            int over = actualValue - searchBufferLength;

            int j = 0;
            while (over > 0)
            {
                bytes[j++]--;
                j %= bytes.Length;
                over--;
            }
            
            int[] starts = new int[_threadsCount];
            int index = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                starts[i] = _start + index;
                index += bytes[i];
            }

            for (int i = 0; i < _threadsCount; i++)
            {
                int border = i != starts.Length - 1 ? starts[i + 1] : _border;
                tasks[i] = FindToken(i, starts[i], border);
            }

            for (int i = 0; i < tasks.Length; i++)
                tasks[i].Wait();

            Token max = tokens[0];

            for (int i = 1; i < tokens.Length; i++)
            {
                if (tokens[i].Length > max.Length)
                    max = tokens[i];
                else if (tokens[i].Length == max.Length && tokens[i].Offset > max.Offset) // wątek pierwszy powinien mieć najbardziej odległy
                    max = tokens[i];
            }

            Slide(max.Length + 1);

            return max;
        }

        private int GetSearchBufferLength()
        {
            int border = _border;
            if (border < _start)
                border += Bytes.Length;
            return border - _start;
        }

        public Task FindToken(int threadIndex, int start, int border) // działa ok z jednym wątkiem
        {
            byte firstInLookAhead = Bytes[_border]; // do zmiennej?

            return Task.Run(() =>
            {
                int index = FindLast(firstInLookAhead, start, border);
                int lastLength = 0;

                if (index != -1)
                    lastLength = MatchLength(index);

                int lastIndex = index;

                while (index != -1) // do while
                {
                    index = FindLast(firstInLookAhead, start, index);

                    if (index == -1)
                        break;

                    int currentMatchLength = MatchLength(index);

                    if (currentMatchLength < lastLength)
                        continue;

                    lastLength = currentMatchLength;
                    lastIndex = index;
                }

                int lastOffset = _border - lastIndex;
                if (_border < lastIndex)
                    lastOffset += Bytes.Length;

                var token = lastIndex == -1 ? new Token { Empty = true, Byte = Bytes[(_border + lastLength) % Bytes.Length] } // Token.Empty - null object pattern, zamiast empty można offset 0 użyć?
                                       : new Token { Offset = lastOffset, Length = lastLength, Byte = Bytes[(_border + lastLength) % Bytes.Length] };

                tokens[threadIndex] = token;
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

        private int FindLast(byte searchItem, int start, int end)
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

            // przełącznik ? :

            return _border + offset < end - 1; // end - 1 because we can't match last element of look-ahead buffer, it is needed for token
        }
    }
}
namespace CryptZip.Compression
{
    public class SlidingBuffer
    {
        private readonly byte[] _bytes;
        private int _end;

        public SlidingBuffer(int size)
        {
            _bytes = new byte[size];
        }

        public void Add(byte b)
        {
            _bytes[_end++] = b;
            _end %= _bytes.Length;
        }

        public byte this[int offset]
        {
            get
            {
                int index = _end - offset;
                if (index < 0)
                    index += _bytes.Length;
                return _bytes[index];
            }
        }
    }
}
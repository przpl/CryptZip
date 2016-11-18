
namespace CryptZip.Compression
{
    public class SlidingBuffer
    {
        public byte[] Bytes { get; set; }

        private int _index;

        public SlidingBuffer(int size)
        {
            Bytes = new byte[size];
            _index = Bytes.Length;
        }

        public void Add(byte b)
        {
            if (_index > 0)
                _index--;

            for (int i = _index; i < Bytes.Length - 1; i++)
                Bytes[i] = Bytes[i + 1];

            Bytes[Bytes.Length - 1] = b;
        }
    }
}

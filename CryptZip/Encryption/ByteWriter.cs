
namespace CryptZip.Encryption
{
    public class ByteWriter
    {
        public byte[] Bytes { get; }

        private int _writeIndex;
        
        public ByteWriter(int dataLength)
        {
            Bytes = new byte[dataLength];
        }

        public void WriteWords(uint[] words)
        {
            foreach (var word in words)
                WriteBytes(Word32Bits.ToBytes(word));
        }

        public void WriteBytes(byte[] bytes)
        {
            foreach (var b in bytes)
                WriteByte(b);
        }

        public void WriteByte(byte value)
        {
            Bytes[_writeIndex++] = value;
        }
    }
}

using System;

namespace CryptZip.Encryption.Padding
{
    public class PKCS7Padding : IPadding
    {
        public void Add(byte[] bytes)
        {
            Add(bytes, -1);
        }

        public void Add(byte[] bytes, int lastByteIndex)
        {
            int difference = bytes.Length - lastByteIndex;
            if (difference == 0)
                return;

            byte paddingByte = Convert.ToByte(difference - 1);
            for (int i = lastByteIndex + 1; i < bytes.Length; i++)
                bytes[i] = paddingByte;
        }

        public byte[] Remove(byte[] block)
        {
            byte paddingByte = block[block.Length - 1];
            var removed = new byte[block.Length - paddingByte];
            if (removed.Length == 0)
                return removed;
            Array.Copy(block, removed, removed.Length);
            return removed;
        }
    }
}
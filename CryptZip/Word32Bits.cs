using System;
using System.Linq;

namespace CryptZip
{
    public static class Word32Bits
    {
        public static uint ToUint(byte[] bytes, ValueRepresentation representation)
        {
            if (representation == ValueRepresentation.BigEndian)
                return ToUint(bytes);

            return ToUint(bytes.Reverse().ToArray());
        }

        public static uint ToUint(byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new ArgumentException("Word can be constructed from four bytes only.");

            uint word = 0;

            for (int i = 0; i < 4; i++)
                word += bytes[i] * PowerCalculator.Two(8 * i);

            return word;
        }

        public static byte GetByte(uint word, int byteIndex)
        {
            return (byte)(word / PowerCalculator.Two(8*byteIndex) % 256);
        }

        public static uint[] ToUintBytes(uint word, ValueRepresentation representation)
        {
            if (representation == ValueRepresentation.BigEndian)
                return ToUintBytes(word);

            return ToUintBytes(word).Reverse().ToArray();
        }

        public static uint[] ToUintBytes(uint word)
        {
            var bytes = new uint[4];

            for (int i = 0; i < 4; i++)
                bytes[i] = GetByte(word, i);

            return bytes;
        }

        public static byte[] ToBytes(uint word, ValueRepresentation representation)
        {
            if (representation == ValueRepresentation.BigEndian)
                return ToBytes(word);

            return ToBytes(word).Reverse().ToArray();
        }

        public static byte[] ToBytes(uint word)
        {
            var bytes = new byte[4];

            for (int i = 0; i < 4; i++)
                bytes[i] = GetByte(word, i);

            return bytes;
        }

        public static uint RotateLeft(uint word)
        {
            return RotateLeft(word, 1);
        }

        public static uint RotateLeft(uint word, int count)
        {
            return (word << count) | (word >> (32 - count));
        }

        public static uint RotateRight(uint word)
        {
            return RotateRight(word, 1);
        }

        public static uint RotateRight(uint word, int count)
        {
            return (word >> count) | (word << (32 - count));
        }
    }

    public enum ValueRepresentation
    {
        BigEndian, LittleEndian
    }
}

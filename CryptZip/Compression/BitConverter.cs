using System;

namespace CryptZip.Compression
{
    public static class BitConverter
    {
        public static byte ToByte(bool[] bits)
        {
            if (bits.Length > 8)
                throw new ArgumentOutOfRangeException(nameof(bits), "Byte is less or equal to eight bits");

            return Convert.ToByte(ToInt(bits));
        }

        public static int ToInt(bool[] bits)
        {
            int value = 0;
            int power = 1;

            for (int i = bits.Length - 1; i >= 0; i--, power *= 2)
                if (bits[i])
                    value += power;

            return value;
        }

        public static byte MinimalNumberOfBits(int value)
        {
            if (value <= 1)
                return 1;

            byte bitsCount = 0;
            int power = 1;

            while (power <= value)
            {
                power *= 2;
                bitsCount++;
            }

            return bitsCount;
        }

        public static bool[] ToBits(int value)
        {
            var bits = new bool[MinimalNumberOfBits(value)];
            int pow = (int)Math.Pow(2, bits.Length - 1);

            for (int i = 0; i < bits.Length; i++, pow/=2)
            {
                if (value >= pow)
                {
                    bits[i] = true;
                    value -= pow;
                }
            }

            return bits;
        }

        public static bool[] ToBits(int value, int minimalBitsCount)
        {
            var bits = new bool[minimalBitsCount];

            bool[] binaryValue = ToBits(value);

            int shift = bits.Length - binaryValue.Length;

            for (int i = 0; i < binaryValue.Length; i++)
                bits[i + shift] = binaryValue[i];

            return bits;
        }
    }
}

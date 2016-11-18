
namespace CryptZip
{
    public static class Word4Bits
    {
        public static byte RotateLeft(byte word)
        {
            bool lastBit = word >= 8;
            word <<= 1;
            if (lastBit)
                word -= 15;
            return word;
        }

        public static byte RotateLeft(byte word, int count)
        {
            for (int i = 0; i < count; i++)
                word = RotateLeft(word);

            return word;
        }

        public static byte RotateRight(byte word)
        {
            bool firstBit = (word % 2) == 1;
            word >>= 1;
            if (firstBit)
                word += 8;
            return word;
        }

        public static byte RotateRight(byte word, int count)
        {
            for (int i = 0; i < count; i++)
                word = RotateRight(word);

            return word;
        }
    }
}

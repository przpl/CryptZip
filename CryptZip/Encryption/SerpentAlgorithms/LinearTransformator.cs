
namespace CryptZip.Encryption.SerpentAlgorithms
{
    public static class LinearTransformator
    {
        public static uint[] Transform(uint[] x)
        {
            x[0] = Word32Bits.RotateLeft(x[0], 13);
            x[2] = Word32Bits.RotateLeft(x[2], 3);
            x[1] = x[1] ^ x[0] ^ x[2];
            x[3] = x[3] ^ x[2] ^ x[0] << 3;
            x[1] = Word32Bits.RotateLeft(x[1], 1);
            x[3] = Word32Bits.RotateLeft(x[3], 7);
            x[0] = x[0] ^ x[1] ^ x[3];
            x[2] = x[2] ^ x[3] ^ x[1] << 7;
            x[0] = Word32Bits.RotateLeft(x[0], 5);
            x[2] = Word32Bits.RotateLeft(x[2], 22);
            return x;
        }

        public static uint[] Inverse(uint[] x)
        {
            uint x1 = Word32Bits.RotateRight(x[2], 22) ^ x[3] ^ x[1] << 7;
            uint x2 = Word32Bits.RotateRight(x[0], 5) ^ x[1] ^ x[3];
            uint num1 = Word32Bits.RotateRight(x[3], 7);
            uint num2 = Word32Bits.RotateRight(x[1], 1);
            x[3] = num1 ^ x1 ^ x2 << 3;
            x[1] = num2 ^ x2 ^ x1;
            x[2] = Word32Bits.RotateRight(x1, 3);
            x[0] = Word32Bits.RotateRight(x2, 13);
            return x;
        }
    }
}
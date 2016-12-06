using System;

namespace CryptZip.Encryption.Rijndael
{
    public static class AESFunction
    {
        private static readonly byte[][] _galoisField =
            {
                new byte[]{2, 3, 1, 1},
                new byte[]{1, 2, 3, 1},
                new byte[]{1, 1, 2, 3},
                new byte[]{3, 1, 1, 2}
            };

        private static readonly byte[][] _inversedGaloisField =
            {
                new byte[]{14, 11, 13, 9},
                new byte[]{9, 14, 11, 13},
                new byte[]{13, 9, 14, 11},
                new byte[]{11, 13, 9, 14}
            };

        public static byte[][] SubBytes(byte[][] array)
        {
            return AesSBox.Transform(array);
        }

        public static byte[][] ReverseSubBytes(byte[][] array)
        {
            return AesSBox.Inverse(array);
        }

        public static byte[][] ShiftRows(byte[][] array)
        {
            byte[][] shifted = GetFirstRow(array);

            for (int i = 1; i < array.Length; i++)
                shifted[i] = array[i].ShiftToLeft(i);

            return shifted;
        }

        public static byte[][] ReverseShiftRows(byte[][] array)
        {
            byte[][] shifted = GetFirstRow(array);

            for (int i = 1; i < array.Length; i++)
                shifted[i] = array[i].ShiftToRight(i);

            return shifted;
        }

        private static byte[][] GetFirstRow(byte[][] array)
        {
            byte[][] shifted = new byte[array.Length][];

            shifted[0] = new byte[array[0].Length];
            Array.Copy(array[0], shifted[0], array[0].Length);

            return shifted;
        }

        public static byte[][] MixColumns(byte[][] array)
        {
            return Mix(array, _galoisField);
        }

        public static byte[][] ReverseMixColumns(byte[][] array)
        {
            return Mix(array, _inversedGaloisField);
        }

        private static byte[][] Mix(byte[][] array, byte[][] galoisField)
        {
            byte[][] mixed = GetMatrix4x4();

            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j++)
                    for (int k = 0; k < _inversedGaloisField.Length; k++)
                        mixed[j][i] ^= GaloisTable.Multiply(array[k][i], galoisField[j][k]);

            return mixed;
        }

        private static byte[][] GetMatrix4x4()
        {
            byte[][] matrix = new byte[4][];

            for (int i = 0; i < 4; i++)
                matrix[i] = new byte[4];

            return matrix;
        }

        public static byte[][] AddRoundKey(byte[][] data, byte[][] roundKey)
        {
            byte[][] result = new byte[data.Length][];

            for (int i = 0; i < data.Length; i++)
                result[i] = data[i].XOR(roundKey[i]);

            return result;
        }
    }
}
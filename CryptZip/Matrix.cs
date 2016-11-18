using System;

namespace CryptZip
{
    public class Matrix
    {
        public byte[,] Array { get; set; }
        public int Rows => Array.GetLength(0);
        public int Columns => Array.GetLength(1);

        public Matrix(byte[] array)
        {
            Array = new byte[array.Length, 1];

            for (int i = 0; i < array.Length; i++)
                Array[i, 0] = array[i];
        }

        public Matrix(byte[,] value)
        {
            Array = value;
        }

        public Matrix(int rows, int columns)
        {
            Array = new byte[rows, columns];
        }

        public byte[] ToArray()
        {
            var array = new byte[Rows];

            for (int i = 0; i < Rows; i++)
                array[i] = Array[i,0];

            return array;
        }

        public Matrix Multiply(Matrix matrix)
        {
            if (Columns != matrix.Rows)
                throw new ArrayTypeMismatchException("Equal number of columns and rows in matrices is required.");

            var result = new Matrix(Rows, matrix.Columns);

            for (int row = 0; row < result.Rows; row++)
                for (int column = 0; column < result.Columns; column++)
                    result.Array[row, column] = Multiply(Array, matrix.Array, row, column);

            return result;
        }

        private static byte Multiply(byte[,] a, byte[,] b, int rowIndex, int columnIndex)
        {
            int sum = 0;

            for (int i = 0; i < a.GetLength(1); i++)
                sum += a[rowIndex, i] * b[i, columnIndex];

            return (byte)sum;
        }
    }
}

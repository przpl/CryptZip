using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Constructor_ConstructsOneColumnArray_Constructed()
        {
            byte[] bytes = {1, 2, 3, 4, 5};
            byte[,] expected =
            {
                {1},
                {2},
                {3},
                {4},
                {5}
            };
            Matrix matrix = new Matrix(bytes);
            CollectionAssert.AreEqual(expected, matrix.Array);
        }

        [TestMethod]
        public void ToArray_ConvertsOneColumnMatrixToArray_Converted()
        {
            byte[,] bytes =
            {
                {1},
                {2},
                {3},
                {4},
                {5}
            };
            byte[] expected = { 1, 2, 3, 4, 5 };
            Matrix matrix = new Matrix(bytes);
            CollectionAssert.AreEqual(expected, matrix.ToArray());
        }

        [TestMethod]
        public void Multiply_MultipliesTwoMatrices_Calculated()
        {
            byte[,] firstBytes =
            {
                {1,2,3},
                {4,5,6}
            };
            byte[,] secondBytes =
            {
                {7,8},
                {9,10},
                {11,12}
            };
            byte[,] expectedBytes =
            {
                {58, 64},
                {139, 154}
            };
            Matrix first = new Matrix(firstBytes);
            Matrix second = new Matrix(secondBytes);
            Matrix result = first.Multiply(second);
            CollectionAssert.AreEqual(expectedBytes, result.Array);
            Assert.AreEqual(expectedBytes.GetLength(0), result.Rows);
            Assert.AreEqual(expectedBytes.GetLength(1), result.Columns);
        }
    }
}
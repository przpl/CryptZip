using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class Word32BitTests
    {
        [TestMethod]
        public void ToUint_ConvertsToUint_Converted()
        {
            byte[] bytes = {2, 108, 209, 77};
            uint excepted = 1305570306;
            Assert.AreEqual(excepted, Word32Bits.ToUint(bytes));
        }

        [TestMethod]
        public void ToUint_ConvertsToUintLittleEndian_Converted()
        {
            byte[] bytes = { 2, 108, 209, 77 };
            uint excepted = 40685901;
            Assert.AreEqual(excepted, Word32Bits.ToUint(bytes, ValueRepresentation.LittleEndian));
        }

        [TestMethod]
        public void GetByte_GetsByteFromWord_GotByte()
        {
            uint value = 1305570306;
            Assert.AreEqual(2, Word32Bits.GetByte(value, 0));
            Assert.AreEqual(108, Word32Bits.GetByte(value, 1));
            Assert.AreEqual(209, Word32Bits.GetByte(value, 2));
            Assert.AreEqual(77, Word32Bits.GetByte(value, 3));
        }

        [TestMethod]
        public void ToBytes_ConvertsToBytes_Converted()
        {
            uint word = 1305570306;
            byte[] excepted = { 2, 108, 209, 77 };
            CollectionAssert.AreEqual(excepted, Word32Bits.ToBytes(word));
        }

        [TestMethod]
        public void ToBytes_ConvertsToBytesLittleEndian_Converted()
        {
            uint word = 40685901;
            byte[] excepted = { 2, 108, 209, 77 };
            CollectionAssert.AreEqual(excepted, Word32Bits.ToBytes(word, ValueRepresentation.LittleEndian));
        }

        [TestMethod]
        public void RotateLeft_RotatesOneBit_Rotated()
        {
            uint value = 2147483649;
            uint expected = 3;
            Assert.AreEqual(expected, Word32Bits.RotateLeft(value));
        }

        [TestMethod]
        public void RotateLeft_RotatesTwoBits_Rotated()
        {
            uint value = 2147483649;
            uint expected = 6;
            Assert.AreEqual(expected, Word32Bits.RotateLeft(value, 2));
        }

        [TestMethod]
        public void RotateRight_RotatesOneBit_Rotated()
        {
            uint value = 3;
            uint expected = 2147483649;
            Assert.AreEqual(expected, Word32Bits.RotateRight(value));
        }

        [TestMethod]
        public void RotateRight_RotatesTwoBits_Rotated()
        {
            uint value = 6;
            uint expected = 2147483649;
            Assert.AreEqual(expected, Word32Bits.RotateRight(value, 2));
        }
    }
}
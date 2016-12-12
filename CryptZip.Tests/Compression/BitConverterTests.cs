using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class BitConverterTests
    {
        [TestMethod]
        public void ToInt_Converts_Coverted()
        {
            bool[] binaryValue =
            {
                true,
                true,
                false,
                true,
                false,
                false,
                true,
                true,
            };
            Assert.AreEqual(211, BitConverter.ToInt(binaryValue));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides0_Calculated()
        {
            Assert.AreEqual(1, BitConverter.MinimalNumberOfBits(0));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides1_Calculated()
        {
            Assert.AreEqual(1, BitConverter.MinimalNumberOfBits(1));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides2_Calculated()
        {
            Assert.AreEqual(2, BitConverter.MinimalNumberOfBits(2));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides5_Calculated()
        {
            Assert.AreEqual(3, BitConverter.MinimalNumberOfBits(5));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides17_Calculated()
        {
            Assert.AreEqual(5, BitConverter.MinimalNumberOfBits(17));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides63_Calculated()
        {
            Assert.AreEqual(6, BitConverter.MinimalNumberOfBits(63));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides64_Calculated()
        {
            Assert.AreEqual(7, BitConverter.MinimalNumberOfBits(64));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides65_Calculated()
        {
            Assert.AreEqual(7, BitConverter.MinimalNumberOfBits(65));
        }

        [TestMethod]
        public void MinimalNumberOfBits_Provides240_Calculated()
        {
            Assert.AreEqual(8, BitConverter.MinimalNumberOfBits(240));
        }

        [TestMethod]
        public void ToBits_Converts8bits_Converted()
        {
            bool[] expected =
                {
                    true,
                    true,
                    false,
                    true,
                    false,
                    false,
                    true,
                    true,
                };
            CollectionAssert.AreEqual(expected, BitConverter.ToBits(211));
        }

        [TestMethod]
        public void ToBits_Converts9Bits_Converted()
        {
            bool[] expected =
                {
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    true,
                    false
                };
            bool[] temp = BitConverter.ToBits(258);
            CollectionAssert.AreEqual(expected, temp);
        }
    }
}

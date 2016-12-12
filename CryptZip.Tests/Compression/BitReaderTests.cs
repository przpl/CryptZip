using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class BitReaderTests
    {
        [TestMethod]
        public void ReadNext_Reads8Bits_Equal()
        {
            byte value = Convert.ToByte("10110110", 2);
            var bitReader = new BitReader(new MemoryStream(new[] { value }));
            bool[] expected =
            {
                true,
                false,
                true,
                true,
                false,
                true,
                true,
                false
            };
            CollectionAssert.AreEqual(expected, bitReader.Read(8));
        }

        [TestMethod]
        public void ReadNext_Reads12Bits_Equal()
        {
            byte value1 = Convert.ToByte("10110110", 2);
            byte value2 = Convert.ToByte("00100000", 2);
            var bitReader = new BitReader(new MemoryStream(new[] { value1, value2 }));
            bool[] expected =
            {
                true,
                false,
                true,
                true,
                false,
                true,
                true,
                false,
                false,
                false,
                true,
                false
            };
            CollectionAssert.AreEqual(expected, bitReader.Read(12));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ReadNext_ReadsTooManyBits_ThrowsException()
        {
            byte value = Convert.ToByte("10110110", 2);
            var bitReader = new BitReader(new MemoryStream(new[] { value }));
            bitReader.Read(9);
        }

        [TestMethod]
        public void BytesLeft_Initialize_OneByte()
        {
            byte value = Convert.ToByte("10110110", 2);
            var bitReader = new BitReader(new MemoryStream(new[] { value }));
            Assert.AreEqual(1, bitReader.BytesLeft);
        }

        [TestMethod]
        public void BytesLeft_ReadsOneBit_ZeroBytes()
        {
            byte value = Convert.ToByte("10110110", 2);
            var bitReader = new BitReader(new MemoryStream(new[] { value }));
            bitReader.Read(1);
            Assert.AreEqual(0, bitReader.BytesLeft);
        }

        [TestMethod]
        public void BytesLeft_ReadsEightBits_OneByte()
        {
            byte value1 = Convert.ToByte("10110110", 2);
            byte value2 = Convert.ToByte("00100000", 2);
            var bitReader = new BitReader(new MemoryStream(new[] { value1, value2 }));
            bitReader.Read(8);
            Assert.AreEqual(1, bitReader.BytesLeft);
        }
    }
}
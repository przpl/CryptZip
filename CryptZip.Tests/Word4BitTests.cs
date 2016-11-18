using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests
{
    [TestClass]
    public class Word4BitTests
    {
        [TestMethod]
        public void RotateLeft_RotatesOneBit_Rotated()
        {
            Assert.AreEqual(15, Word4Bits.RotateLeft(15));
            Assert.AreEqual(14, Word4Bits.RotateLeft(7));
        }

        [TestMethod]
        public void RotateLeft_RotatesThreeBits_Rotated()
        {
            Assert.AreEqual(15, Word4Bits.RotateLeft(15, 3));
            Assert.AreEqual(11, Word4Bits.RotateLeft(7, 3));
        }

        [TestMethod]
        public void RotateRight_RotatesOneBit_Rotated()
        {
            Assert.AreEqual(15, Word4Bits.RotateRight(15));
            Assert.AreEqual(7, Word4Bits.RotateRight(14));
        }

        [TestMethod]
        public void RotateRight_RotatesThreeBits_Rotated()
        {
            Assert.AreEqual(15, Word4Bits.RotateRight(15, 3));
            Assert.AreEqual(7, Word4Bits.RotateRight(11, 3));
        }
    }
}

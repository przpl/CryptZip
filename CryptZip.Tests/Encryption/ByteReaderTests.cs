using CryptZip.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Encryption
{
    [TestClass]
    public class ByteReaderTests
    {
        [TestMethod]
        public void HasEnded_ReadsAllBytes_Ended()
        {
            ByteReader byteReader = new ByteReader(new byte[] {1,2,3}, 8, 8);
            byteReader.ReadByte();
            byteReader.ReadByte();
            byteReader.ReadByte();
            Assert.AreEqual(true, byteReader.HasEnded());
        }

        [TestMethod]
        public void HasEnded_ReadsOneByte_NotEnded()
        {
            ByteReader byteReader = new ByteReader(new byte[] { 1, 2, 3 }, 8, 8);
            byteReader.ReadByte();
            Assert.AreEqual(false, byteReader.HasEnded());
        }

        [TestMethod]
        public void HasEnded_ReadsZeroBytes_NotEnded()
        {
            ByteReader byteReader = new ByteReader(new byte[] { 1, 2, 3 }, 8, 8);
            Assert.AreEqual(false, byteReader.HasEnded());
        }
    }
}

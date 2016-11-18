using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptZip.Encryption;

namespace CryptZip.Tests
{
    [TestClass]
    public class ByteWriterTests
    {
        [TestMethod]
        public void WriteWords_WritesTwoWords_Written()
        {
            ByteWriter byteWriter = new ByteWriter(8);
            byteWriter.WriteWords(new uint[] {562714,217417});
            CollectionAssert.AreEqual(new byte[] {0x1a, 0x96, 0x08, 0x00, 0x49, 0x51, 0x03, 0x00}, byteWriter.Bytes);
        }

        [TestMethod]
        public void WriteBytes_WritesTwoBytes_Written()
        {
            ByteWriter byteWriter = new ByteWriter(4);
            byteWriter.WriteBytes(new byte[] { 1, 2 });
            CollectionAssert.AreEqual(new byte[] { 1, 2, 0, 0 }, byteWriter.Bytes);
        }

        [TestMethod]
        public void WriteByte_WritesOne_Written()
        {
            ByteWriter byteWriter = new ByteWriter(4);
            byteWriter.WriteByte(1);
            CollectionAssert.AreEqual(new byte[] { 1, 0, 0, 0 }, byteWriter.Bytes);
        }
    }
}

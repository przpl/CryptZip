using System.IO;
using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class BitWriterTests
    {
        [TestMethod]
        public void GetByte_Write8Bits_Equal()
        {
            MemoryStream stream = new MemoryStream();
            var writer = new BitWriter(stream);
            writer.Write(true);
            writer.Write(false);
            writer.Write(true);
            writer.Write(true);
            writer.Write(true);
            writer.Write(true);
            writer.Write(false);
            writer.Write(true);

            writer.Write(true);
            writer.Write(false);
            writer.Write(false);
            writer.Write(false);
            Assert.AreEqual(189, stream.ToArray()[0]);
            writer.Write(true);
            writer.Write(false);
            writer.Write(true);
            writer.Write(false);
            Assert.AreEqual(138, stream.ToArray()[1]);
        }

        [TestMethod]
        public void GetLastByte_Writes4Bits_CompleteByte()
        {
            MemoryStream stream = new MemoryStream();
            var writer = new BitWriter(stream);
            writer.Write(true);
            writer.Write(true);
            writer.Write(true);
            writer.Write(true);
            writer.Flush();
            Assert.AreEqual(240, stream.ToArray()[0]);
        }
    }
}

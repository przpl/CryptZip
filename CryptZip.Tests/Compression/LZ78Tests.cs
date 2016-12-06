using CryptZip.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CryptZip.Tests.Compression
{
    [TestClass]
    public class LZ78Tests
    {
        [TestMethod]
        public void Compress_Compresses_Compressed()
        {
            var lz78 = new LZ78();
            var input = new MemoryStream(new byte[]{ 2, 5, 7, 1, 8, 2, 5, 2, 0, 2, 5, 1, 6, 8, 9, 2, 1 });
            var output = new MemoryStream();
            byte[] expected = { 1, 0, 160, 56, 1, 1, 4, 20, 128, 48, 8, 3, 40, 72, 128, 128 };
            lz78.Compress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decompress_Decompresses_Decompressed()
        {
            var lz78 = new LZ78();
            var input = new MemoryStream(new byte[] { 1, 0, 160, 56, 1, 1, 4, 20, 128, 48, 8, 3, 40, 72, 128, 128 });
            var output = new MemoryStream();
            byte[] expected = { 2, 5, 7, 1, 8, 2, 5, 2, 0, 2, 5, 1, 6, 8, 9, 2, 1 };
            lz78.Decompress(input, output);
            byte[] result = output.ToArray();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
